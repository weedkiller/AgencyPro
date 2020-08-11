// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Orders.Model;

namespace AgencyPro.Core.Orders
{
    public static partial class WorkOrderExtensions
    {
        public static IQueryable<WorkOrder> ApplyWhereFilters(this IQueryable<WorkOrder> entities,
            WorkOrderFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        public static Expression<Func<WorkOrder, bool>> WhereFilter(WorkOrderFilters filters)
        {
            var expr = PredicateBuilder.True<WorkOrder>();

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.AccountManagerOrganizationId == filters.AccountManagerOrganizationId);
            
            return expr;
        }
    }
}