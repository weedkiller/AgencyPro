// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Retainers.Services;
using AgencyPro.Core.Retainers.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Retainers
{
    public partial class RetainerService : Service<ProjectRetainerIntent>, IRetainerService
    {
        private readonly ILogger<RetainerService> _logger;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(RetainerService)}.{callerName}] - {message}";
        }

        private readonly IRepositoryAsync<Project> _projects;

        public RetainerService(
            ILogger<RetainerService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _logger = logger;
            _projects = UnitOfWork.RepositoryAsync<Project>();
        }

        public Task<RetainerOutput> GetRetainer(IOrganizationCustomer customer, Guid retainerId)
        {
            return Repository.Queryable().Where(x => x.CustomerId == customer.CustomerId 
                                                     && x.CustomerOrganizationId == customer.OrganizationId 
                                                     && x.ProjectId == retainerId)
                .ProjectTo<RetainerOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}
