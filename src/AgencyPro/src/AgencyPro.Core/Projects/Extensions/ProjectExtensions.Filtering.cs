// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Projects.Filters;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.Projects.Extensions
{
    public static partial class ProjectExtensions
    {
        public static IQueryable<Project> ApplyWhereFilters(this IQueryable<Project> entities,
            ProjectFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        public static Expression<Func<Project, bool>> WhereFilter(ProjectFilters filters)
        {
            var expr = PredicateBuilder.True<Project>();

            if (filters.CustomerId.HasValue)
                expr = expr.And(x => x.CustomerId == filters.CustomerId);

            if (filters.CustomerOrganizationId.HasValue)
                expr = expr.And(x => x.CustomerOrganizationId == filters.CustomerOrganizationId);

            if (filters.ProjectManagerId.HasValue) expr = expr.And(x => x.ProjectManagerId == filters.ProjectManagerId);

            if (filters.ProjectManagerOrganizationId.HasValue)
                expr = expr.And(x => x.ProjectManagerOrganizationId == filters.ProjectManagerOrganizationId);

            if (filters.AccountManagerId.HasValue) expr = expr.And(x => x.AccountManagerId == filters.AccountManagerId);

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.AccountManagerOrganizationId == filters.AccountManagerOrganizationId);

            return expr;
        }
    }
}