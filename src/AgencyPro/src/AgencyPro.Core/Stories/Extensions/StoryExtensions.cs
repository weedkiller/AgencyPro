// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Stories.Extensions
{
    public static partial class StoryExtensions
    {
        public static IQueryable<Story> ApplyWhereFilters(this IQueryable<Story> entities,
            StoryFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        private static Expression<Func<Story, bool>> WhereFilter(StoryFilters filters)
        {
            var expr = PredicateBuilder.True<Story>();

            if (filters.CustomerId.HasValue)
                expr = expr.And(x => x.Project.CustomerId == filters.CustomerId);

            if (filters.CustomerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.CustomerOrganizationId == filters.CustomerOrganizationId);

            if (filters.ContractorId.HasValue)
                expr = expr.And(x => x.ContractorId == filters.ContractorId);

            if (filters.ContractorOrganizationId.HasValue)
                expr = expr.And(x => x.ContractorOrganizationId == filters.ContractorOrganizationId);

            if (filters.AccountManagerId.HasValue)
                expr = expr.And(x => x.Project.AccountManagerId == filters.AccountManagerId);

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.AccountManagerOrganizationId == filters.AccountManagerOrganizationId);

            if (filters.ProjectId.HasValue)
                expr = expr.And(x => x.ProjectId == filters.ProjectId);

            if (filters.StoryStatus.Any())
                expr = expr.And(x => filters.StoryStatus.Contains(x.Status));

            return expr;
        }

        public static IQueryable<Story> FindById(this IQueryable<Story> entities, Guid id)
        {
            return entities.Where(x => x.Id == id);
        }
    }
}