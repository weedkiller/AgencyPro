// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationPeople.Filters
{
    public class ProjectManagerFilters
    {
        public static readonly ProjectManagerFilters NoFilter = new ProjectManagerFilters();
        public Guid? Acc { get; set; }
    }
}