// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Extensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.Contractors
{
    public partial class ContractorService : Service<Contractor>, IContractorService
    {
        private readonly ILogger<ContractorService> _logger;
        private readonly IRepositoryAsync<OrganizationRecruiter> _recruiterRepository;

        public ContractorService(
            IServiceProvider serviceProvider,
            ILogger<ContractorService> logger) : base(serviceProvider)
        {
            _logger = logger;
            _recruiterRepository = UnitOfWork.RepositoryAsync<OrganizationRecruiter>();
        }


        public Task<PackedList<T>> GetContractors<T>(IOrganizationRecruiter re, CommonFilters filters)
            where T : ContractorOutput
        {
            return Repository.Queryable()
                .ForOrganizationRecruiter(re)
                .PaginateProjection<Contractor, T>(filters, ProjectionMapping);
        }


        public Task<T> GetById<T>(Guid id)
            where T : ContractorOutput
        {
            return Repository.Queryable()
                .FindById(id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<ContractorOutput> Get(Guid id)
        {
            return GetById<ContractorOutput>(id);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[ContractorService.{callerName}] - {message}";
        }
    }
}