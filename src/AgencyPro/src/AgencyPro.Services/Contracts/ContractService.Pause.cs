// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.Contracts.Extensions;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Contracts
{
    public partial class ContractService
    {
        private async Task<ContractResult> PauseContract(Contract contract)
        {
            _logger.LogInformation(GetLogMessage($@"For Contract: {contract.Id}"));
            var retVal = new ContractResult()
            {
                ContractId = contract.Id
            };
            if (contract.Status != ContractStatus.Paused)
            {
                _logger.LogDebug(GetLogMessage("Contract ready to be paused"));

                contract.Updated = DateTimeOffset.UtcNow;
                contract.UpdatedById = _userInfo.UserId;
                contract.Status = ContractStatus.Paused;
                contract.ObjectState = ObjectState.Modified;

                contract.StatusTransitions.Add(new ContractStatusTransition()
                {
                    Status = contract.Status,
                    ObjectState = ObjectState.Added
                });
                
                var records = Repository.InsertOrUpdateGraph(contract, true);

                _logger.LogDebug(GetLogMessage("{0} records updated"), records);

                await Task.Run(() =>
                {
                    retVal.Succeeded = true;
                    RaiseEvent(new ContractPausedEvent()
                    {
                        ContractId = contract.Id
                    });
                });

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Contract is already paused"));
            }

            return retVal;
        }

        public async Task<ContractResult> PauseContract(IOrganizationCustomer cu, Guid contractId) 
        {
            _logger.LogInformation(GetLogMessage("Cu: {0}; Contract: {1}"), cu.OrganizationId, contractId);

            var contract = await Repository.Queryable().ForOrganizationCustomer(cu)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.CustomerPauseDate = DateTimeOffset.UtcNow;

            return await PauseContract(contract);
        }

        public async Task<ContractResult> PauseContract(IOrganizationContractor co, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Co: {0}; Contract: {1}"), co.OrganizationId, contractId);

            var contract = await Repository
                .Queryable()
                .ForOrganizationContractor(co)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.ContractorPauseDate = DateTimeOffset.UtcNow;

            return await PauseContract(contract);
        }

        public async Task<ContractResult> PauseContract(IProviderAgencyOwner ao, Guid contractId)
        {

            _logger.LogInformation(GetLogMessage("Ao: {0}; Contract: {1}"), ao.OrganizationId, contractId);

            var contract = await Repository.Queryable()
                .ForAgencyOwner(ao)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.AgencyOwnerPauseDate = DateTimeOffset.UtcNow;

            return await PauseContract(contract);
        }

        public async Task<ContractResult> PauseContract(IOrganizationAccountManager am, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Contract: {1}"), am.OrganizationId, contractId);

            var contract = await Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.AccountManagerPauseDate = DateTimeOffset.UtcNow;

            return await PauseContract(contract);
        }
    }
}