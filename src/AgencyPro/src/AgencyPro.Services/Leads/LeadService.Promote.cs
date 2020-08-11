// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.Extensions;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Leads.Models;
using Microsoft.AspNetCore.Identity;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        public async Task<PromoteLeadResult> PromoteLead(
            IOrganizationAccountManager am,
            Guid leadId,
            PromoteLeadOptions options)
        {

            _logger.LogInformation(GetLogMessage("Account manager promotes lead: {0}"), leadId);

            var lead = await Repository.Queryable()
                .Where(x => x.Id == leadId)
                .ForOrganizationAccountManager(am)
                .FirstOrDefaultAsync();

            if (lead == null)
                throw new ApplicationException("Lead not found");

            options.PhoneNumber =
                !string.IsNullOrEmpty(options.PhoneNumber?.Trim()) ? options.PhoneNumber.Trim() : null;

            var customer = await _customers.Queryable()
                .Include(x => x.Person)
                .ThenInclude(x => x.ApplicationUser)
                .Where(x => x.Person.ApplicationUser.Email == options.Email)
                .FirstOrDefaultAsync();

            var isNotValid = CheckValidation(leadId, options, customer != null, out var retVal);

            if (isNotValid) return retVal;

            if (lead.Status != LeadStatus.Promoted)
            {
                lead.EmailAddress = options.Email;
                lead.PhoneNumber = options.PhoneNumber;
                lead.OrganizationName = options.OrganizationName;
                lead.Status = LeadStatus.Promoted;
                lead.IsContacted = true;
                lead.UpdatedById = _userInfo.Value.UserId;
                lead.Updated = DateTimeOffset.UtcNow;
                lead.ObjectState = ObjectState.Modified;
                lead.StatusTransitions.Add(new LeadStatusTransition()
                {
                    Status = LeadStatus.Promoted,
                    ObjectState = ObjectState.Added
                });

                var leadResult = Repository.InsertOrUpdateGraph(lead, true);
                
                _logger.LogDebug(GetLogMessage("{0} records updated"), leadResult);

                if (leadResult > 0)
                {
                    retVal.Succeeded = true;

                    CustomerAccountResult account;
                    if (customer == null)
                    {
                        _logger.LogDebug(GetLogMessage("Customer not found with Lead Email: {0}"), lead.EmailAddress);
                        account = await _accountService.Create(am, new NewCustomerAccountInput()
                        {
                            Iso2 = options.Iso2,
                            ProvinceState = options.ProvinceState,
                            FirstName = options.FirstName,
                            LastName = options.LastName,
                            AccountManagerId = am.AccountManagerId,
                            EmailAddress = options.Email,
                            MarketerId = lead.MarketerId,
                            MarketerOrganizationId = lead.MarketerOrganizationId,
                            PaymentTermId = options.PaymentTermId.GetValueOrDefault(1),
                            OrganizationName = options.OrganizationName,
                            PhoneNumber = options.PhoneNumber,
                            SendEmail = true,
                        }, false);

                        if (account.Succeeded)
                        {
                            _logger.LogDebug(GetLogMessage("Account created"));

                            retVal.AccountCreated = true;

                            lead.PersonId = account.CustomerId;
                            lead.ObjectState = ObjectState.Modified;
                            lead.Updated = DateTimeOffset.UtcNow;
                            
                            var records = Repository.InsertOrUpdateGraph(lead, true);

                            _logger.LogDebug(GetLogMessage("{0} Records updated"), records);

                            if (records > 0)
                            {
                                await Task.Run(() =>
                                {
                                    RaiseEvent(new LeadPromotedEvent()
                                    {
                                        LeadId = leadId
                                    });
                                });
                            }
                        }
                        else
                        {
                            _logger.LogDebug(GetLogMessage("Account creation failed : {0}"), account.ErrorMessage);
                        }
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("Customer found with Id : {0}, Lead Email : {1}"), customer.Id, lead.EmailAddress);

                        account = await _accountService.LinkOrganizationCustomer(am, new LinkCustomerInput()
                        {
                            AccountManagerId = am.AccountManagerId,
                            AccountManagerOrganizationId = am.OrganizationId,
                            EmailAddress = lead.EmailAddress,
                            PaymentTermId = options.PaymentTermId
                        });

                        if (account.Succeeded)
                        {
                            retVal.AccountLinked = true;
                            await Task.Run(() =>
                            {
                                RaiseEvent(new LeadPromotedExistingCustomer()
                                {
                                    LeadId = leadId
                                });
                            });
                        }
                        else
                        {
                            _logger.LogDebug(GetLogMessage("Account Link failed : {0}"), account.ErrorMessage);
                        }
                    }

                    if (account.Succeeded)
                    {
                        retVal.Succeeded = true;
                        retVal.AccountNumber = account.Number;
                        retVal.CustomerOrganizationId = account.CustomerOrganizationId;
                    }
                }
            }
            else
            {

                _logger.LogDebug(GetLogMessage("Lead is already promoted"));
            }
            
            return retVal;

        }

        private bool CheckValidation(Guid leadId, PromoteLeadOptions options, bool customerExists, out PromoteLeadResult retVal)
        {
            bool isPhoneExists = false;
            bool isOrganizationExists = false;
            bool isEmailExists = false;

            if (!string.IsNullOrEmpty(options.PhoneNumber?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Phone Number validation : {0}"), options.PhoneNumber);
                isPhoneExists = _applicationUsers.Queryable().Any(a => a.PhoneNumber == options.PhoneNumber);
            }

            if (!customerExists && !string.IsNullOrEmpty(options.Email?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Email validation : {0}"), options.Email);
                isEmailExists = _applicationUsers.Queryable().Any(a => a.Email == options.Email);
            }

            if (!customerExists && !string.IsNullOrEmpty(options.OrganizationName?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Organization Name validation : {0}"), options.OrganizationName);
                isOrganizationExists = _organizations.Queryable().Any(a => a.Name == options.OrganizationName);
            }

            retVal = new PromoteLeadResult
            {
                LeadId = leadId
            };

            if (!isPhoneExists && !isOrganizationExists && !isEmailExists) return false;

            retVal.Succeeded = false;
            retVal.ErrorMessage = GetErrorMessage(isPhoneExists, isOrganizationExists, isEmailExists);
            _logger.LogInformation(GetLogMessage(retVal.ErrorMessage));
            return true;
        }

        private string GetErrorMessage(bool phoneValidation, bool organizationValidation, bool isEmailExists)
        {
            List<string> validationList = new List<string>();
            if (phoneValidation)
                validationList.Add("Phone number");

            if (organizationValidation)
                validationList.Add("Organization name");

            if (isEmailExists)
                validationList.Add("Email");

            return string.Join(", ", validationList) + " already exists.";
        }
    }
}