// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Invoices.Events;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {

        public Task<InvoiceResult> CreateInvoice(IProviderAgencyOwner agencyOwner, InvoiceInput input)
        {
            _logger.LogInformation(GetLogMessage("Creating invoice as agency owner"));
            return Create(input, agencyOwner.OrganizationId);
        }

        public Task<InvoiceResult> CreateInvoice(IOrganizationAccountManager am, InvoiceInput input)
        {
            _logger.LogInformation(GetLogMessage("Creating invoice as account manager"));
            return Create(input, am.OrganizationId);
        }
        
        private async Task<InvoiceResult> Create(InvoiceInput input, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage("{organizationId} with options {@input}"), organizationId, input);

            var retVal = new InvoiceResult()
            {
                ProjectId = input.ProjectId
            };

            var project = await _projectService
                .Repository.Queryable()
                .Include(x => x.CustomerAccount)
                .ThenInclude(x => x.PaymentTerm)
                .Include(x => x.Contracts)
                .ThenInclude(x => x.InvoiceItems)
                .Include(x => x.BuyerOrganization)
                .ThenInclude(x => x.OrganizationBuyerAccount)
                .Include(x => x.ProviderOrganization)
                .Include(x => x.Contracts)
                .ThenInclude(x => x.TimeEntries)
                .Include(x => x.Contracts)
                .ThenInclude(x => x.Contractor)
                .ThenInclude(x => x.Person)
                .Include(x => x.ProviderOrganization)
                .ThenInclude(x => x.Organization)
                .Include(x => x.Contracts)
                .ThenInclude(x => x.ProviderOrganization)
                .ThenInclude(x => x.Organization)
                .Where(x => x.Id == input.ProjectId && x.ProjectManagerOrganizationId == organizationId)
                .FirstAsync();

            if (project == null)
                throw new ApplicationException("Project Not Found. Id : " + input.ProjectId + " Organization Id : " + organizationId);

            if (project.BuyerOrganization.OrganizationBuyerAccount == null)
            {
                _logger.LogInformation(GetLogMessage("No buyer account found, creating..."));

                var result = await _buyerAccountService.PushCustomer(project.CustomerOrganizationId, project.CustomerId);
                if (result > 0)
                {
                    _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);
                    
                    return await Create(input, organizationId);
                }

                retVal.ErrorMessage = "Unable to establish buyer account for customer";
                return retVal;
            }

            List<Contract> contracts;

            // this could be filtered by active, etc

            if (input.IncludeAllContracts)
                contracts = project.Contracts.ToList();
            else
                contracts = project
                    .Contracts
                    .Where(x => input.ContractIds.Contains(x.Id))
                    .ToList();

            _logger.LogDebug(GetLogMessage("Contracts Found: {contracts}"), contracts.Count);
            _logger.LogDebug(GetLogMessage("Buyer Account: {buyerAcct}"), project
                .BuyerOrganization.OrganizationBuyerAccount.BuyerAccountId);

            var options = new InvoiceCreateOptions()
            {
                Customer = project.BuyerOrganization.OrganizationBuyerAccount.BuyerAccountId,
                AutoAdvance = false,
                CollectionMethod = "send_invoice",
                //DaysUntilDue = project.CustomerAccount.PaymentTerm.NetValue > 0 ? project.CustomerAccount.PaymentTerm.NetValue : 1,
                //Footer = project.Proposal.AgreementText,
                DueDate = DateTime.Today.AddDays(project.CustomerAccount.PaymentTerm.NetValue > 0 ? project.CustomerAccount.PaymentTerm.NetValue : 1),
                CustomFields = new List<InvoiceCustomFieldOptions>()
                {
                    new InvoiceCustomFieldOptions()
                    {
                        Name = "Project Name",
                        Value = project.Name
                    },
                    new InvoiceCustomFieldOptions()
                    {
                        Name = "Provider Company",
                        Value = project.ProviderOrganization.Organization.Name
                    }
                },
                Metadata = new Dictionary<string, string>()
                {
                    { "proj_id", project.Id.ToString() }
                }
            };

            var itemsCreated = 0;
            var itemsUpdated = 0;

            foreach (var c in contracts)
            {
                _logger.LogInformation(GetLogMessage("Contract Id: {0}"), c.Id);

                var timeEntries = c.TimeEntries
                    .Where(x => x.Status == TimeStatus.Approved && x.InvoiceItemId == null)
                    .ToList();

                _logger.LogDebug(GetLogMessage("{entries} Approved Entries Found"), timeEntries.Count);

                var totalHours = timeEntries.Sum(x => x.TotalHours);
                if (totalHours > 0)
                {
                    var totalCustomerAmount = timeEntries.Sum(x => x.TotalCustomerAmount);
                    var ancientEntry = c.TimeEntries.Min(x => x.StartDate);
                    var latterDayEntry = c.TimeEntries.Max(x => x.EndDate);

                    var hours = totalHours.ToString("F");

                    _logger.LogDebug(GetLogMessage("Hours: {0}"), hours);

                    _logger.LogDebug(GetLogMessage("Amount {amount:C}"), totalCustomerAmount);


                    var description =
                        $@"{hours} Hours Worked by {c.Contractor.Person.DisplayName} [{c.ProviderOrganization.Organization.Name}]";

                    var hasInvoiceItems = c.InvoiceItems.Any(x => x.InvoiceId == null);
                    if (hasInvoiceItems)
                    {
                        var invoiceItems = c.InvoiceItems.Where(x => x.InvoiceId == null);
                        _logger.LogDebug(GetLogMessage("Contract has invoice items : {0}"), invoiceItems.Count());
                        foreach (var item in invoiceItems)
                        {
                            _logger.LogDebug(GetLogMessage("Invoice Item Id : {0}"), item.Id);
                            var stripeItem = _invoiceItemService.Update(item.Id, new InvoiceItemUpdateOptions()
                            {
                                Description = description,
                                Amount = Convert.ToInt64(totalCustomerAmount * 100m)
                            });

                            if (stripeItem != null)
                            {
                                _logger.LogDebug(GetLogMessage("Item Updated in Stripe. Stripe Item Id : {0}"), stripeItem.Id);
                                itemsUpdated++;
                            }
                            else
                            {
                                _logger.LogDebug(GetLogMessage("Item Update Failed in Stripe"));
                            }
                        }
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("Contract doesn't have invoice items. Creating New Invoice Item"));
                        var invoiceItemOptions = new InvoiceItemCreateOptions()
                        {
                            Period = new InvoiceItemPeriodOptions()
                            {
                                Start = ancientEntry.DateTime,
                                End = latterDayEntry.DateTime
                            },
                            Customer = project.BuyerOrganization.OrganizationBuyerAccount.BuyerAccountId,
                            Amount = Convert.ToInt64(totalCustomerAmount * 100),
                            Currency = "usd",
                            Description = description,
                            Metadata = new Dictionary<string, string>()
                            {
                                {  "contract-id", c.Id.ToString() }
                            }
                        };

                        _logger.LogInformation(GetLogMessage("options: {0}"), invoiceItemOptions);

                        var invoiceItem = _invoiceItemService.Create(invoiceItemOptions);

                        _logger.LogDebug(GetLogMessage("Invoice Item: {0}"), invoiceItem);

                        var invoiceItemResult = await InvoiceItemCreated(invoiceItem);

                        _logger.LogDebug(GetLogMessage("Invoice Item Result: {@result}"), invoiceItemResult);

                        if (invoiceItemResult.Succeeded)
                        {
                            itemsCreated++;
                        }
                    }

                    if (itemsUpdated + itemsCreated > 0)
                    {
                        c.ObjectState = ObjectState.Modified;
                    }

                }
                else
                {
                    _logger.LogDebug(GetLogMessage("no billable time for {contract}"), c.Id);
                }
            }

            var entriesUpdated = _timeEntries.Commit();

            _logger.LogDebug(GetLogMessage("Entries Updated: {entriesUpdated}"), entriesUpdated);
            if (entriesUpdated == 0)
            {
                _logger.LogWarning(GetLogMessage("No Entities were updated"));
            }

            _logger.LogInformation(GetLogMessage("options: {@Options}"), options);

            var invoice = _invoiceService.Create(options);

            if (invoice != null)
            {
                var stripeResult = await InvoiceCreated(invoice, input.RefNo);

                _logger.LogDebug(GetLogMessage("Stripe Result: {@result}"), stripeResult);

                if (stripeResult.Succeeded)
                {
                    retVal.Succeeded = true;
                    retVal.InvoiceId = invoice.Id;
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Unable to create invoice"));
            }
            

            if(retVal.Succeeded)
                await Task.Run(() => new InvoiceCreatedEvent()
                {
                    InvoiceId = invoice.Id
                });
             
             return retVal;

        }
    }
}