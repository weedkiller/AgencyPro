// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Contracts.Filters;
using AgencyPro.Core.Notifications.Models;

namespace AgencyPro.Core.ActivityFeed.Extensions
{
    public static class ActivityFeedExtensions
    {
        public static IQueryable<Notification> ApplyWhereFilters(this IQueryable<Notification> entities,
            ContractFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        private static Expression<Func<Notification, bool>> WhereFilter(ContractFilters filters)
        {
            var expr = PredicateBuilder.True<Notification>();

           

            return expr;
        }

    }
}
