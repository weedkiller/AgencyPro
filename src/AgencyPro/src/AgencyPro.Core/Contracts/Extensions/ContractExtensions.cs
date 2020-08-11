// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.Contracts.Extensions
{
    public static partial class ContractExtensions
    {
        public static IQueryable<Contract> ForOrganizationMarketer(this IQueryable<Contract> entities,
            IOrganizationMarketer ma)
        {
            return entities.Where(x => x.MarketerId == ma.MarketerId && x.MarketerOrganizationId == ma.OrganizationId);
        }

        public static IQueryable<Contract> ForAgencyOwner(this IQueryable<Contract> entities,
            IProviderAgencyOwner ao)
        {
            return entities.Where(x => x.ContractorOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Contract> ForAgencyOwner(this IQueryable<Contract> entities,
            IRecruitingAgencyOwner ao)
        {
            return entities.Where(x => x.RecruiterOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Contract> ForAgencyOwner(this IQueryable<Contract> entities,
            IMarketingAgencyOwner ao)
        {
            return entities.Where(x => x.MarketerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<Contract> ForOrganizationAccountManager(this IQueryable<Contract> entities,
            IOrganizationAccountManager am)
        {
            return entities.Where(x =>
                x.AccountManagerId == am.AccountManagerId &&
                x.ContractorOrganizationId == am.OrganizationId);
        }

        public static IQueryable<Contract> ForOrganizationProjectManager(this IQueryable<Contract> entities,
            IOrganizationProjectManager pm)
        {
            return entities.Where(x =>
                x.ProjectManagerId == pm.ProjectManagerId &&
                x.ProjectManagerOrganizationId == pm.OrganizationId);
        }

        public static IQueryable<Contract> ForOrganizationRecruiter(this IQueryable<Contract> entities,
            IOrganizationRecruiter re)
        {
            return entities.Where(
                x => x.RecruiterId == re.RecruiterId && x.RecruiterOrganizationId == re.OrganizationId);
        }

        public static IQueryable<Contract> ForOrganizationContractor(this IQueryable<Contract> entities,
            IOrganizationContractor co)
        {
            return entities.Where(x =>
                x.ContractorId == co.ContractorId && x.ContractorOrganizationId == co.OrganizationId);
        }

        public static IQueryable<Contract> ForOrganizationCustomer(this IQueryable<Contract> entities,
            IOrganizationCustomer cu)
        {
            return entities.Where(x =>
                x.CustomerId == cu.CustomerId && x.BuyerOrganizationId == cu.OrganizationId);
        }
    }
}