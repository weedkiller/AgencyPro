// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Common;

namespace AgencyPro.Core.Projects.Filters
{
    public class ProjectFilters : CommonFilters
    {
        public static readonly ProjectFilters NoFilter = new ProjectFilters();

        public Guid? CustomerId { get; set; }
        public Guid? CustomerOrganizationId { get; set; }
        public Guid? AccountManagerId { get; set; }
        public Guid? AccountManagerOrganizationId { get; set; }
        public Guid? ProjectManagerId { get; set; }
        public Guid? ProjectManagerOrganizationId { get; set; }
    }
}