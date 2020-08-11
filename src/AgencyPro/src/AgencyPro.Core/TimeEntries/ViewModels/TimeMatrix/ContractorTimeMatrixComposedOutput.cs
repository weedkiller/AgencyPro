// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class ContractorTimeMatrixComposedOutput
    {
        public ICollection<ContractorTimeMatrixOutput> Matrix { get; set; }

        public ICollection<ContractorOrganizationAccountManagerOutput> AccountManagers { get; set; }
        public ICollection<ContractorOrganizationRecruiterOutput> Recruiters { get; set; }
        public ICollection<ContractorOrganizationProjectManagerOutput> ProjectManagers { get; set; }
        public ICollection<ContractorOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<OrganizationContractorOutput> Contractors { get; set; }
        public ICollection<ContractorContractOutput> Contracts { get; set; }
        public ICollection<ContractorProjectOutput> Projects { get; set; }
        public ICollection<ContractorStoryOutput> Stories { get; set; }
    }
}