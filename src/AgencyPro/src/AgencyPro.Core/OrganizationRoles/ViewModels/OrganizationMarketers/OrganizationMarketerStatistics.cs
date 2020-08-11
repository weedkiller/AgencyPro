// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers
{
    public class OrganizationMarketerStatistics : OrganizationMarketerOutput
    {
        public int TotalLeads { get; set; }
        public int TotalContracts { get; set; }
        public int TotalCustomers { get; set; }
    }
}