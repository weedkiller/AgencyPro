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
        private async Task<ContractResult> EndContract(Contract contract)
        {
            _logger.LogInformation(GetLogMessage($@"For Contract: {contract.Id}"));
            var retVal = new ContractResult()
            {
                ContractId = contract.Id
            };
            if (contract.Status != ContractStatus.Ended)
            {
                _logger.LogDebug(GetLogMessage("Contract ready to be ended"));

                contract.Updated = DateTimeOffset.UtcNow;
                contract.UpdatedById = _userInfo.UserId;
                contract.Status = ContractStatus.Ended;
                contract.ObjectState = ObjectState.Modified;

                contract.StatusTransitions.Add(new ContractStatusTransition()
                {
                    Status = contract.Status,
                    ObjectState = ObjectState.Added
                });

                var records = Repository.InsertOrUpdateGraph(contract, true);

                _logger.LogDebug(GetLogMessage("{0} records updated"), records);

                if (records > 0)
                {
                    retVal.Succeeded = true;
                    retVal.ContractId = contract.Id;
                    await Task.Run(() =>
                    {
                        RaiseEvent(new ContractEndedEvent()
                        {
                            ContractId = contract.Id
                        });
                    });

                }
            }

            return retVal;
        }

        public async Task<ContractResult> EndContract(IOrganizationCustomer cu, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage($@"Ending Contract: {contractId}"));

            var contract = await Repository.Queryable()
                .Include(x=>x.TimeEntries)
                .ForOrganizationCustomer(cu)
                .FindById(contractId)
                .FirstOrDefaultAsync();
            contract.CustomerEndDate = DateTimeOffset.UtcNow;
            return await EndContract(contract);
        }

        public async Task<ContractResult> EndContract(IOrganizationContractor co, Guid contractId) 
        {
            _logger.LogTrace(GetLogMessage($@"Ending Contract: {contractId}"));

            var contract = await Repository.Queryable()
                .Include(x => x.TimeEntries)
                .ForOrganizationContractor(co)
                .FindById(contractId)
                .FirstOrDefaultAsync();
            contract.ContractorEndDate = DateTimeOffset.UtcNow;

            return await EndContract(contract);
        }

        public async Task<ContractResult> EndContract(IProviderAgencyOwner ao, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage($@"Ending Contract: {contractId}"));

            var contract = await Repository.Queryable().Include(x => x.TimeEntries)
                .ForAgencyOwner(ao)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.AgencyOwnerEndDate = DateTimeOffset.UtcNow;
            return await EndContract(contract);
        }

        public async Task<ContractResult> EndContract(IOrganizationAccountManager am, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage($@"Ending Contract: {contractId}"));

            var contract = await Repository.Queryable()
                .Include(x=>x.TimeEntries)
                .ForOrganizationAccountManager(am)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            contract.AccountManagerEndDate = DateTimeOffset.UtcNow;
            return await EndContract(contract);

        }
    }
}