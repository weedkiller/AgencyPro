// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Data.Repositories;
using AgencyPro.Services.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.People
{
    public partial class PersonService : Service<Person>, IPersonService
    {
        private readonly IConfiguration _configuration;
        private readonly UserAccountManager _userAccountManager;
        private readonly IStripeService _stripeService;
        private readonly IRepositoryAsync<OrganizationPerson> _orgPersonRepository;
        private readonly IRepositoryAsync<OrganizationRecruiter> _orgRecruiterRepository;
        private readonly IRepositoryAsync<OrganizationMarketer> _orgMarketerRepository;
        private readonly ILogger<PersonService> _logger;


        public PersonService(
            UserAccountManager userAccountManager,
            IServiceProvider serviceProvider,
            IStripeService stripeService,
            ILogger<PersonService> logger,
            IConfiguration configuration) : base(serviceProvider)
        {
            _userAccountManager = userAccountManager;
            _stripeService = stripeService;
            _orgPersonRepository = UnitOfWork.RepositoryAsync<OrganizationPerson>();
            _logger = logger;
            _configuration = configuration;
            _orgRecruiterRepository = UnitOfWork.RepositoryAsync<OrganizationRecruiter>();
            _orgMarketerRepository = UnitOfWork.RepositoryAsync<OrganizationMarketer>();
        }

        public async Task<T> GetPerson<T>(Guid personId)
            where T : PersonOutput
        {
            var person = await Repository
                .Queryable()
                .GetById(personId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();

            return person;
        }

        public Task<PersonOutput> Get(Guid personId)
        {
            return GetPerson<PersonOutput>(personId);
        }

        public Task<T> GetPerson<T>(string email)
            where T : PersonOutput
        {
            var person = Repository.Queryable()
                .GetByEmail(email)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();

            return person;
        }


        public Task<List<T>> GetPeople<T>(Guid[] ids)
            where T : PersonOutput
        {
            return Repository.Queryable()
                .Where(x => ids.Contains(x.Id))
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[PersonService.{callerName}] - {message}";
        }

        public async Task<PersonOutput> CreateOrUpdate(PersonOutput model)
        {
            var entity = await Repository.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (entity != null)
            {
                entity.InjectFrom(model);
                await Repository.UpdateAsync(entity, true);
            }
            var output = await GetPerson<PersonOutput>(model.Id);
            return output;
        }
    }
}