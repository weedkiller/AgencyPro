// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;

namespace AgencyPro.Core.Roles.Projections
{
    public class ProjectManagerProjectionMap : Profile
    {
        public ProjectManagerProjectionMap()
        {
            CreateMap<ProjectManager, ProjectManagerOutput>()
                .IncludeAllDerived();

            CreateMap<ProjectManager, ProjectManagerDetailsOutput>();
            CreateMap<ProjectManager, AccountManagerProjectManagerOutput>();
            CreateMap<ProjectManager, AgencyOwnerProjectManagerOutput>();
        }
    }
}