// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Roles.Extensions
{
    public static partial class ContractorExtensions
    {
        public static IQueryable<Contractor> StandardIncludes(this IQueryable<Contractor> entities)
        {
            return entities;
        }

        public static IQueryable<Contractor> IncludeDetailedIncludes(this IQueryable<Contractor> entities)
        {
            return entities;
        }
    }
}