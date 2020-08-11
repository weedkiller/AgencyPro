// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Models;
using System.Linq;
using AgencyPro.Core.Proposals.Enums;

namespace AgencyPro.Core.Proposals.Extensions
{
    public static partial class ProposalExtensions
    {
        public static IQueryable<T> ForAgencyOwner<T>(this IQueryable<T> entities,
            IProviderAgencyOwner ao) where T : FixedPriceProposal
        {
            return entities.Where(x => x.Project.AccountManagerOrganizationId == ao.OrganizationId);
        }

        public static IQueryable<T> ForOrganizationAccountManager<T>(this IQueryable<T> entities,
            IOrganizationAccountManager am) where T : FixedPriceProposal
        {
            return entities.Where(x =>
                x.Project.AccountManagerId == am.AccountManagerId &&
                x.Project.AccountManagerOrganizationId == am.OrganizationId);
        }

        public static IQueryable<T> ForOrganizationProjectManager<T>(this IQueryable<T> entities,
            IOrganizationProjectManager pm) where T : FixedPriceProposal
        {
            return entities.Where(x =>
                x.Project.AccountManagerId == pm.ProjectManagerId &&
                x.Project.ProjectManagerOrganizationId == pm.OrganizationId);
        }

        public static IQueryable<T> ForOrganizationContractor<T>(this IQueryable<T> entities,
            IOrganizationContractor co) where T : FixedPriceProposal
        {
            return entities
                .Where(s => s.Project.Contracts.Any(q =>
                    q.ContractorOrganizationId == co.OrganizationId && q.ContractorId == co.ContractorId));
        }

        public static IQueryable<T> ForOrganizationCustomer<T>(this IQueryable<T> entities,
            IOrganizationCustomer cu) where T : FixedPriceProposal
        {
            return entities.Where(x =>
                x.Project.CustomerId == cu.CustomerId && x.Project.CustomerOrganizationId == cu.OrganizationId && x.Status != ProposalStatus.Draft);
        }
    }
}