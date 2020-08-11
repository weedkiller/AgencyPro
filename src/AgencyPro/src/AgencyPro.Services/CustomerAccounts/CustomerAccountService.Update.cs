// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.CustomerAccounts.Events;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService
    {
        //public async Task<T> Update<T>(IOrganizationAccountManager am, int id, CustomerAccountInput model)
        //    where T : AccountManagerCustomerAccountOutput
        //{
        //    var account = await GetAccount(am, id);
        //    account.InjectFrom(model);

        //    await Repository.UpdateAsync(account, true);

        //    var output = await GetAccount<T>(am, id);

        //    await Task.Run(() =>
        //    {
        //        RaiseEvent(new CustomerAccountUpdatedEvent()
        //        {
        //            Account = output
        //        });
        //    });

        //    return output;
        //}

        public async Task<CustomerAccountResult> Update(IProviderAgencyOwner ao, int id, CustomerAccountInput model)
        {
            var retVal = new  CustomerAccountResult();

            var am = await _accountManagerService
                .Get(model.AccountManagerId, model.AccountManagerOrganizationId);

            var cu = await _customerService
                .Get(model.CustomerId, model.CustomerOrganizationId);

            var isValid = am != null && cu != null;
            if (isValid)
            {
                retVal.CustomerId = cu.CustomerId;
                retVal.AccountManagerId = am.AccountManagerId;
                retVal.CustomerOrganizationId = cu.OrganizationId;
                retVal.AccountManagerOrganizationId = am.OrganizationId;

                var account = await GetAccount(ao, id);
              
                account.InjectFrom(model);

                if (model.PaymentTermId.HasValue)
                    account.PaymentTermId = model.PaymentTermId.GetValueOrDefault(1);

                var result = await Repository.UpdateAsync(account, true);

                _logger.LogDebug(GetLogMessage("{0} records updated"));

                if (result > 0)
                {
                    retVal.Succeeded = true;
                    await Task.Run(() =>
                    {
                        
                        RaiseEvent(new CustomerAccountUpdatedEvent()
                        {
                            CustomerId = retVal.CustomerId.Value,
                            CustomerOrganizationId = retVal.CustomerOrganizationId.Value,
                            AccountManagerId = retVal.AccountManagerId.Value,
                            AccountManagerOrganizationId = retVal.AccountManagerOrganizationId.Value

                        });
                    });
                }
            }

            return retVal;
        }
    }
}