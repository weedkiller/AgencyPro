// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Extensions
{
    public static class LinqQueryBuilder
    {
        public static Expression<Func<Story, bool>> FromFilter(StoryFilters filters)
        {
            var expr = PredicateBuilder.True<Story>();
            if (filters.ProjectId.HasValue) expr = expr.And(x => x.ProjectId == filters.ProjectId);

            return expr;
        }

        public static Expression<Func<ProjectInvoice, bool>> FromFilter(InvoiceFilters filters)
        {
            var expr = PredicateBuilder.True<ProjectInvoice>();

            return expr;
        }
    }
}