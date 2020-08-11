// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;

namespace AgencyPro.Core.OrganizationPeople
{
    public class OrganizationPeopleFilters : CommonFilters
    {
        public static readonly OrganizationPeopleFilters NoFilter = new OrganizationPeopleFilters();

        public bool? ProjectManagers { get; set; }
        public bool? AccountManagers { get; set; }
        public bool? Marketers { get; set; }
        public bool? Recruiters { get; set; }
        public bool? Contractors { get; set; }
        public bool? Customers { get; set; }
    }
}