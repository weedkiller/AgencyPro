// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using Stripe;

namespace AgencyPro.Services.Invoices
{
    public partial class ProjectInvoiceService
    {
        public async Task<InvoiceResult> InvoiceSent(Invoice invoice)
        {
            _logger.LogInformation(GetLogMessage("{invoice}"), invoice.Id);

            var retVal = new InvoiceResult {InvoiceId = invoice.Id};



            var entity = await _invoices.Queryable()
                .Include(x => x.Items)
                .Where(x => x.Id == invoice.Id)
                .FirstOrDefaultAsync();


            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                Guid projectId = Guid.Parse(invoice.Metadata["proj_id"]);

                retVal.ProjectId = projectId;

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not a project invoice"));
            }
            
            var records = _invoices.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} invoice records updated"), records);
            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public async Task<InvoiceResult> InvoiceDeleted(Invoice invoice)
        {
            _logger.LogInformation(GetLogMessage("{invoice}"), invoice.Id);


            var retVal = new InvoiceResult {InvoiceId = invoice.Id};


            var entity = await _invoices.Queryable()
                .Include(x => x.Lines)
                .Include(x => x.ProjectInvoice)
                .Where(x => x.Id == invoice.Id)
                .FirstOrDefaultAsync();


            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                Guid projectId = Guid.Parse(invoice.Metadata["proj_id"]);

                retVal.ProjectId = projectId;
               
                foreach (var x in entity.Lines)
                {
                    x.ObjectState = ObjectState.Deleted;
                }
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not a project invoice"));
            }

            entity.IsDeleted = true;
            entity.ObjectState = ObjectState.Modified;


