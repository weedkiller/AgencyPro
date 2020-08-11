// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using System;

namespace AgencyPro.Core.Proposals.Filters
{
    public class ProposalFilters : CommonFilters
    {
        public static readonly ProposalFilters NoFilter = new ProposalFilters();
        public Guid? CustomerId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public Guid? AccountManagerId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public Guid? ProjectManagerOrganizationId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}