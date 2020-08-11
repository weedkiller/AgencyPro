// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.CustomerAccounts.Events;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.Organizations.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.CustomerAccounts
{
    public partial class CustomerAccountService
    {
        private async Task<CustomerAccountResult> CreateInternal(IOrganizationAccountManager am,
            NewCustomerAccountInput model)
        {
            var retVal = new CustomerAccountResult();

            _logger.LogInformation(GetLogMessage("AM: {0}; Input: {@input};"), am.OrganizationId, model);

            model.AccountManagerId = am.AccountManagerId;

            var person = await _personService
                .CreatePerson(model, null, model.MarketerId, model.MarketerOrganizationId??am.OrganizationId);

            if (person.Succeeded)
            {
                _logger.LogDebug(GetLogMessage("Person was created successfully"));

                var result = await _organizationService.CreateOrganization(am, new OrganizationCreateInput()
                {
                    Name = model.OrganizationName,
                    Description = string.Empty,
                    Iso2 = model.Iso2,
                    ProvinceState = model.ProvinceState,
                }, person.PersonId.Value);

                if (result.Succeeded)
                {

                    _logger.LogWarning(GetLogMessage("Organization was created successfully"));

                    retVal.Succeeded = true;
                    retVal.AccountManagerId = model.AccountManagerId;
                    retVal.AccountManagerOrganizationId = am.OrganizationId;
                    retVal.CustomerId = person.PersonId.Value;
                    retVal.CustomerOrganizationId = result.OrganizationId.Value;

                    if (result?.OrganizationId != null)
                    {
                        var m = new CustomerAccountInput()
                        {
                            PaymentTermId = model.PaymentTermId.GetValueOrDefault(1),
                            AccountManagerId = am.AccountManagerId,
                            AccountManagerOrganizationId = am.OrganizationId,
                            CustomerId = person.PersonId.Value,
                            CustomerOrganizationId = result.OrganizationId.Value,
                            AutoApproveTimeEntries = model.AutoApproveTimeEntries,
                        };




                        return await Create(m);
                    }

                }
                else
                {
                    retVal.ErrorMessage = result.ErrorMessage;
                    _logger.LogWarning(GetLogMessage("unable to create organization"));
                }
            }
            else
            {
                retVal.ErrorMessage = person.ErrorMessage;
                _logger.LogWarning(GetLogMessage("unable to create person"));
            }

            return retVal;

        }

        public async Task<CustomerAccountResult> CreateInternalAccount(IOrganizationAccountManager am)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}"), am.OrganizationId);

            var retVal = new CustomerAccountResult();

            var providerOrganization = _organizations
                .Queryable()
                .Include(x=>x.ProviderOrganization)
                .Include(x => x.BuyerCustomerAccounts)
                .First(x => x.Id == am.OrganizationId);

            if (!providerOrganization.BuyerCustomerAccounts.Any(x => x.AccountManagerId == am.AccountManagerId &&
                                                                    x.AccountManagerOrganizationId == am.AccountManagerId))
            {
                _logger.LogWarning(GetLogMessage("Internal account doesn't exist"));

                retVal = await Create(new CustomerAccountInput()
                {
                    AccountManagerId = am.AccountManagerId,
                    AutoApproveTimeEntries = providerOrganization.ProviderOrganization.AutoApproveTimeEntries,
                    AccountManagerOrganizationId = am.OrganizationId,
                    CustomerId = providerOrganization.CustomerId,
                    CustomerOrganizationId = providerOrganization.Id,
                    PaymentTermId = null
                });
            }
            else
            {
                retVal.AccountManagerId = am.AccountManagerId;
                retVal.AccountManagerOrganizationId = am.OrganizationId;
                retVal.CustomerId = am.AccountManagerId;
                retVal.CustomerOrganizationId = am.OrganizationId;
                retVal.Succeeded = false;
                retVal.ErrorMessage = "Internal account already exists";

                _logger.LogWarning(GetLogMessage("Internal account already exists"));
            }

            return retVal;

        }

        public async Task<CustomerAccountResult> Create(
            IOrganizationAccountManager am,
            NewCustomerAccountInput model, bool checkValidation = true)
        {
            _logger.LogInformation(GetLogMessage("AM: {0}; Input: {@input};"), am.OrganizationId, model);

            model.PhoneNumber = !string.IsNullOrEmpty(model.PhoneNumber?.Trim()) ? model.PhoneNumber.Trim() : null;

            if (!checkValidation)
                return await CreateInternal(am, model);

            return CheckValidation(model) ?? await CreateInternal(am, model);
        }

        private CustomerAccountResult CheckValidation(NewCustomerAccountInput model)
        {
            bool isPhoneExists = false;
            bool isOrganizationExists = false;
            bool isEmailExists = false;

            if (!string.IsNullOrEmpty(model.PhoneNumber?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Phone Number validation : {0}"), model.PhoneNumber);
                isPhoneExists = _applicationUsers.Queryable().Any(a => a.PhoneNumber == model.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(model.EmailAddress?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Email validation : {0}"), model.EmailAddress);
                isEmailExists = _applicationUsers.Queryable().Any(a => a.Email == model.EmailAddress);
            }

            if (!string.IsNullOrEmpty(model.OrganizationName?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Organization Name validation : {0}"), model.OrganizationName);
                isOrganizationExists = _organizations.Queryable().Any(a => a.Name == model.OrganizationName);
            }

            if (!isPhoneExists && !isOrganizationExists && !isEmailExists) return null;

            string errorMessage = GetErrorMessage(isPhoneExists, isOrganizationExists, isEmailExists);
            _logger.LogInformation(GetLogMessage(errorMessage));

            return new CustomerAccountResult
            {
                Succeeded = false,
                ErrorMessage = errorMessage
            };
        }

        private string GetErrorMessage(bool phoneValidation, bool organizationValidation, bool isEmailExists)
        {
            List<string> validationList = new List<string>();
            if (phoneValidation)
                validationList.Add("Phone number");

            if (organizationValidation)
                validationList.Add("Organization name");

            if (isEmailExists)
                validationList.Add("Email");

            return string.Join(", ", validationList) + " already exists.";
        }

        public async Task<CustomerAccountResult> Create(
            IProviderAgencyOwner ao,
            NewCustomerAccountInput model)
        {
            _logger.LogInformation(GetLogMessage("PAO: {0}; Input: {@input};"), ao.OrganizationId, model);

            var accountManager = await _accountManagerService
                .GetAccountManagerOrDefault<AgencyOwnerOrganizationAccountManagerOutput>
                    (ao.OrganizationId, model.AccountManagerId);

            model.PhoneNumber = !string.IsNullOrEmpty(model.PhoneNumber?.Trim()) ? model.PhoneNumber.Trim() : null;

            return CheckValidation(model) ?? await CreateInternal(accountManager, model);
        }


        public async Task<CustomerAccountResult> Create(IOrganizationCustomer cu, JoinAsCustomerInput input)
        {
            _logger.LogInformation(GetLogMessage("Customer: {0}"), cu.OrganizationId);

            var org = await _organizationService.Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Include(x => x.AccountManagers)
                .Where(x => x.ProviderOrganization.Discoverable && x.Id == input.ProviderOrganizationId)
                .FirstOrDefaultAsync();

            var model = new CustomerAccountInput()
            {
                AccountManagerId = org.ProviderOrganization.DefaultAccountManagerId,
                AccountManagerOrganizationId = org.Id,
                CustomerId = cu.CustomerId,
                CustomerOrganizationId = cu.OrganizationId,
                PaymentTermId = null
            };



            return await Create(model);
            

        }


        private async Task<CustomerAccountResult> Create(CustomerAccountInput input)
        {
            _logger.LogInformation(GetLogMessage(
                $@"Creating Account, Account Manager: {input.AccountManagerId}, Customer: {input.CustomerId}"));

            var retVal = new CustomerAccountResult()
            {
                CustomerOrganizationId = input.CustomerOrganizationId,
                CustomerId = input.CustomerId,
                AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                AccountManagerId = input.AccountManagerId
            };

            var cu = await 
                _customerService.Repository.Queryable()
                    .Where(x => x.OrganizationId == input.CustomerOrganizationId && x.CustomerId == input.CustomerId)
                    .Include(x => x.Customer)
                    .ThenInclude(x => x.OrganizationMarketer)
                    .FirstAsync();

            var am = await
                _accountManagerService.Repository.Queryable().Where(x=>x.AccountManagerId == input.AccountManagerId && x.OrganizationId == input.AccountManagerOrganizationId)
                    .FirstOrDefaultAsync();

            var acctCandidate = await Repository.Queryable().Where(
                x=>x.AccountManagerOrganizationId==input.AccountManagerOrganizationId &&
                   x.AccountManagerId == input.AccountManagerId &&
                   x.CustomerId == input.CustomerId && x.CustomerOrganizationId == input.CustomerOrganizationId)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();

            

            if (cu == null)
            {
                _logger.LogInformation(GetLogMessage("Customer not found"));
                retVal.ErrorMessage = "Customer not found";
                return retVal;
            }

            if (am == null)
            {
                _logger.LogInformation(GetLogMessage("Account manager not found"));
                retVal.ErrorMessage = "Account manager not found";
                return retVal;
            }

            // todo: this value could come from somewhere else if it's a repeat customer
            var marketerStream = cu.Customer.OrganizationMarketer.MarketerStream;
            var marketingAgencyStream = cu.Customer.OrganizationMarketer.MarketerBonus;

            var marketingAgreement = await _marketingAgreements.Queryable()
                .Where(x => x.MarketingOrganizationId == cu.Customer.MarketerOrganizationId &&
                            x.ProviderOrganizationId == am.OrganizationId)
                .FirstOrDefaultAsync();

            if (marketingAgreement != null)
            {
                marketerStream = marketingAgreement.MarketerStream;
                marketingAgencyStream = marketingAgreement.MarketingAgencyStream;
            }


            var entity = new CustomerAccount
            {
                AccountStatus = AccountStatus.Active,
                ObjectState = ObjectState.Added,
                AccountManagerOrganizationId = input.AccountManagerOrganizationId,
                AccountManagerId = input.AccountManagerId,
                CustomerId = input.CustomerId,
                CreatedById = _userInfo.UserId,
                UpdatedById = _userInfo.UserId,
                MarketerStream = marketerStream,
                MarketingAgencyStream = marketingAgencyStream,
                CustomerOrganizationId = input.CustomerOrganizationId,
                PaymentTermId = input.PaymentTermId.GetValueOrDefault(1),
                AutoApproveTimeEntries = input.AutoApproveTimeEntries
            }.InjectFrom(input) as CustomerAccount;
            
            entity.StatusTransitions.Add(new CustomerAccountStatusTransition()
            {
                Status = entity.AccountStatus,
                ObjectState = ObjectState.Added
            });

            var records = 0;
            if (acctCandidate != null)
            {
                _logger.LogDebug(GetLogMessage("Existing customer account found already, was deleted: {0}"), 
                    acctCandidate.IsDeleted);

                // there is an existing account already
                entity = await Get(
                    input.AccountManagerOrganizationId, 
                    input.AccountManagerId,
                    input.CustomerOrganizationId, 
                    input.CustomerId);
                entity.ObjectState = ObjectState.Modified;
                entity.UpdatedById = _userInfo.UserId;
                entity.Updated = DateTimeOffset.UtcNow;
                entity.IsDeleted = false;
                records = await Repository.UpdateAsync(entity, true);
            }
            else
            {

                _logger.LogDebug(GetLogMessage("Ready to create new customer"));

                // create a new account
                entity.Number = 
                    await GetNextAccountId(input.AccountManagerOrganizationId);

                entity.BuyerNumber = 
                    await GetNextBuyerAccountId(input.CustomerOrganizationId);

                entity.ObjectState = ObjectState.Added;
                records = await Repository.InsertAsync(entity, true);


            }
            _logger.LogDebug(GetLogMessage("{0} customer account records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.BuyerNumber = entity.BuyerNumber;
                retVal.Number = entity.Number;

                await Task.Run(() =>
                {
                    RaiseEvent(new CustomerAccountCreatedEvent
                    {
                        CustomerId = retVal.CustomerId.Value,
                        AccountManagerId = retVal.AccountManagerId.Value,
                        AccountManagerOrganizationId = retVal.AccountManagerOrganizationId.Value,
                        CustomerOrganizationId = retVal.CustomerOrganizationId.Value,
                        
                    });
                });
            }
            else
            {
                _logger.LogWarning(GetLogMessage("Unable to create customer account"));
                retVal.ErrorMessage = "Unable to create customer account";
            }
           

            return retVal;
        }
    }
}