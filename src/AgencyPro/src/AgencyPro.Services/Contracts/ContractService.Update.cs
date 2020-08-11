// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.Contracts.Extensions;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Threading.Tasks;

namespace AgencyPro.Services.Contracts
{
    public partial class ContractService
    {
       

        public Task<ContractResult> UpdateContract(IOrganizationAccountManager am, Guid contractId, UpdateProviderContractInput input)
        {
            return UpdateContract(contractId, input);
        }

        public async Task<ContractResult> UpdateContract(IProviderAgencyOwner ao, Guid contractId, UpdateProviderContractInput input)
        {
            return await UpdateContract(contractId, input);
        }
        

        public async Task<ContractResult> UpdateContract(IOrganizationContractor co, Guid contractId, UpdateProviderContractInput input)
        {
            return await UpdateContract(contractId, input);
        }
        
        
        private async Task<ContractResult> UpdateContract(Contract co)
        {
            _logger.LogInformation(GetLogMessage($@"For Contract: {co.Id}"));

            var retVal = new ContractResult()
            {
                ContractId = co.Id
            };

            co.Updated = DateTimeOffset.UtcNow;
            co.UpdatedById = _userInfo.UserId;

            var result = await Repository.UpdateAsync(co, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new ContractUpdatedEvent()
                    {
                        ContractId = co.Id
                    });
                });
            }
           

            return retVal;
        }

        private async Task<ContractResult> UpdateContract(Guid id, UpdateProviderContractInput input)
        {
            var co = await Repository.Queryable()
                .FirstAsync(x => x.Id == id);

            co.InjectFrom<NullableInjection>(input);
            if(input.ContractorStream.HasValue)
                co.ContractorStream = input.ContractorStream.Value;

            if (input.ProjectManagerStream.HasValue)
                co.ProjectManagerStream = input.ProjectManagerStream.Value;

            if (input.AccountManagerStream.HasValue)
                co.AccountManagerStream = input.AccountManagerStream.Value;

            if (input.AgencyStream.HasValue)
                co.AgencyStream = input.AgencyStream.Value;
            
            co.MaxWeeklyHours = input.MaxWeeklyHours;
         

            return await UpdateContract(co);
        }

        public async Task<ContractResult> UpdateContract(IMarketingAgencyOwner ao, Guid contractId, UpdateMarketingContractInput input)
        {
            var co = await Repository.Queryable().ForAgencyOwner(ao)
                .FirstAsync(x => x.Id == contractId);

            co.InjectFrom<NullableInjection>(input);
         
            if (input.MarketerStream.HasValue)
                co.MarketerStream = input.MarketerStream.Value;

            if (input.MarketingAgencyStream.HasValue)
                co.MarketingAgencyStream = input.MarketingAgencyStream.Value;
            
            return await UpdateContract(co);
        }

        public async Task<ContractResult> UpdateContract(IRecruitingAgencyOwner ao, Guid contractId, UpdateRecruitingContractInput input)
        {
            var co = await Repository.Queryable().ForAgencyOwner(ao)
                .FirstAsync(x => x.Id == contractId);

            co.InjectFrom<NullableInjection>(input);

            if (input.RecruiterStream.HasValue)
                co.MarketerStream = input.RecruiterStream.Value;

            if (input.RecruitingAgencyStream.HasValue)
                co.MarketingAgencyStream = input.RecruitingAgencyStream.Value;


            return await UpdateContract(co);
        }

        public async Task<ContractResult> UpdateContract(IOrganizationCustomer cu, Guid contractId, UpdateBuyerContractInput input)
        {
            var co = await Repository.Queryable()
                .FirstAsync(x => x.Id == contractId);

            co.InjectFrom<NullableInjection>(input);

            co.MaxWeeklyHours = input.MaxWeeklyHours;
            

            return await UpdateContract(co);
        }
    }
}