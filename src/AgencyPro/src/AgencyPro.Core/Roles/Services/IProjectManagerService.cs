// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Roles.Services
{
    public interface IProjectManagerService : IService<ProjectManager>,
        IRoleService<ProjectManagerInput, ProjectManagerUpdateInput, ProjectManagerOutput, IProjectManager>
    {
    }
}