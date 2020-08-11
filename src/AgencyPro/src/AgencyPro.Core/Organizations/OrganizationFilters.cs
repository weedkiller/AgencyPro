// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Organizations
{
    public class OrganizationFilters
    {
        public static readonly OrganizationFilters NoFilter = new OrganizationFilters();

        public Guid[] Skills { get; set; }

        public int? CategoryId { get; set; }
        

    }
}
