// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Events;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements
{
    public partial class MarketingAgreementService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MarketingAgreementService)}.{callerName}] - {message}";
        }

        public Task<AgreementResult> CreateAgreement(IProviderAgencyOwner providerAgencyOwner,
            Guid marketingOrganizationId)
        {

            _logger.LogInformation(GetLogMessage("Creating agreement between provider {0} and marketer {1}"), providerAgencyOwner.OrganizationId, marketingOrganizationId);

            var marketingOrganization = UnitOfWork
                .RepositoryAsync<MarketingOrganization>().Queryable()
                .Include(x=>x.MarketingAgreements)
                .FirstOrDefault(x => x.Id == marketingOrganizationId);
            
            if(marketingOrganization == null)
                throw new ApplicationException("Marketing organization not found");

            _logger.LogDebug(GetLogMessage("Marketing organization found: {0}"), marketingOrganization.Id);

            if (marketingOrganization.Discoverable == false)
                throw new ApplicationException("Marketing organization is not accepting offers");

            if (marketingOrganization.MarketingAgreements.Any(x => x.ProviderOrganizationId == providerAgencyOwner.OrganizationId))
                throw new ApplicationException("An existing offer already exists");

            var agreement = new MarketingAgreement()
            {
                ProviderOrganizationId = providerAgencyOwner.OrganizationId,
                MarketingOrganizationId = marketingOrganizationId,
                Status = AgreementStatus.AwaitingApproval,
                ObjectState = ObjectState.Added,
                InitiatedByProvider = true,
                MarketingAgencyStream = marketingOrganization.MarketingAgencyStream,
                MarketerBonus = marketingOrganization.MarketerBonus,
                MarketingAgencyBonus = marketingOrganization.MarketingAgencyBonus,
                MarketerStream = marketingOrganization.MarketerStream
            };

            var result = Repository.InsertOrUpdateGraph(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);

            if (result > 0)
            {
                Task.Run(() => RaiseEvent(new MarketingAgreementCreated()
                {
                    ProviderOganizationId = providerAgencyOwner.OrganizationId,
                    MarketingOrganizationId = marketingOrganizationId
                }));
            }

            return Task.FromResult(new AgreementResult()
            {
                Succeeded = result > 0,

                MarketingOrganizationId = agreement.MarketingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            });
        }

        public async Task<AgreementResult> AcceptAgreement(IProviderAgencyOwner providerAgencyOwner,
            Guid marketingOrganizationId)
        {
            _logger.LogInformation(GetLogMessage("Provider: {0}; Marketer: {1}"),
                providerAgencyOwner.OrganizationId, 
                marketingOrganizationId);

            var retVal = new AgreementResult()
            {
                MarketingOrganizationId = marketingOrganizationId,
                ProviderOrganizationId = providerAgencyOwner.OrganizationId
            };

            var agreement = await Repository.Queryable()
                .FirstOrDefaultAsync(x =>
                    x.ProviderOrganizationId == providerAgencyOwner.OrganizationId &&
                    x.MarketingOrganizationId == marketingOrganizationId);

            if (agreement == null)
            {
                retVal.ErrorMessage = "No agreement found";
                return retVal;
            }

            if (agreement.Status != AgreementStatus.AwaitingApproval)
                return new AgreementResult()
                {
                    Succeeded = false,
                    
                };
            


            agreement.Status = AgreementStatus.Approved;
            agreement.ObjectState = ObjectState.Modified;
            agreement.Updated = DateTimeOffset.UtcNow;

            var result = Repository.InsertOrUpdateGraph(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);

            if (result > 0)
            {
                await Task.Run(() => RaiseEvent(new MarketingAgreementAccepted()
                {
                    MarketingOrganizationId = marketingOrganizationId,
                    ProviderOganizationId = providerAgencyOwner.OrganizationId
                }));
            }

            return new AgreementResult()
            {
                Succeeded = result > 0,
                MarketingOrganizationId = agreement.MarketingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            };
        }
    }
}