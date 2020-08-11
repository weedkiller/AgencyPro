// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class ProviderAgencyOwnerTimeMatrixComposedOutput
    {
        public ICollection<ProviderAgencyOwnerTimeMatrixOutput> Matrix { get; set; }

        public ICollection<AgencyOwnerOrganizationAccountManagerOutput> AccountManagers { get; set; }
        public ICollection<AgencyOwnerOrganizationProjectManagerOutput> ProjectManagers { get; set; }
        public ICollection<AgencyOwnerOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<AgencyOwnerOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<AgencyOwnerProviderContractOutput> Contracts { get; set; }
        public ICollection<AgencyOwnerProjectOutput> Projects { get; set; }
        public ICollection<AgencyOwnerStoryOutput> Stories { get; set; }
    }
}