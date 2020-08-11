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
        private async Task<ContractResult> RestartContract(Contract contract)
        {
            _logger.LogInformation(GetLogMessage($@"For Contract: {contract.Id}"));

            var retValue = new ContractResult
            {
                ContractId = contract.Id
            };

            if (contract.Status != ContractStatus.Paused)
            {
                retValue.ErrorMessage = "You can only restart a contract that has been paused";
                return retValue;
            }

            _logger.LogDebug(GetLogMessage("Contract is ready to be restarted"));

            contract.ObjectState = ObjectState.Modified;
            contract.ContractorPauseDate = null;
            contract.ContractorEndDate = null;
            contract.AccountManagerEndDate = null;
            contract.AccountManagerPauseDate = null;
            contract.CustomerEndDate = null;
            contract.CustomerPauseDate = null;
            contract.AgencyOwnerEndDate = null;
            contract.AgencyOwnerPauseDate = null;
            contract.CustomerPauseDate = null;
            contract.CustomerEndDate = null;
            contract.Status = ContractStatus.Active;

            contract.Updated = DateTimeOffset.UtcNow;
            contract.UpdatedById = _userInfo.UserId;

            contract.StatusTransitions.Add(new ContractStatusTransition()
            {
                Status = contract.Status,
                ObjectState = ObjectState.Added
            });

            var records =  Repository.InsertOrUpdateGraph(contract, true);

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retValue.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new ContractRestartedEvent()
                    {
                        ContractId = contract.Id
                    });
                });
            }
           

            return retValue;

        }

        public async Task<ContractResult> RestartContract(IOrganizationCustomer cu, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("CU: {0}; Contract: {1}"), cu.OrganizationId, contractId);
            var contract = await Repository.Queryable().ForOrganizationCustomer(cu)
                .FindById(contractId)
                .FirstAsync();
          
            return await RestartContract(contract);
        }

        public async Task<ContractResult> RestartContract(IOrganizationContractor co, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("CO: {0}; Contract: {1}"), co.OrganizationId, contractId);

            var contract = await Repository.Queryable().ForOrganizationContractor(co)
                .FindById(contractId)
                .FirstAsync();

            return await RestartContract(contract);
        }

        public async Task<ContractResult> RestartContract(IProviderAgencyOwner ao, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("AO: {0}; Contract: {1}"), ao.OrganizationId, contractId);

            var contract = await Repository.Queryable().ForAgencyOwner(ao)
                .FindById(contractId)
                .FirstAsync();

            return await RestartContract(contract);
        }

        public async Task<ContractResult> RestartContract(IOrganizationAccountManager am, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Contract: {1}"), am.OrganizationId, contractId);

            var contract = await Repository
                .Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            return await RestartContract(contract);
        }
    }
}