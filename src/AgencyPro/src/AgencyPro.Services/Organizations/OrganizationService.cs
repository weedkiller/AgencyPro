// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Categories.ViewModels;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Infrastructure.Storage;
using AgencyPro.Core.OrganizationPeople.Extensions;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Subscriptions.Services;
using AgencyPro.Core.UserAccount.Services;

namespace AgencyPro.Services.Organizations
{
    public partial class OrganizationService : Service<Organization>, IOrganizationService
    {
        private readonly ILogger<OrganizationService> _logger;
        private readonly IRepositoryAsync<OrganizationPerson> _organizationPersonRepo;
      
        private readonly IStorageService _storageService;
        private readonly IBuyerAccountService _buyerService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly Lazy<IUserInfo> _userInfo;

        public OrganizationService(
            IServiceProvider serviceProvider,
            IStorageService storageService,
            ISubscriptionService subscriptionService,
            Lazy<IUserInfo> userInfo,
            ILogger<OrganizationService> logger, 
            IBuyerAccountService buyerService) : base(serviceProvider)
        {
            _logger = logger;
            _buyerService = buyerService;
            _organizationPersonRepo = UnitOfWork.RepositoryAsync<OrganizationPerson>();
          
            UnitOfWork.RepositoryAsync<Contract>();
            _storageService = storageService;
            _subscriptionService = subscriptionService;
            _userInfo = userInfo;
        }


        public async Task<T> GetOrganization<T>(Guid organizationId
        ) where T : OrganizationOutput
        {
            var organization = await Repository.Queryable()
                .Where(x => x.Id == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return organization;
        }
        public async Task DeleteOrganization(Guid organizationId) 
        {
            await Repository.DeleteAsync(o => o.Id == organizationId, true);
        }

        public Task<T> GetOrganization<T>(IOrganizationProjectManager pm) where T : ProjectManagerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == pm.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationCustomer cu) where T : CustomerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == cu.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationContractor co) where T : ContractorOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == co.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationMarketer ma) where T : MarketerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == ma.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOrganization<T>(IOrganizationRecruiter re) where T : RecruiterOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == re.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetOrganization<T>(IAgencyOwner ao) where T :OrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == ao.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProviderOrganization<T>(IOrganizationCustomer cu, Guid organizationId) where T : CustomerProviderOrganizationOutput
        {
            return Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.OrganizationType.HasFlag(OrganizationType.Provider) 
                            && x.ProviderOrganization.Discoverable
                            && x.Id == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public async Task<List<T>> GetProviderOrganizations<T>(IOrganizationCustomer cu, OrganizationFilters filters) where T : CustomerProviderOrganizationOutput
        {
            var org = await Repository.Queryable()
                .Include(x => x.BuyerCustomerAccounts)
                .Where(x => x.Id == cu.OrganizationId)
                .FirstAsync();

            var existingAccountOrgs = org.BuyerCustomerAccounts.Select(x => x.AccountManagerOrganizationId)
                .ToArray();

            var  orgs = await Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.ProviderOrganization.Discoverable &&
                            x.OrganizationType.HasFlag(OrganizationType.Provider) &&
                            x.Id != cu.OrganizationId)
                .Where(x=>!existingAccountOrgs.Contains(x.Id))
                .ApplyWhereFilters(filters)
                
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
            
            return orgs;
        }

        public async Task<CustomerProviderOrganizationSummary> GetProviderOrganizationSummary(IOrganizationCustomer cu, OrganizationFilters filters)
        {
            var categories = await Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.OrganizationType.HasFlag(OrganizationType.Provider) && x.ProviderOrganization.Discoverable &&
                            x.Id != cu.OrganizationId)
                                .ApplyWhereFilters(filters)

                .Include(x => x.Category)
                .Select(s => s.Category).Distinct()
                .ProjectTo<CategoryOutput>(ProjectionMapping)
                .ToListAsync();

            var skills = await Repository.Queryable()
                .Include(x=>x.ProviderOrganization)
                .Where(x => x.OrganizationType.HasFlag(OrganizationType.Provider) &&
                            x.ProviderOrganization.Discoverable &&
                            x.Id != cu.OrganizationId)
                .Include(x => x.ProviderOrganization.Skills)
                .SelectMany(x => x.ProviderOrganization.Skills)
                .ProjectTo<SkillOutput>(ProjectionMapping)
                .ToListAsync();

            var resp = new CustomerProviderOrganizationSummary
            {
                Organizations = await GetProviderOrganizations<CustomerProviderOrganizationOutput>(cu, filters),
                AvailableCategories = categories,
                AvailableSkills = skills
            };

            return resp;
        }

        public Task<OrganizationOutput> Get(Guid organizationId)
        {
            return GetOrganization<OrganizationOutput>(organizationId);
        }

        public Task<T> GetOrganization<T>(IOrganizationAccountManager am) where T : AccountManagerOrganizationOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == am.OrganizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<List<T>> GetOrganizations<T>(Guid personId) where T : OrganizationOutput
        {
            return _organizationPersonRepo.Queryable()
                .Where(x => x.PersonId == personId)
                .Select(x => x.Organization)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetOrganizations<T>(IMarketer marketer) where T : MarketerOrganizationOutput
        {
            return Repository.Queryable()
                .Include(x=>x.MarketingOrganization)
                .Where(x => x.MarketingOrganization != null && x.MarketingOrganization.Discoverable)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<T>> GetOrganizations<T>(IRecruiter marketer) where T : RecruiterOrganizationOutput
        {
            return Repository.Queryable()
                .Include(x=>x.RecruitingOrganization)
                .Where(x => x.RecruitingOrganization != null && x.RecruitingOrganization.Discoverable)
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<AffiliationOutput>> GetAffiliationsForPerson(Guid personId)
        {
            return _organizationPersonRepo.Queryable()
                .ForPerson(personId)
                .ProjectTo<AffiliationOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<AgencyOwnerCounts> GetCounts(IAgencyOwner agencyOwner)
        {
            return Repository.Queryable()
                .Where(x => x.Id == agencyOwner.OrganizationId)
                .ProjectTo<AgencyOwnerCounts>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<AgencyOwnerMarketingOrganizationDetailsOutput> GetMarketingDetails(IMarketingAgencyOwner agencyOwner)
        {
            return Repository.Queryable()
                .Include(x => x.MarketingOrganization)
                .Where(x => x.MarketingOrganization != null && x.Id == agencyOwner.OrganizationId)
                .Select(x => x.MarketingOrganization)
                .ProjectTo<AgencyOwnerMarketingOrganizationDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<AgencyOwnerRecruitingOrganizationDetailsOutput> GetRecruitingDetails(IRecruitingAgencyOwner agencyOwner)
        {
            return Repository.Queryable()
                .Include(x => x.RecruitingOrganization)
                .Where(x => x.RecruitingOrganization != null && x.Id == agencyOwner.OrganizationId)
                .Select(x=>x.RecruitingOrganization)
                .ProjectTo<AgencyOwnerRecruitingOrganizationDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<AgencyOwnerProviderOrganizationDetailsOutput> GetProviderDetails(IProviderAgencyOwner agencyOwner)
        {
            return Repository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Where(x => x.ProviderOrganization != null && x.Id == agencyOwner.OrganizationId)
                .Select(x => x.ProviderOrganization)
                .ProjectTo<AgencyOwnerProviderOrganizationDetailsOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationService)}.{callerName}] - {message}";
        }
    }
}