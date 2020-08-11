// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Marketers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.Marketers
{
    public partial class MarketerService : Service<Marketer>, IMarketerService
    {
        private readonly ILogger<IMarketerService> _logger;
        private readonly IRepositoryAsync<Person> _peopleRepository;


        public MarketerService(
            IServiceProvider serviceProvider,
            ILogger<IMarketerService> logger,
            IRepositoryAsync<Person> peopleRepository) : base(serviceProvider)
        {
            _logger = logger;
            _peopleRepository = peopleRepository;
        }

        public Task<T> GetById<T>(Guid id)
            where T : MarketerOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }


        public Task<MarketerOutput> Get(Guid id)
        {
            return GetById<MarketerOutput>(id);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[MarketerService.{callerName}] - {message}";
        }
    }
}