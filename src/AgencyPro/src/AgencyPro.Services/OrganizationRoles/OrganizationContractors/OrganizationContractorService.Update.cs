// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationContractors
{
    public partial class OrganizationContractorService
    {
        public Task<T> Update<T>(IAgencyOwner ao, OrganizationContractorInput input)
            where T : OrganizationContractorOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Update<T>(input);
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationContractorInput input) where T : OrganizationContractorOutput
        {
            input.OrganizationId = am.OrganizationId;
            return Update<T>(input);
        }
        
        private async Task<T> Update<T>(OrganizationContractorInput input)
            where T : OrganizationContractorOutput
        {
            _logger.LogTrace(
                GetLogMessage(
                    $@"OrganizationId: {input.OrganizationId}, ContractorId: {input.ContractorId}"));

            var entity = await Repository.Queryable()
                .FindById(input.ContractorId, input.OrganizationId)
                .FirstAsync();

            entity.InjectFrom(input);
            entity.Updated = DateTimeOffset.UtcNow;
            

            await Repository.UpdateAsync(entity, true);

            var result = await GetById<T>(input.ContractorId, input.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationContractorUpdatedEvent {OrganizationContractor = result});
            });

            return result;
        }

        public async Task<T> UpdateRecruiter<T>(IAgencyOwner agencyOwner, Guid contractorId,  UpdateContractorRecruiterInput input) where T : AgencyOwnerOrganizationContractorOutput
        {
            var entity = await Repository.Queryable()
                .Include(x=>x.Contractor)
                .ThenInclude(x=>x.OrganizationContractors)
                .ThenInclude(x=>x.Contracts)
                .Include(x => x.Contractor)
                .ThenInclude(x=>x.OrganizationRecruiter)
                .FindById(contractorId, agencyOwner.OrganizationId)
                .FirstAsync();

            var recruiter = await _recuiterRepository.Queryable()
                .FirstAsync(x => x.RecruiterId == input.RecruiterId && x.OrganizationId == agencyOwner.OrganizationId);

            entity.Contractor.OrganizationRecruiter = recruiter;
            entity.Contractor.ObjectState = ObjectState.Modified;
            entity.Contractor.Updated = DateTimeOffset.UtcNow;

            if (input.UpdateAllContracts)
            {
                foreach (var contract in entity.Contracts)
                {
                    contract.OrganizationRecruiter = recruiter;
                    contract.UpdatedById = _userInfo.UserId;
                    contract.Updated = DateTimeOffset.UtcNow;
                    contract.ObjectState = ObjectState.Modified;
                    
                }
            }

            Repository.InsertOrUpdateGraph(entity, true);

            var response = await Repository.Queryable()
                .FindById(contractorId, agencyOwner.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return response;
        }
    }
}