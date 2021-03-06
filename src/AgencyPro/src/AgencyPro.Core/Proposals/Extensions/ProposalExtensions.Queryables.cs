﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Proposals.Filters;
using AgencyPro.Core.Proposals.Models;

namespace AgencyPro.Core.Proposals.Extensions
{
    public static partial class ProposalExtensions
    {
        public static IQueryable<T> ApplyWhereFilters<T>(this IQueryable<T> entities,
            ProposalFilters filters) where T : FixedPriceProposal
        {
            return entities.Where(WhereFilter<T>(filters));
        }

        private static Expression<Func<T, bool>> WhereFilter<T>(ProposalFilters filters)
        where T : FixedPriceProposal
        {
            var expr = PredicateBuilder.True<T>();

            if (filters.CustomerId.HasValue)
                expr = expr.And(x => x.Project.CustomerId == filters.CustomerId);

            if (filters.CustomerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.CustomerOrganizationId == filters.CustomerOrganizationId);

            if (filters.ProjectManagerId.HasValue)
                expr = expr.And(x => x.Project.ProjectManagerId == filters.ProjectManagerId);

            if (filters.ProjectManagerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.ProjectManagerOrganizationId == filters.ProjectManagerOrganizationId);

            if (filters.AccountManagerId.HasValue)
                expr = expr.And(x => x.Project.AccountManagerId == filters.AccountManagerId);

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.AccountManagerOrganizationId == filters.AccountManagerOrganizationId);

            if (filters.CustomerId.HasValue)
                expr = expr.And(x => x.Project.CustomerId == filters.CustomerId);

            if (filters.CustomerOrganizationId.HasValue)
                expr = expr.And(x => x.Project.CustomerOrganizationId == filters.CustomerOrganizationId);

            if (filters.ProjectId.HasValue)
                expr = expr.And(x => x.Project.Id == filters.ProjectId);


            return expr;
        }
    }
}