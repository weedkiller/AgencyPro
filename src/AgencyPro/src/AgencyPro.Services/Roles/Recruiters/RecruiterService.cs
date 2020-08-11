// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.Recruiters
{
    public partial class RecruiterService : Service<Recruiter>, IRecruiterService
    {
        private readonly IRepositoryAsync<Person> _peopleRepository;
        private readonly ILogger<RecruiterService> _recruiterLogger;

        public RecruiterService(
            IServiceProvider serviceProvider,
            IRepositoryAsync<Person> peopleRepository,
            ILogger<RecruiterService> recruiterLogger) : base(serviceProvider)
        {
            _peopleRepository = peopleRepository;
            _recruiterLogger = recruiterLogger;
        }

        public Task<T> GetById<T>(Guid id)
            where T : RecruiterOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public Task<RecruiterOutput> Get(Guid id)
        {
            return GetById<RecruiterOutput>(id);
        }
    }
}