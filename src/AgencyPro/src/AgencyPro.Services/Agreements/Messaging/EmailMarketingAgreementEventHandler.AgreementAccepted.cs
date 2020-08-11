// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Agreements.Emails;
using AgencyPro.Core.Agreements.Events;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Agreements.Messaging
{
    public partial class MultiMarketingAgreementEventHandler
    {
        private void AgreementAcceptedSendMarketingAgencyOwnerEmail(Guid providerOrganizationId,
            Guid marketingOrganizationId)
        {
            _logger.LogInformation(GetLogMessage("Provider: {0}; Marketing: {1}"), providerOrganizationId, marketingOrganizationId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity =context.MarketingAgreements.Where(x =>
                        x.ProviderOrganizationId == providerOrganizationId &&
                        x.MarketingOrganizationId == marketingOrganizationId)
                    .ProjectTo<MarketingAgencyAgreementEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);


                Send(TemplateTypes.MarketingAgencyOwnerAgreementAccepted, entity,
                    $@"[{entity.MarketingOrganizationName} : Marketing] Agreement accepted");
            }

           
        }

        private void AgreementAcceptedSendProviderAgencyOwnerEmail(Guid providerOrganizationId,
            Guid marketingOrganizationId)
        {
            _logger.LogInformation(GetLogMessage("Provider: {0}; Marketing: {1}"), providerOrganizationId, marketingOrganizationId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var entity = context.MarketingAgreements.Where(x =>
                        x.ProviderOrganizationId == providerOrganizationId &&
                        x.MarketingOrganizationId == marketingOrganizationId)
                    .ProjectTo<ProviderMarketingAgreementEmail>(ProjectionMapping)
                    .First();

                entity.Initialize(Settings);

                Send(TemplateTypes.MarketingAgencyOwnerAgreementAccepted, entity,
                    $@"[{entity.ProviderOrganizationName}] Marketing Agreement accepted");
            }

           
        }

        public void Handle(MarketingAgreementAccepted evt)
        {
            _logger.LogInformation(GetLogMessage("Marketing Agreement Accepted Event Triggered"));

            Parallel.Invoke(new List<Action>
            {
                () =>  AgreementAcceptedSendMarketingAgencyOwnerEmail(evt.ProviderOganizationId, evt.MarketingOrganizationId),
                () => AgreementAcceptedSendProviderAgencyOwnerEmail(evt.ProviderOganizationId, evt.MarketingOrganizationId)
            }.ToArray());
        }
    }
}