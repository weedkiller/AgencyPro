// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using AgencyPro.Core.Proposals.Models;

namespace AgencyPro.Core.Proposals.Extensions
{
    public static partial class ProposalExtensions
    {
        public static IQueryable<T> FindById<T>(this IQueryable<T> entities, Guid id)
        where T : FixedPriceProposal
        {
            return entities.Where(x => x.Id == id);
        }
    }
}