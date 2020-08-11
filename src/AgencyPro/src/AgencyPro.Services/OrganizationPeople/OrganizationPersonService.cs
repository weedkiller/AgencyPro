// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationPeople;
using AgencyPro.Core.OrganizationPeople.Extensions;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Account;
using AgencyPro.Services.OrganizationPeople.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationPeople
{
    public partial class OrganizationPersonService : Service<OrganizationPerson>, IOrganizationPersonService
    {
        private readonly ILogger<OrganizationPersonService> _logger;
        private readonly IOrganizationService _orgService;
        private readonly IPersonService _personService;
        private readonly IStripeService _stripeService;
        private readonly IUserInfo _userInfo;
        private readonly UserAccountManager _accountManager;
        private readonly IRepositoryAsync<Organization> _organizationRepository;
        private readonly IRepositoryAsync<ApplicationUser> _applicationUsers;

        public OrganizationPersonService(
            IOrganizationService orgService,
            IServiceProvider serviceProvider,
            IPersonService personService,
            IStripeService stripeService,
            IUserInfo userInfo,
            OrganizationPersonEventHandler handler,
            UserAccountManager accountManager,
            ILogger<OrganizationPersonService> logger) : base(serviceProvider)
        {
            _orgService = orgService;
            _personService = personService;
            _stripeService = stripeService;
            _userInfo = userInfo;
            _accountManager = accountManager;
            _organizationRepository = UnitOfWork.RepositoryAsync<Organization>();
            _applicationUsers = UnitOfWork.RepositoryAsync<ApplicationUser>();

            _logger = logger;

            AddEventHandler(handler);
        }


        public async Task<T> GetOrganizationPerson<T>(Guid personId, Guid organizationId)
            where T : OrganizationPersonOutput
        {
            var p = await Repository
                .Queryable()
                .Where(x => x.PersonId == personId && x.OrganizationId == organizationId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return p;
        }

        public Task<OrganizationPersonOutput> Get(Guid personId, Guid organizationId)
        {
            return GetOrganizationPerson<OrganizationPersonOutput>(personId, organizationId);
        }

        public async Task<IOrganizationPerson> GetPrincipal(Guid personId, Guid organizationId)
        {
            var principal = await Get(personId, organizationId);
            return principal;
        }

        public Task<PackedList<T>> GetPeople<T>(IAgencyOwner ao, OrganizationPeopleFilters filters)
            where T : AgencyOwnerOrganizationPersonOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<OrganizationPerson, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetPeople<T>(IOrganizationAccountManager am, OrganizationPeopleFilters filters)
            where T : AccountManagerOrganizationPersonOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .OrderByDescending(x => x.Updated)
                .PaginateProjection<OrganizationPerson, T>(filters, ProjectionMapping);
        }

        public Task<OrganizationPersonOutput> GetPersonByCode(string code)
        {
            return Repository.Queryable()
                .Where(x => x.AffiliateCode == code)
                .ProjectTo<OrganizationPersonOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationPersonService.{callerName}] - {message}";
        }
    }
}