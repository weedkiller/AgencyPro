// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Agreements
{
    public partial class RecruitingAgreementService
    {
        public async Task<AgreementResult> AcceptAgreement(IProviderAgencyOwner principal,
            Guid recruitingOrganizationId)
        {

            _logger.LogInformation(GetLogMessage("Accepting agreement as Provider Agency Owner: {0}"), principal.OrganizationId);

            var agreement = await Repository.Queryable()
                .FirstOrDefaultAsync(x =>
                    x.ProviderOrganizationId == principal.OrganizationId &&
                    x.RecruitingOrganizationId == recruitingOrganizationId);

            if (agreement.Status != AgreementStatus.AwaitingApproval)
            {
                _logger.LogDebug(GetLogMessage("Agreement cannot be approved while in status: {0}"), agreement.Status);

                return new AgreementResult()
                {
                    Succeeded = false,
                };
            }

            agreement.Status = AgreementStatus.Approved;
            agreement.ObjectState = ObjectState.Modified;
            agreement.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);


            return new AgreementResult()
            {
                Succeeded = result > 0,
                RecruitingOrganizationId = agreement.RecruitingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            };
        }
   
        public Task<AgreementResult> CreateAgreement(IProviderAgencyOwner principal,
            Guid recruitingOrganizationId, RecruitingAgreementInput input)
        {
            _logger.LogInformation(GetLogMessage("Creating agreement as Provider Agency Owner: {0}"), principal.OrganizationId);

            return CreateAgreement(principal, recruitingOrganizationId);
        }

        public Task<AgreementResult> CreateAgreement(IProviderAgencyOwner providerAgencyOwner, Guid recruitingOrganizationId)
        {

            _logger.LogInformation(GetLogMessage("Creating agreement between provider {0} and recruiter {1}"), providerAgencyOwner.OrganizationId, recruitingOrganizationId);

            var recruitingOrganization = UnitOfWork
              .RepositoryAsync<RecruitingOrganization>().Queryable()
              .Include(x => x.RecruitingAgreements)
              .FirstOrDefault(x => x.Id == recruitingOrganizationId);
            
            if (recruitingOrganization == null)
                throw new ApplicationException("Recruiting organization not found");

            _logger.LogDebug(GetLogMessage("Recruiting organization found: {0}"), recruitingOrganization.Id);

            if (recruitingOrganization.Discoverable == false)
                throw new ApplicationException("Recruiting organization is not accepting offers");

            if (recruitingOrganization.RecruitingAgreements.Any(x => x.ProviderOrganizationId == providerAgencyOwner.OrganizationId))
                throw new ApplicationException("An existing offer already exists");

            var agreement = new RecruitingAgreement()
            {
                ProviderOrganizationId = providerAgencyOwner.OrganizationId,
                RecruitingOrganizationId = recruitingOrganizationId,
                Status = AgreementStatus.AwaitingApproval,
                Created = DateTimeOffset.UtcNow,
                ObjectState = ObjectState.Added,
                InitiatedByProvider = true,
                RecruitingAgencyStream = recruitingOrganization.RecruitingAgencyStream,
                RecruitingAgencyBonus = recruitingOrganization.RecruitingAgencyBonus,
                RecruiterBonus = recruitingOrganization.RecruiterBonus,
                RecruiterStream = recruitingOrganization.RecruiterStream
            };

            var result = Repository.InsertOrUpdateGraph(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in database"), result);

            return Task.FromResult(new AgreementResult()
            {
                Succeeded = result > 0,

                RecruitingOrganizationId = agreement.RecruitingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            });
        }
    }
}