            var records = _invoices.InsertOrUpdateGraph(entity, true);
            _logger.LogInformation(GetLogMessage("{0} Invoice Records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public async Task<InvoiceResult> InvoiceUpdated(Invoice invoice)
        {
            _logger.LogInformation(GetLogMessage("Invoice Id : {invoice}"), invoice.Id);
            _logger.LogInformation(GetLogMessage("Invoice Metadata : {0}"), invoice.Metadata);

            var entity = await _invoices.Queryable()
                .Include(x => x.Lines)
                .Include(x => x.ProjectInvoice)
                .Where(x => x.Id == invoice.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new ApplicationException("Invoice not found. Invoice Id :" + invoice.Id);

            var retVal = new InvoiceResult { InvoiceId = invoice.Id };

            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                Guid projectId = Guid.Parse(invoice.Metadata["proj_id"]);

                retVal.ProjectId = projectId;
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not a project invoice"));
            }

            // mark as deleted unless they are preserved
            foreach (var x in entity.Lines) x.ObjectState = ObjectState.Deleted;

            foreach (var line in invoice.Lines)
            {
                var li = entity.Lines.FirstOrDefault(x => x.Id == line.Id);
                if (li != null)
                {
                    li.ObjectState = ObjectState.Modified;
                }
                else
                {
                    li = new StripeInvoiceLine
                    {
                        ObjectState = ObjectState.Added
                    };

                    entity.Lines.Add(li);
                }

                li.InvoiceId = invoice.Id;
                li.PeriodEnd = line.Period.End;
                li.PeriodStart = line.Period.Start;
                li.Amount = Convert.ToDecimal(line.Amount / 100m);

                li.InjectFrom(line);
            }

            UpdateInvoiceItems(invoice);

            entity.InjectFrom(invoice);

            entity.Updated = DateTimeOffset.UtcNow;
            entity.Id = invoice.Id;
            entity.AmountDue = Convert.ToDecimal(invoice.AmountDue / 100m);
            entity.AmountRemaining = Convert.ToDecimal(invoice.AmountRemaining / 100m);
            entity.AmountPaid = Convert.ToDecimal(invoice.AmountPaid / 100m);
            entity.Total = Convert.ToDecimal(invoice.Total / 100m);
            entity.Subtotal = Convert.ToDecimal(invoice.Subtotal / 100m);

            entity.Attempted = invoice.Attempted;
            entity.CustomerId = invoice.CustomerId;
            entity.InvoicePdf = invoice.InvoicePdf;
            entity.Status = invoice.Status;
            entity.SubscriptionId = invoice.SubscriptionId;
            entity.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
            entity.BillingReason = invoice.BillingReason;
            entity.AttemptCount = invoice.AttemptCount;
            entity.Number = invoice.Number;
            entity.ObjectState = ObjectState.Modified;
            
            var records = _invoices.InsertOrUpdateGraph(entity, true);
            _logger.LogDebug(GetLogMessage("{0} invoice records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }

        public async Task<InvoiceResult> InvoicePaymentSucceeded(Invoice invoice)
        {
            _logger.LogInformation(GetLogMessage("Invoice Id : {invoice}"), invoice.Id);

            _logger.LogInformation(GetLogMessage("Invoice Metadata : {0}"), invoice.Metadata);

            var entity = await _invoices.Queryable()
                .Include(x => x.Items)
                .ThenInclude(x => x.TimeEntries)
                .Include(x => x.Items)
                .ThenInclude(x => x.IndividualPayoutIntents)
                .Include(x => x.Items)
                .ThenInclude(x => x.OrganizationPayoutIntents)
                .Include(x => x.Items)
                .ThenInclude(x => x.Contract)
                .Where(x => x.Id == invoice.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
                throw new ApplicationException("Invoice not found. Invoice Id : " + invoice.Id);

            var retVal = new InvoiceResult { InvoiceId = invoice.Id };

            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                Guid projectId = Guid.Parse(invoice.Metadata["proj_id"]);

                retVal.ProjectId = projectId;
                var processableTimeEntries = entity.Items.SelectMany(x => x.TimeEntries).ToList();

                _logger.LogDebug(GetLogMessage("Time Entries to Process: {0}"), processableTimeEntries.Count);

                foreach (var timeEntry in processableTimeEntries)
                {
                    var originalStatus = timeEntry.Status;

                    if (originalStatus != TimeStatus.PendingPayout)
                    {
                        timeEntry.Status = TimeStatus.PendingPayout;
                        timeEntry.Updated = DateTimeOffset.UtcNow;
                        timeEntry.ObjectState = ObjectState.Modified;

                        timeEntry.StatusTransitions.Add(new TimeEntryStatusTransition()
                        {
                            Status = TimeStatus.PendingPayout,
                            ObjectState = ObjectState.Added
                        });
                    }
                }

                _logger.LogDebug(GetLogMessage("Invoice Items to Process: {0}"), entity.Items.Count);


                foreach (var item in entity.Items)
                {
                    var totalAccountManagerStream = item.TimeEntries.Sum(x => x.TotalAccountManagerStream);
                    if (totalAccountManagerStream > 0)
                    {
                        item.IndividualPayoutIntents.Add(new IndividualPayoutIntent
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.AccountManagerOrganizationId,
                            PersonId = item.Contract.AccountManagerId,
                            Amount = item.TimeEntries.Sum(x => x.TotalAccountManagerStream),
                            Type = CommissionType.AccountManagerStream,
                            InvoiceItemId = item.Id,
                            Description = item.Description,
                            ObjectState = ObjectState.Added
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No project manager stream added"));
                    }


                    var totalProjectManagerStream = item.TimeEntries.Sum(x => x.TotalProjectManagerStream);
                    if (totalProjectManagerStream > 0)
                    {
                        item.IndividualPayoutIntents.Add(new IndividualPayoutIntent
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.ProjectManagerOrganizationId,
                            PersonId = item.Contract.ProjectManagerId,
                            Amount = item.TimeEntries.Sum(x => x.TotalProjectManagerStream),
                            Type = CommissionType.ProjectManagerStream,
                            InvoiceItemId = item.Id,
                            Description = item.Description,
                            ObjectState = ObjectState.Added
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No project manager stream added"));
                    }



                    var totalContractorStream = item.TimeEntries.Sum(x => x.TotalContractorStream);
                    if (totalContractorStream > 0)
                    {
                        item.IndividualPayoutIntents.Add(new IndividualPayoutIntent
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.ContractorOrganizationId,
                            PersonId = item.Contract.ContractorId,
                            Amount = item.TimeEntries.Sum(x => x.TotalContractorStream),
                            Type = CommissionType.ContractorStream,
                            InvoiceItemId = item.Id,
                            Description = item.Description,
                            ObjectState = ObjectState.Added
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No contractor stream added"));
                    }


                    var totalRecruiterStream = item.TimeEntries.Sum(x => x.TotalRecruiterStream);
                    if (totalRecruiterStream > 0)
                    {
                        item.IndividualPayoutIntents.Add(new IndividualPayoutIntent
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.RecruiterOrganizationId,
                            PersonId = item.Contract.RecruiterId,
                            Amount = item.TimeEntries.Sum(x => x.TotalRecruiterStream),
                            Type = CommissionType.RecruiterStream,
                            InvoiceItemId = item.Id,
                            Description = item.Description,
                            ObjectState = ObjectState.Added
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No recruiter stream added"));
                    }


                    var totalMarketerStream = item.TimeEntries.Sum(x => x.TotalMarketerStream);
                    if (totalMarketerStream > 0)
                    {
                        item.IndividualPayoutIntents.Add(new IndividualPayoutIntent
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.MarketerOrganizationId,
                            PersonId = item.Contract.MarketerId,
                            Amount = totalMarketerStream,
                            Type = CommissionType.MarketerStream,
                            InvoiceItemId = item.Id,
                            Description = item.Description,
                            ObjectState = ObjectState.Added
                        });
                    }

                    var totalMarketingAgencyStream = item.TimeEntries.Sum(x => x.TotalMarketingAgencyStream);
                    if (totalMarketingAgencyStream>0)
                    {
                        item.OrganizationPayoutIntents.Add(new OrganizationPayoutIntent()
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.MarketerOrganizationId,
                            Amount = totalMarketingAgencyStream,
                            Type = CommissionType.MarketingAgencyStream,
                            Description = item.Description,
                            ObjectState = ObjectState.Added,
                            InvoiceItemId = item.Id
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No marketing agency stream added"));
                    }

                    var totalRecruitingAgencyStream = item.TimeEntries.Sum(x => x.TotalRecruitingAgencyStream);
                    if (totalRecruitingAgencyStream > 0)
                    {
                        item.OrganizationPayoutIntents.Add(new OrganizationPayoutIntent()
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.RecruiterOrganizationId,
                            Amount = item.TimeEntries.Sum(x => x.TotalRecruitingAgencyStream),
                            Type = CommissionType.RecruitingAgencyStream,
                            Description = item.Description,
                            ObjectState = ObjectState.Added,
                            InvoiceItemId = item.Id
                        });
                    }
                    else
                    {
                        _logger.LogDebug(GetLogMessage("No recruiting agency stream added"));
                    }

                    var totalAgencyStream = item.TimeEntries.Sum(x => x.TotalAgencyStream);
                    if (totalAgencyStream > 0)
                    {
                        item.OrganizationPayoutIntents.Add(new OrganizationPayoutIntent()
                        {
                            InvoiceId = item.InvoiceId,
                            OrganizationId = item.Contract.ContractorOrganizationId,
                            Amount = totalAgencyStream,
                            Type = CommissionType.ProviderAgencyStream,
                            Description = item.Description,
                            ObjectState = ObjectState.Added,
                            InvoiceItemId = item.Id
                        });
                    }
                   
                    item.ObjectState = ObjectState.Modified;
                }
               
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not a project invoice"));
            }

            entity.Updated = DateTimeOffset.UtcNow;
            entity.Id = invoice.Id;
            entity.AmountDue = Convert.ToDecimal(invoice.AmountDue / 100m);
            entity.AmountRemaining = Convert.ToDecimal(invoice.AmountRemaining / 100m);
            entity.Attempted = invoice.Attempted;
            entity.CustomerId = invoice.CustomerId;
            entity.InvoicePdf = invoice.InvoicePdf;
            entity.Status = invoice.Status;
            entity.SubscriptionId = invoice.SubscriptionId;
            entity.AmountPaid = Convert.ToDecimal(invoice.AmountPaid / 100m);
            entity.Total = Convert.ToDecimal(invoice.Total / 100m);
            entity.Subtotal = Convert.ToDecimal(invoice.Subtotal / 100m);
            entity.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
            entity.BillingReason = invoice.BillingReason;
            entity.AttemptCount = invoice.AttemptCount;
            entity.Number = invoice.Number;
            entity.ObjectState = ObjectState.Modified;
            entity.StripePaymentIntentId = invoice.PaymentIntentId;

            var records = _invoices.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} Invoice Records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;

            }

            return retVal;
        }

        public async Task<InvoiceResult> InvoiceCreated(Invoice invoice, string refNo)
        {
            _logger.LogInformation(GetLogMessage("Invoice: {invoice}"), invoice.Id);
            
            var retVal = new InvoiceResult
            {
                InvoiceId = invoice.Id
            };
            
            var entity = new StripeInvoice
            {
                Id = invoice.Id,
                ObjectState = ObjectState.Added,
                Updated = DateTimeOffset.UtcNow,
                AmountDue = Convert.ToDecimal(invoice.AmountDue / 100m),
                AmountRemaining = Convert.ToDecimal(invoice.AmountRemaining / 100m),
                Attempted = invoice.Attempted,
                CustomerId = invoice.CustomerId,
                InvoicePdf = invoice.InvoicePdf,
                Status = invoice.Status,
                SubscriptionId = invoice.SubscriptionId,
                AmountPaid = Convert.ToDecimal(invoice.AmountPaid / 100m),
                Total = Convert.ToDecimal(invoice.Total / 100m),
                Subtotal = Convert.ToDecimal(invoice.Subtotal / 100m),
                HostedInvoiceUrl = invoice.HostedInvoiceUrl,
                BillingReason = invoice.BillingReason,
                AttemptCount = invoice.AttemptCount,
                Number = invoice.Number,
                DueDate = invoice.DueDate
            };


            if (invoice.Metadata.ContainsKey("proj_id"))
            {
                Guid projectId = Guid.Parse(invoice.Metadata["proj_id"]);
                _logger.LogInformation(GetLogMessage("Project Id : {0}"), projectId.ToString());

                var project = await _projectService.Repository.Queryable()
                    .Where(x => x.Id == projectId).FirstAsync();

                retVal.ProjectId = projectId;

                entity.ProjectInvoice = new ProjectInvoice()
                {
                    AccountManagerId = project.AccountManagerId,
                    ProviderOrganizationId = project.ProjectManagerOrganizationId,
                    CustomerId =  project.CustomerId,
                    BuyerOrganizationId = project.CustomerOrganizationId,
                    ProjectManagerId = project.ProjectManagerId,
                    ProjectId = projectId,
                    RefNo = refNo,
                    ObjectState = ObjectState.Added
                };
            }
            else
            {
                _logger.LogDebug(GetLogMessage("Not a project invoice"));
            }

            var records = _invoices.InsertOrUpdateGraph(entity, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                UpdateInvoiceItems(invoice, true);
                retVal.Succeeded = true;
            }

            return await Task.FromResult(retVal);
        }

        private void UpdateInvoiceItems(Invoice invoice, bool commit = false)
        {
            if (invoice.Lines == null || !invoice.Lines.Any()) return;

            _logger.LogInformation(GetLogMessage("Invoice Id : {0}"), invoice.Id);

           _logger.LogInformation(GetLogMessage("Stripe Invoice Item Line : {0}"), invoice.Lines.Select(a => new
            {
                InvoiceId = invoice.Id,
                LineId = a.Id,
                a.InvoiceItemId
            }));

            var invoiceItemIds = invoice.Lines.Select(a => a.InvoiceItemId).ToHashSet();

            var stripeInvoiceItems = _items.Queryable().Where(a => invoiceItemIds.Contains(a.Id) && a.InvoiceId == null).ToList();

            stripeInvoiceItems.ForEach(item =>
            {
                _logger.LogInformation(
                    GetLogMessage("Stripe Invoice Item Update with InvoiceId. Item Id : {0}, Invoice Id : {1}"),
                    item.Id, invoice.Id);
                item.InvoiceId = invoice.Id;
                item.Updated = DateTimeOffset.UtcNow;
                item.ObjectState = ObjectState.Modified;
                _items.InsertOrUpdateGraph(item);
            });

            if (commit)
                _items.Commit();
        }
    }
}