// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Common;

namespace AgencyPro.Core.Candidates.Extensions
{
    public static partial class CandidateExtensions
    {
        public static IQueryable<Candidate> FindById(this IQueryable<Candidate> entities, Guid id)
        {
            return entities.Where(x => x.Id == id);
        }

        public static IQueryable<Candidate> ApplyWhereFilters(this IQueryable<Candidate> entities,
            CandidateFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        public static Expression<Func<Candidate, bool>> WhereFilter(CandidateFilters filters)
        {
            var expr = PredicateBuilder.True<Candidate>();

            if (filters.ProjectManagerId.HasValue)
                expr = expr.And(x => x.ProjectManagerId == filters.ProjectManagerId.Value);

            if (filters.ProjectManagerOrganizationId.HasValue)
                expr = expr.And(x => x.ProjectManagerOrganizationId == filters.ProjectManagerOrganizationId.Value);

            return expr;
        }
    }
}