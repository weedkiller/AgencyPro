// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Roles.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Core.Roles.Extensions
{
    public static class ProjectManagerExtensions
    {
        public static IQueryable<ProjectManager> IncludeStandardIncludes(this IQueryable<ProjectManager> entities)
        {
            return entities.Include(x => x.Person);
        }

        public static IQueryable<ProjectManager> IncludeDetailedIncludes(this IQueryable<ProjectManager> entities)
        {
            return entities.IncludeStandardIncludes()
                .Include(x => x.OrganizationProjectManagers);
        }
    }
}