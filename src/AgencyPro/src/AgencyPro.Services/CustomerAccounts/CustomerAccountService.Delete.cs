// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.CustomerAccounts.Events;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService
    {
        public Task<CustomerAccountResult> DeleteAccount(IProviderAgencyOwner agencyOwner, int accountId)
        {
            return DeleteAccount(agencyOwner.OrganizationId, accountId);
        }

        private async Task<CustomerAccountResult> DeleteAccount(Guid providerOrganizationId, int accountId)
        {
            _logger.LogTrace(
                GetLogMessage($@"Deleting account {accountId} from Organization {providerOrganizationId}"));

            var retVal = new CustomerAccountResult();

            var a = await Repository.Queryable()
                .Include(x=>x.Projects)
                .ThenInclude(x=>x.Contracts)
                .ThenInclude(x=>x.TimeEntries)
                .Where(x => x.AccountManagerOrganizationId == providerOrganizationId && x.Number == accountId)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();

            if (a == null) return new CustomerAccountResult()
            {
                Succeeded = false
            };
            
            retVal.InjectFrom(a);

            a.IsDeleted = true;
            a.UpdatedById = _userInfo.UserId;
            a.Updated = DateTimeOffset.UtcNow;
            a.ObjectState = ObjectState.Modified;

            foreach (var p in a.Projects)
            {
                p.UpdatedById = _userInfo.UserId;
                p.IsDeleted = true;
                p.Updated = DateTimeOffset.UtcNow;
                p.ObjectState = ObjectState.Modified;

                foreach (var contract in p.Contracts)
                {
                    contract.Updated = DateTimeOffset.UtcNow;
                    contract.UpdatedById = _userInfo.UserId;
                    contract.IsDeleted = true;
                    contract.ObjectState = ObjectState.Modified;

                    foreach (var e in contract.TimeEntries)
                    {
                        e.Updated = DateTimeOffset.UtcNow;
                        e.UpdatedById = _userInfo.UserId;
                        e.IsDeleted = true;
                        e.ObjectState = ObjectState.Modified;
                    }
                }
            }

            var result =  Repository.InsertOrUpdateGraph(a, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"));
            if (result > 0)
            {
                retVal.Succeeded = true;
                var evt = new CustomerAccountDeletedEvent().InjectFrom(retVal) as CustomerAccountDeletedEvent;
                await Task.Run(() =>
                {
                    RaiseEvent(evt);
                });
            }

            return retVal;
        }
    }
}