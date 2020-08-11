// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Roles.Extensions
{
    public static partial class CustomerExtensions
    {
        public static IQueryable<Customer> ForOrganizationMarketer(this IQueryable<Customer> entities,
            IOrganizationMarketer ma)
        {
            return entities.Where(x => x.MarketerOrganizationId == ma.OrganizationId);
        }
    }
}