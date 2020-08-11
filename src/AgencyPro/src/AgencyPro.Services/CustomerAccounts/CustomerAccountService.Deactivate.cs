// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.CustomerAccounts.Extensions;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService
    {
        private Task<CustomerAccountResult> Deactivate(CustomerAccount account)
        {
            _logger.LogInformation(GetLogMessage("Acct: {0}"), account.Number);
            var retVal = new CustomerAccountResult
            {
                AccountManagerId = account.AccountManagerId,
                AccountManagerOrganizationId = account.AccountManagerOrganizationId,
                CustomerId = account.CustomerId,
                CustomerOrganizationId = account.CustomerOrganizationId,
                Number = account.Number,
                BuyerNumber = account.BuyerNumber
            };

            account.ObjectState = ObjectState.Modified;
            account.UpdatedById = _userInfo.UserId;
            account.Updated = DateTimeOffset.UtcNow;
            account.AccountStatus = AccountStatus.Inactive;

            account.StatusTransitions.Add(new CustomerAccountStatusTransition()
            {
                Status = account.AccountStatus,
                ObjectState = ObjectState.Added
            });

            var records = Repository.InsertOrUpdateGraph(account, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"));
            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return Task.FromResult(retVal);
        }

        public async Task<CustomerAccountResult> Deactivate(IOrganizationAccountManager am, int number)
        {
            _logger.LogInformation(GetLogMessage("AM:{0};Number:{1}"), am.OrganizationId, number);
            
            var acct = await Repository.Queryable().ForOrganizationAccountManager(am)
                .Where(x => x.Number == number)
                .FirstOrDefaultAsync();

            acct.AccountManagerDeactivationDate = DateTimeOffset.UtcNow;

            return await Deactivate(acct);
        }

        public async Task<CustomerAccountResult> Deactivate(IProviderAgencyOwner ao, int number)
        {
            var acct = await Repository.Queryable().ForAgencyOwner(ao)
                .Where(x => x.Number == number)
                .FirstOrDefaultAsync();

            acct.AgencyOwnerDeactivationDate = DateTimeOffset.UtcNow;
            
            return await Deactivate(acct);
        }

        public async Task<CustomerAccountResult> Deactivate(IOrganizationCustomer cu, Guid accountManagerOrganizationId, int number) 
        {
            var acct = await Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .Where(x => x.Number == number && x.AccountManagerOrganizationId == accountManagerOrganizationId)
                .FirstOrDefaultAsync();

            acct.CustomerDeactivationDate = DateTimeOffset.UtcNow;
          
            return await Deactivate(acct);
        }
    }
}