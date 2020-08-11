// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.CustomerAccounts.Events;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService
    {
        public async Task<CustomerAccountResult> LinkOrganizationCustomer(IOrganizationAccountManager am, LinkCustomerWithCompanyInput input)
        {
            input.AccountManagerId = am.AccountManagerId;
            input.AccountManagerOrganizationId = am.OrganizationId;

            return await Link(input);
        }

        public async Task<CustomerAccountResult> LinkOrganizationCustomer(IOrganizationAccountManager am, LinkCustomerInput input)
        {
            input.AccountManagerId = am.AccountManagerId;
            input.AccountManagerOrganizationId = am.OrganizationId;

            return await Link(input);
        }

        public async Task<CustomerAccountResult> LinkOrganizationCustomer(IProviderAgencyOwner ao, LinkCustomerInput input)
        {
            input.AccountManagerOrganizationId = ao.OrganizationId;
            return await Link(input);
        }

        public async Task<CustomerAccountResult> LinkOrganizationCustomer(IProviderAgencyOwner ao, LinkCustomerWithCompanyInput input)
        {
            input.AccountManagerOrganizationId = ao.OrganizationId;
            return await Link(input);
        }

        public async Task<CustomerAccountResult> LinkOrganizationCustomer(IOrganizationCustomer customer)
        {
            _logger.LogInformation(GetLogMessage("Customer: {0}; Organization: {1}"), customer.CustomerId, customer.OrganizationId);
            var retVal = new CustomerAccountResult()
            {
                CustomerId = customer.CustomerId,
                CustomerOrganizationId = customer.OrganizationId
            };

            var customerOrganization = await _organizations
                .Queryable()
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.OrganizationMarketer)
                .ThenInclude(x=>x.Organization)
                .ThenInclude(x=>x.ProviderOrganization)
                .FirstAsync();

            var agencyOrganization = customerOrganization.Customer.OrganizationMarketer.Organization;

            _logger.LogDebug(GetLogMessage("Agency Organization Id: {0}"), agencyOrganization.Id);
            if (agencyOrganization.ProviderOrganization != null)
            {
                _logger.LogDebug(GetLogMessage("Provider Agency found"));

                var c = _customers
                    .Queryable().Include(x => x.Person).ThenInclude(x => x.ApplicationUser)
                    .First(x => x.Id == customer.CustomerId);

                retVal.AccountManagerOrganizationId = agencyOrganization.Id;
                retVal.AccountManagerId = agencyOrganization.ProviderOrganization.DefaultAccountManagerId;
                
                var input = new LinkCustomerInput()
                {
                    AccountManagerId = agencyOrganization.ProviderOrganization.DefaultAccountManagerId,
                    AccountManagerOrganizationId =agencyOrganization.Id,
                    EmailAddress = c.Person.ApplicationUser.Email,
                };

                _logger.LogDebug(GetLogMessage("Linking {@input}"), input);
                
                retVal = await Link(input);

                _logger.LogDebug(GetLogMessage("Response {@retVal}"), retVal);
            }
            else
            {
                retVal.Succeeded = true;
                _logger.LogDebug(GetLogMessage("Referrer Provider Organization not found," +
                                               " it shouldn't happen but it's not the end of the world"));
            }

            return retVal;

        }

        private async Task<CustomerAccountResult> Link(LinkCustomerWithCompanyInput input)
        {
            _logger.LogInformation(GetLogMessage(
                $@"Linking Account, Account Manager: {input.AccountManagerId}, Customer Lookup Info: {input.EmailAddress} ({input.CompanyName})"));

            CustomerAccountResult retVal = new CustomerAccountResult();


            var cu = _customerService.Repository.Queryable()
                .FindByOrganizationNameAndEmail(input.EmailAddress, input.CompanyName).FirstOrDefaultAsync();

            await Task.WhenAll(cu);

            if (cu.Result == null)
            {
                retVal.ErrorMessage = "Customer not found";
                return retVal;

            }

            var input2 = new CustomerAccountInput
            {
                CustomerId = cu.Result.CustomerId,
                CustomerOrganizationId = cu.Result.OrganizationId,
                PaymentTermId = input.PaymentTermId.GetValueOrDefault(1)
            };

            input2.InjectFrom(input);
            var output = await Create(input2);
            if (output.Succeeded)
            {
                await Task.Run(() =>
                {
                    retVal = new CustomerAccountResult()
                    {
                        AccountManagerId = input.AccountManagerId,
                        AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                        CustomerId = cu.Result.CustomerId,
                        CustomerOrganizationId = cu.Result.OrganizationId,
                        Succeeded = true,
                        BuyerNumber = output.BuyerNumber,
                        Number = output.Number
                    };

                    RaiseEvent(new CustomerAccountLinkedEvent
                    {
                        AccountManagerId = input.AccountManagerId,
                        AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                        CustomerId = cu.Result.CustomerId,
                        CustomerOrganizationId = cu.Result.OrganizationId
                    });
                });

            }


            return retVal;
        }

        private async Task<CustomerAccountResult> Link(LinkCustomerInput input)
        {
            _logger.LogInformation(GetLogMessage(
                $@"Linking Account, Account Manager: {input.AccountManagerId}, Customer Lookup Info: {input.EmailAddress}"));

            CustomerAccountResult retVal = new CustomerAccountResult();
            
            var cu = await  _customers.Queryable()
                .Include(x => x.Person).ThenInclude(x => x.ApplicationUser)
                .Include(x=>x.OrganizationCustomers)
                .Where(x => x.Person.ApplicationUser.Email == input.EmailAddress)
                .FirstAsync();

            if (cu.OrganizationCustomers.Count > 0)
            {
                var orgCu = cu.OrganizationCustomers.First();

                var input2 = new CustomerAccountInput
                {
                    CustomerId = cu.Id,
                    CustomerOrganizationId = orgCu.OrganizationId,
                    PaymentTermId = input.PaymentTermId.GetValueOrDefault(1)
                };

                input2.InjectFrom(input);
                var output = await Create(input2);
                if (output.Succeeded)
                {
                    await Task.Run(() =>
                    {
                        retVal = new CustomerAccountResult()
                        {
                            AccountManagerId = input.AccountManagerId,
                            AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                            CustomerId = orgCu.CustomerId,
                            CustomerOrganizationId = orgCu.OrganizationId,
                            Succeeded = true,
                            BuyerNumber = output.BuyerNumber,
                            Number = output.Number
                        };

                        RaiseEvent(new CustomerAccountLinkedEvent
                        {
                            AccountManagerId = input.AccountManagerId,
                            AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                            CustomerId = orgCu.CustomerId,
                            CustomerOrganizationId = orgCu.OrganizationId
                        });
                    });

                }

            }


            return retVal;
        }

    }
}