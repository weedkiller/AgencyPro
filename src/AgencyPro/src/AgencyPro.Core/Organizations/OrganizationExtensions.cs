// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.Organizations
{
    public static class OrganizationExtensions
    {
        public static IQueryable<Organization> ApplyWhereFilters(this IQueryable<Organization> entities,
            OrganizationFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        public static Expression<Func<Organization, bool>> WhereFilter(OrganizationFilters filters)
        {
            var expr = PredicateBuilder.True<Organization>();

            if (filters.CategoryId.HasValue)
                expr = expr.And(x => x.CategoryId == filters.CategoryId.Value);

            return expr;
        }
    }
}
