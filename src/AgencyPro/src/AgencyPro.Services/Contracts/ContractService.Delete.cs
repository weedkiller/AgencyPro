// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Extensions;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.Contracts.ViewModels;

namespace AgencyPro.Services.Contracts
{
    public partial class ContractService
    {
        public async Task<ContractResult> DeleteContract(IProviderAgencyOwner agencyOwner, Guid contractId)
        {
            _logger.LogInformation(GetLogMessage($@"Deleting Contract: {contractId}"));
            var retVal = new ContractResult
            {
                ContractId = contractId
            };

            var c = await Repository
                .Queryable()
                .Include(x=>x.TimeEntries)
                .ForAgencyOwner(agencyOwner)
                .FindById(contractId)
                .FirstOrDefaultAsync();

            if (c.TimeEntries.Any())
            {
                foreach (var entry in c.TimeEntries)
                {
                    entry.IsDeleted = true;
                    entry.Updated = DateTimeOffset.UtcNow;
                    entry.ObjectState = ObjectState.Modified;
                }
            }

            c.IsDeleted = true;
            c.Updated = DateTimeOffset.UtcNow;
            c.UpdatedById = _userInfo.UserId;
            c.ObjectState = ObjectState.Modified;


            int result =  Repository.InsertOrUpdateGraph(c, true);
            _logger.LogDebug(GetLogMessage("{0} results updated"), result);

            if (result > 0)
            {
                retVal.Succeeded = true;
                await Task.Run(() =>
                {
                    RaiseEvent(new ContractDeletedEvent()
                    {
                        ContractId = c.Id
                    });
                });

            }

            return retVal;
        }
    }
}