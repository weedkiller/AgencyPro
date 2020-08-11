// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.DisperseFunds.Emails;
using AgencyPro.Core.DisperseFunds.Events;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using AgencyPro.Services.Messaging.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.DisperseFunds.EventHandlers
{
    public class FundsDispersalEventHandlers : MultiEventHandler<FundsDispersedEvent>, 
        IEventHandler<FundsDispersedEvent>,
        IEventHandler<FundsDispersedToPersonEvent>,
        IEventHandler<FundsDispersedToOrganizationEvent>
    {
        private readonly ILogger<FundsDispersalEventHandlers> _logger;

        public FundsDispersalEventHandlers(
            ILogger<FundsDispersalEventHandlers> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(FundsDispersalEventHandlers)}.{callerName}] - {message}";
        }

        public void Handle(FundsDispersedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Funds dispersed: {@evt}"), evt);
        }

        public void Handle(FundsDispersedToPersonEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Person Id: {person}; Amount: {amount}"), evt.PersonId, evt.Amount);

            Parallel.Invoke(() =>
            {
                FundsDispersedToPersonEmail(evt);
            });

            //using (var context = new AppDbContext(DbContextOptions))
            //{
                //var contract = context.Invoices
                //    .Include(x=>x.Invoice)
                //    .ThenInclude(x=>x.Items)
                //    .ThenInclude(x=>x.IndividualPayoutIntents)
                //    .Where(x => x.InvoiceId == evt.InvoiceId)
                //    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                //    .First();

                //contract.Initialize(Settings);

                //Send(TemplateTypes.ContractorContractApproved, contract,
                //    $@"[{contract.ProjectManagerOrganizationName} : Contracting] A new contract has been approved");

                //AddContractNotification(context, "Contract was approved", contract);

                //var invoice = context.InvoiceTransfers
                //    .Include(x => x.Invoice)
                //    .ThenInclude(x => x.Items)
                //    .ThenInclude(x => x.IndividualPayoutIntents)
                //    .Where(x => x.InvoiceId == evt.InvoiceId)
                //    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                //    .First();

                //invoice.Initialize();

            //}

        }

        private void FundsDispersedToPersonEmail(FundsDispersedToPersonEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Person Id: {person}; InvoiceId: {invoice}; Amount: {amount}"), evt.PersonId, evt.InvoiceId, evt.Amount);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var person = context.UsersAccounts.Where(a => a.Id == evt.PersonId)
                    .Include(a => a.Person).First();

                var invoice = context.Invoices
                    .Where(a => a.InvoiceId == evt.InvoiceId)
                    .Include(a => a.ProviderOrganization)
                    .Include(a => a.Invoice).Include(a => a.Project)
                    .Include(a => a.OrganizationProjectManager).ThenInclude(a=>a.Organization)
                    .First();

                var personTransferEmail = new PersonTransferEmail
                {
                    RecipientEmail = person.Email,
                    RecipientName = person.Person.FirstName,
                    SendMail = person.SendMail,
                    Amount = evt.Amount,
                    Number = invoice.Invoice.Number,
                    FlowUrl = Settings.Urls.Flow,
                    ProjectName = invoice.Project.Name,
                    ProviderOrganizationName = invoice.ProviderOrganization.Name,
                    ProjectManagerOrganizationName = invoice.OrganizationProjectManager.Organization.Name
                };

                Send(TemplateTypes.AgencyOwnerInvoiceDispersedIndividual, personTransferEmail,
                    $@"[{personTransferEmail.ProviderOrganizationName}] - Payout Notification");
            }
        }

        public void Handle(FundsDispersedToOrganizationEvent evt)
        {
            Parallel.Invoke(() =>
            {
                FundsDispersedToOrganizationEmail(evt);
            });
        }

        private void FundsDispersedToOrganizationEmail(FundsDispersedToOrganizationEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Organization Id: {organization}; InvoiceId: {invoice}; Amount: {amount}"), evt.OrganizationId, evt.InvoiceId, evt.Amount);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var organization = context.Organizations.Where(a => a.Id == evt.OrganizationId)
                    .Include(a => a.Customer).ThenInclude(a => a.Person).ThenInclude(a => a.ApplicationUser)
                    .First();

                var invoice = context.Invoices
                    .Where(a => a.InvoiceId == evt.InvoiceId)
                    .Include(a => a.ProviderOrganization)
                    .Include(a => a.Invoice).Include(a => a.Project)
                    .Include(a => a.OrganizationProjectManager).ThenInclude(a => a.Organization)
                    .First();

                var organizationTransferEmail = new OrganizationTransferEmail
                {
                    RecipientEmail = organization.Customer.Person.ApplicationUser.Email,
                    RecipientName = organization.Customer.Person.FirstName,
                    SendMail = organization.Customer.Person.ApplicationUser.SendMail,
                    Amount = evt.Amount,
                    FlowUrl = Settings.Urls.Flow,
                    Number = invoice.Invoice.Number,
                    ProjectName = invoice.Project.Name,
                    OrganizationName = organization.Name,
                    ProviderOrganizationName = invoice.ProviderOrganization.Name,
                    ProjectManagerOrganizationName = invoice.OrganizationProjectManager.Organization.Name
                };

                Send(TemplateTypes.AgencyOwnerInvoiceDispersedOrganization, organizationTransferEmail,
                    $@"[{organizationTransferEmail.ProviderOrganizationName}] - Payout Notification");
            }
        }
    }
}
