// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Roles.Extensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.AccountManagers
{
    public partial class AccountManagerService : Service<AccountManager>, IAccountManagerService
    {
        private readonly IRepositoryAsync<Person> _peopleRepository;
        private readonly ILogger<AccountManagerService> _logger;

        public AccountManagerService(
            IServiceProvider serviceProvider,
            IRepositoryAsync<Person> peopleRepository, 
            ILogger<AccountManagerService> logger) : base(serviceProvider)
        {
            _peopleRepository = peopleRepository;
            _logger = logger;
        }

        public Task<T> GetById<T>(Guid id) where T : AccountManagerOutput
        {
            return Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<AccountManagerOutput> Get(Guid id)
        {
            return GetById<AccountManagerOutput>(id);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[AccountManagerService.{callerName}] - {message}";
        }
    }
}