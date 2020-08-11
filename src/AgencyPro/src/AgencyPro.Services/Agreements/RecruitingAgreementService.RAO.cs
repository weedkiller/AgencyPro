// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AgencyPro.Services.Agreements
{
    public partial class RecruitingAgreementService
    {
        public async Task<AgreementResult> AcceptAgreement(IRecruitingAgencyOwner principal,
            Guid providerOrganizationId)
        {
            _logger
                .LogInformation(GetLogMessage("Accepting Agreement: Recruiting Agency {1}, Provider Agency {2}"), principal.OrganizationId, providerOrganizationId);

            var agreement = await Repository.Queryable()
                .FirstOrDefaultAsync(x =>
                    x.RecruitingOrganizationId == principal.OrganizationId &&
                    x.ProviderOrganizationId == providerOrganizationId);

            
            if (agreement == null)
                throw new ApplicationException("Agreement not found");
            ;
            if (agreement.Status != AgreementStatus.AwaitingApproval)
                return new AgreementResult()
                {
                    Succeeded = false
                };

            agreement.Status = AgreementStatus.Approved;
            agreement.ObjectState = ObjectState.Modified;
            agreement.Updated = DateTimeOffset.UtcNow;

            var result = await Repository.UpdateAsync(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated in database"), result);

            return new AgreementResult()
            {
                Succeeded = result > 0,
                RecruitingOrganizationId = agreement.RecruitingOrganizationId,
                ProviderOrganizationId = agreement.ProviderOrganizationId
            };
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(RecruitingAgreementService)}.{callerName}] - {message}";
        }

        public async Task<AgreementResult> CreateAgreement(IRecruitingAgencyOwner principal,
            Guid providerOrganizationId, RecruitingAgreementInput input)
        {
            _logger.LogInformation(GetLogMessage("Agency Owner creating recruiting agreement {@agreement}"), input);

            var retVal = new AgreementResult()
            {
                ProviderOrganizationId = providerOrganizationId,
                RecruitingOrganizationId = principal.OrganizationId
            };

            var recruiterOrganization = await _recruitingOrganizations.Queryable()
                .Where(x => x.Id == principal.OrganizationId)
                .FirstOrDefaultAsync();

            if (recruiterOrganization == null)
            {
                retVal.ErrorMessage = "Organization is not configured correctly";
                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Recruiter Organization Found: {0}"), recruiterOrganization.Id);
            
            var providerOrganization = await _providerOrganizations.Queryable()
                .Where(x => x.Id == providerOrganizationId)
                .FirstOrDefaultAsync();

            if (providerOrganization == null)
                throw new ApplicationException("Provider organization was not found");

            _logger.LogDebug(GetLogMessage("Provider Organization Found: {0}"), providerOrganization.Id);

            var agreement = new RecruitingAgreement()
            {
                RecruiterStream = recruiterOrganization.RecruiterStream,
                RecruiterBonus = recruiterOrganization.RecruiterBonus,
                RecruitingAgencyBonus = recruiterOrganization.RecruitingAgencyBonus,
                RecruitingAgencyStream = recruiterOrganization.RecruitingAgencyStream,
                RecruitingOrganizationId = principal.OrganizationId,
                ProviderOrganizationId = providerOrganizationId,
                Status = AgreementStatus.AwaitingApproval,
                ObjectState = ObjectState.Added,
                InitiatedByProvider = false
            };

            _logger.LogDebug(GetLogMessage("Recruiting Agreement: {@agreement}"), agreement);


            var result = Repository.InsertOrUpdateGraph(agreement, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated in database"), result);


            return await Task.FromResult(new AgreementResult()
            {
                Succeeded = result > 0,
                ProviderOrganizationId = agreement.ProviderOrganizationId,
                RecruitingOrganizationId = agreement.RecruitingOrganizationId
            });
        }
    }

}