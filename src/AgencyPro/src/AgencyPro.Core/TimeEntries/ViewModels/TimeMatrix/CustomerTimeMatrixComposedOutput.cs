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
    public class CustomerTimeMatrixComposedOutput
    {
        public ICollection<CustomerTimeMatrixOutput> Matrix { get; set; }

        public ICollection<CustomerOrganizationAccountManagerOutput> AccountManagers { get; set; }
        public ICollection<CustomerOrganizationProjectManagerOutput> ProjectManagers { get; set; }
        public ICollection<CustomerOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<CustomerOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<CustomerContractOutput> Contracts { get; set; }
        public ICollection<CustomerProjectOutput> Projects { get; set; }
        public ICollection<CustomerStoryOutput> Stories { get; set; }
    }
}