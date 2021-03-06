﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class ProjectManagerTimeMatrixComposedOutput
    {
        public ICollection<ProjectManagerTimeMatrixOutput> Matrix { get; set; }

        public ICollection<ProjectManagerOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<ProjectManagerOrganizationAccountManagerOutput> AccountManagers { get; set; }
        public ICollection<ProjectManagerOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<ProjectManagerContractOutput> Contracts { get; set; }
        public ICollection<ProjectManagerProjectOutput> Projects { get; set; }
        public ICollection<ProjectManagerStoryOutput> Stories { get; set; }
    }
}