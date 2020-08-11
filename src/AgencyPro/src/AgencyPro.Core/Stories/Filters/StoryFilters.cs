// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using AgencyPro.Core.Stories.Enums;
using System;

namespace AgencyPro.Core.Stories.Filters
{
    public class StoryFilters : CommonFilters
    {
        public static readonly StoryFilters NoFilter = new StoryFilters();

        public Guid? ProjectId { get; set; }



        public StoryFilters()
        {
            StoryStatus = new StoryStatus[] { };
        }

        public StoryStatus[] StoryStatus { get; set; }
        public Guid? AccountManagerId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? ContractorId { get; set; }
        public Guid? ContractorOrganizationId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
    }
}