// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Linq.Expressions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Leads.Filters;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Leads.Extensions
{
    public static class LeadExtensions
    {
        public static IQueryable<Lead> ForOrganizationAccountManager(this IQueryable<Lead> entities,
            IOrganizationAccountManager am)
        {
            return entities.Where(x =>
                x.AccountManagerId == am.AccountManagerId && x.AccountManagerOrganizationId == am.OrganizationId);
        }

        public static IQueryable<Lead> ForOrganizationMarketer(this IQueryable<Lead> entities,
            IOrganizationMarketer ma)
        {
            return entities.Where(x =>
                x.MarketerId == ma.MarketerId && x.MarketerOrganizationId == ma.OrganizationId);
        }

        public static IQueryable<Lead> ForAgencyOwner(this IQueryable<Lead> entities,
            IProviderAgencyOwner ao)
        {
            return entities.Where(x => x.ProviderOrganizationId == ao.OrganizationId);
        }


        public static IQueryable<Lead> ForMarketingAgencyOwner(this IQueryable<Lead> entities,
            IMarketingAgencyOwner ao)
        {
            return entities.Where(x => x.MarketerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Lead> ApplyWhereFilters(this IQueryable<Lead> entities,
            LeadFilters filters)
        {
            return entities.Where(WhereFilter(filters));
        }

        public static Expression<Func<Lead, bool>> WhereFilter(LeadFilters filters)
        {
            var expr = PredicateBuilder.True<Lead>();

            if (filters.AccountManagerId.HasValue)
                expr = expr.And(x => x.AccountManagerId == filters.AccountManagerId.Value);

            if (filters.AccountManagerOrganizationId.HasValue)
                expr = expr.And(x => x.AccountManagerOrganizationId == filters.AccountManagerOrganizationId.Value);

            return expr;
        }

    }
}