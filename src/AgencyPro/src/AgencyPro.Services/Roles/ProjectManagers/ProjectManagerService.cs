// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Roles.ProjectManagers
{
    public partial class ProjectManagerService : Service<ProjectManager>, IProjectManagerService
    {
        private readonly ILogger<ProjectManagerService> _logger;


        public ProjectManagerService(
            IServiceProvider serviceProvider, ILogger<ProjectManagerService> logger) : base(serviceProvider)
        {
            _logger = logger;
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[ProjectManagerService.{callerName}] - {message}";
        }

        public Task<T> GetById<T>(Guid id)
            where T : ProjectManagerOutput
        {
            return Repository.Queryable()
                .Where(x => x.Id == id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<ProjectManagerOutput> Get(Guid id)
        {
            return GetById<ProjectManagerOutput>(id);
        }
    }
}