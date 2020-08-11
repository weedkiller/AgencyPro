// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.FinancialAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.People.Services;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.FinancialAccounts
{
    public partial class FinancialAccountService : Service<FinancialAccount>, IFinancialAccountService
    {
        private readonly ILogger<FinancialAccountService> _logger;
        private readonly IRepositoryAsync<OrganizationFinancialAccount> _organizationFinancialAccountRepository;
        private readonly IRepositoryAsync<IndividualFinancialAccount> _individualFinancialAccountRepository;

        public FinancialAccountService(
            ILogger<FinancialAccountService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
            _organizationFinancialAccountRepository = UnitOfWork.RepositoryAsync<OrganizationFinancialAccount>();
            _individualFinancialAccountRepository = UnitOfWork.RepositoryAsync<IndividualFinancialAccount>();
        }


        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[FinancialAccountService.{callerName}] - {message}";
        }


        public async Task<FinancialAccountDetails> GetFinancialAccount(IPerson person)
        {
            _logger.LogInformation(GetLogMessage("Person: {0}"), person.Id);

            var account = await _individualFinancialAccountRepository.Queryable()
                .Where(x => x.Id == person.Id)
                .ProjectTo<FinancialAccountDetails>(ProjectionMapping)
                .FirstOrDefaultAsync();

        
            return account;
        }

        public async Task<FinancialAccountDetails> GetFinancialAccount(IAgencyOwner principal)
        {
            _logger.LogInformation(GetLogMessage("Principal: {0}"), principal.CustomerId);

            var account = await _organizationFinancialAccountRepository
                .Queryable()
                .Where(x => x.Id == principal.OrganizationId)
                .ProjectTo<FinancialAccountDetails>(ProjectionMapping)
                .FirstOrDefaultAsync();

            
            return account;
        }


        public async Task<FinancialAccountResult> AccountCreatedOrUpdated(global::Stripe.Account account)
        {
            _logger.LogInformation(GetLogMessage("{Account}"), account.Id);

            _logger.LogInformation(GetLogMessage("Account metadata : {0}"), account.Metadata);

            var retVal = new FinancialAccountResult()
            {
                FinancialAccountId = account.Id
            };

            var entity = await Repository.Queryable()
                .Include(x => x.IndividualFinancialAccount)
                .Include(x => x.OrganizationFinancialAccount)
                .ThenInclude(x => x.Organization)
                .Where(x => x.AccountId == account.Id)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new FinancialAccount()
                {
                    AccountId = account.Id,
                    ObjectState = ObjectState.Added
                };


            }
            else
            {
                entity.ObjectState = ObjectState.Modified;
                entity.Updated = DateTimeOffset.UtcNow;
            }

            if (account.Metadata.ContainsKey("org-id"))
            {
                _logger.LogDebug(GetLogMessage("Organization Account"));

                var organizationId = Guid.Parse(account.Metadata["org-id"]);
                if (entity.OrganizationFinancialAccount == null)
                {
                    entity.OrganizationFinancialAccount = new OrganizationFinancialAccount()
                    {
                        ObjectState = ObjectState.Added,
                        Id = organizationId,
                        FinancialAccountId = account.Id
                    };
                }
            }
            else if (account.Metadata.ContainsKey("person-id"))
            {
                _logger.LogDebug(GetLogMessage("Person Account"));

                var personId = Guid.Parse(account.Metadata["person-id"]);
                if (entity.IndividualFinancialAccount == null)
                {
                    entity.IndividualFinancialAccount = new IndividualFinancialAccount()
                    {
                        
                        ObjectState = ObjectState.Added,
                        Id = personId,
                        FinancialAccountId = account.Id,
                    };
                }
            }

            entity.MerchantCategoryCode = account.BusinessProfile.Mcc;
            entity.SupportEmail = account.BusinessProfile.SupportEmail;
            entity.SupportPhone = account.BusinessProfile.SupportPhone;
            entity.AccountType = account.Type;

            entity.ChargesEnabled = account.ChargesEnabled;
            entity.PayoutsEnabled = account.PayoutsEnabled;

            entity.CardPaymentsCapabilityStatus = account.Capabilities.CardPayments;
            entity.TransfersCapabilityStatus = account.Capabilities.Transfers;
            entity.DefaultCurrency = account.DefaultCurrency;
            entity.IsDeleted = account.Deleted.GetValueOrDefault();

            var records = Repository.InsertOrUpdateGraph(entity, true);
            _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);
            if (records > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }


    }
}
