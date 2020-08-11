// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;

namespace AgencyPro.Services.Roles.ProjectManagers
{
    public partial class ProjectManagerService
    {
        public Task<T> Update<T>(IProjectManager projectManager, ProjectManagerUpdateInput model)
            where T : ProjectManagerOutput
        {
            throw new NotImplementedException();
        }
    }
}