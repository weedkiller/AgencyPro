// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements
{
    public partial class MarketingAgreementService
    {
        public async Task<AgreementResult> CreateAgreement(IMarketingAgencyOwner marketingAgencyOwner,
            Guid providerOrganizationId)
        {
            var retVal = new AgreementResult()
            {
                ProviderOrganizationId = providerOrganizationId,
                MarketingOrganizationId = marketingAgencyOwner.OrganizationId
            };

            var marketingOrganization = await _marketingOrganizations.Queryable()
                .Where(x => x.Id == marketingAgencyOwner.OrganizationId)
                .FirstOrDefaultAsync();

            if (marketingOrganization == null)
            {
                retVal.ErrorMessage = "Organization is not configured correctly";
                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Marketing Organization Found: {0}"), marketingOrganization.Id);

            var providerOrganization = await _providerOrganization.Queryable()
                .Where(x => x.Id == providerOrganizationId)
                .FirstOrDefaultAsync();

            if (providerOrganization == null)
                throw new ApplicationException("Provider organization was not found");

            _logger.LogDebug(GetLogMessage("Provider Organization Found: {0}"), providerOrganization.Id);

            var agreement = new MarketingAgreement
            {
                MarketingStream = marketingOrganization.MarketerStream,
                MarketingBonus = marketingOrganization.MarketerBonus,
                MarketingAgencyBonus = marketingOrganization.MarketingAgencyBonus,
                MarketingAgencyStream = marketingOrganization.MarketingAgencyStream,
                MarketingOrganizationId = marketingAgencyOwner.OrganizationId,
                ProviderOrganizationId = providerOrganizationId,
                Status = AgreementStatus.AwaitingApproval,
                ObjectState = ObjectState.Added,
                InitiatedByProvider = false,
                Created = DateTimeOffset.UtcNow
            };

            _logger.LogDebug(GetLogMessage("Marketing Agreement: {@agreement}"), agreement);

            var result = Repository.InsertOrUpdateGraph(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated in database"), result);

            return await Task.FromResult(new AgreementResult()
            {
                Succeeded = result > 0,
                MarketingOrganizationId = agreement.MarketingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            });
        }

        public async Task<AgreementResult> AcceptAgreement(
            IMarketingAgencyOwner marketingAgencyOwner, Guid providerOrganizationId)
        {
            var agreement = await Repository.Queryable()
                .FirstOrDefaultAsync(x =>
                    x.MarketingOrganizationId == marketingAgencyOwner.OrganizationId &&
                    x.ProviderOrganizationId == providerOrganizationId);

            if (agreement == null)
                throw new ApplicationException("Agreement not found.");

            if (agreement.Status != AgreementStatus.AwaitingApproval)
                return new AgreementResult()
                {
                    Succeeded = false
                };

            agreement.Status = AgreementStatus.Approved;
            agreement.ObjectState = ObjectState.Modified;
            agreement.Updated = DateTimeOffset.UtcNow;

            var result =await Repository.UpdateAsync(agreement, true);

            return new AgreementResult()
            {
                Succeeded = result > 0,
                MarketingOrganizationId = agreement.MarketingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            };
        }
    }
}