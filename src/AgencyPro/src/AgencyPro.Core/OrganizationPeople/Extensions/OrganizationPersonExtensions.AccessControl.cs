// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.OrganizationPeople.Extensions
{
    public static partial class OrganizationPersonExtensions
    {
        public static IQueryable<OrganizationPerson> ForOrganizationMarketer(
            this IQueryable<OrganizationPerson> entities,
            IOrganizationMarketer ma)
        {
            return entities.Where(x => x.OrganizationId == ma.OrganizationId);
        }

        public static IQueryable<OrganizationPerson> ForAgencyOwner(this IQueryable<OrganizationPerson> entities,
            IAgencyOwner ao)
        {
            return entities.Where(x => x.OrganizationId == ao.OrganizationId);
        }

       
        public static IQueryable<OrganizationPerson> ForPerson(this IQueryable<OrganizationPerson> entities,
            Guid personId)
        {
            return entities.Where(x => x.PersonId == personId);
        }

        public static IQueryable<OrganizationPerson> ForOrganizationAccountManager(
            this IQueryable<OrganizationPerson> entities,
            IOrganizationAccountManager am)
        {
            return entities.Where(x => x.OrganizationId == am.OrganizationId);
        }

        public static IQueryable<OrganizationPerson> ForOrganizationProjectManager(
            this IQueryable<OrganizationPerson> entities,
            IOrganizationProjectManager pm)
        {
            return entities.Where(x => x.OrganizationId == pm.OrganizationId);
        }

        public static IQueryable<OrganizationPerson> ForOrganizationRecruiter(
            this IQueryable<OrganizationPerson> entities,
            IOrganizationRecruiter re)
        {
            return entities.Where(x => x.OrganizationId == re.OrganizationId);
        }

        public static IQueryable<OrganizationPerson> ForOrganizationContractor(
            this IQueryable<OrganizationPerson> entities,
            IOrganizationContractor co)
        {
            return entities.Where(x => x.OrganizationId == co.OrganizationId);
        }
    }
}