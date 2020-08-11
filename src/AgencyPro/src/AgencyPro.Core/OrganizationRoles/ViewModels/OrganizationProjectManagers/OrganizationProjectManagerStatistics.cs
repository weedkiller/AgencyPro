// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers
{
    public class OrganizationProjectManagerStatistics : OrganizationProjectManagerOutput
    {
        public int TotalContracts { get; set; }
        public int TotalProjects { get; set; }
        public int TotalCandidates { get; set; }
    }
}