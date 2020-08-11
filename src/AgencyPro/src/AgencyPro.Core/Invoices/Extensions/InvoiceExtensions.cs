// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Invoices.Filters;
using AgencyPro.Core.Invoices.Models;

namespace AgencyPro.Core.Invoices.Extensions
{
    public static partial class InvoiceExtensions
    {
        public static IQueryable<ProjectInvoice> ApplyWhereFilters(this IQueryable<ProjectInvoice> entities,
            InvoiceFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        private static Expression<Func<ProjectInvoice, bool>> WhereFilter(InvoiceFilters filters)
        {
            var expr = PredicateBuilder.True<ProjectInvoice>();

            if (filters.AccountManagerId.HasValue)
                expr = expr.And(x => x.AccountManagerId == filters.AccountManagerId);

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.ProviderOrganizationId == filters.AccountManagerOrganizationId);

            if (filters.ProjectManagerId.HasValue)
                expr = expr.And(x => x.ProjectManagerId == filters.ProjectManagerId);

            if (filters.ProjectManagerOrganizationId.HasValue)
                expr = expr.And(x => x.ProviderOrganizationId == filters.ProjectManagerOrganizationId);



            if (filters.CustomerId.HasValue)
                expr = expr.And(x => x.CustomerId == filters.CustomerId);

            if (filters.CustomerOrganizationId.HasValue)
                expr = expr.And(x => x.BuyerOrganizationId == filters.CustomerOrganizationId);



            expr.And(x => x.Invoice.IsDeleted == false);

            return expr;
        }
        
    }
}