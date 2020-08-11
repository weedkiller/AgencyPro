// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.Projects.Extensions
{
    public static partial class ProjectExtensions
    {
        public static IQueryable<Project> FindById(this IQueryable<Project> entities, Guid id)
        {
            return entities.Where(x => x.Id == id);
        }


        public static Dictionary<ProjectStatus, List<Project>> GroupByProjectStatus(this IEnumerable<Project> projects)
        {
            var projectDictionary = Enum.GetValues(typeof(ProjectStatus))
                .Cast<ProjectStatus>()
                .ToDictionary(s => s, s => projects.Where(x => x.Status == s).ToList());
            return projectDictionary;
        }
    }
}