// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.Common;

namespace AgencyPro.Core.BillingCategories.Extensions
{
    public static class BillingCategoryExtensions
    {
        public static IQueryable<BillingCategory> ApplyWhereFilters(this IQueryable<BillingCategory> entities,
            BillingCategoryFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        private static Expression<Func<BillingCategory, bool>> WhereFilter(BillingCategoryFilters filters)
        {
            var expr = PredicateBuilder.True<BillingCategory>();
            
            return expr;
        }
    }
}
