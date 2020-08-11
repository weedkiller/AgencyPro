// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class AccountManagerTimeMatrixComposedOutput
    {
        public ICollection<AccountManagerTimeMatrixOutput> Matrix { get; set; }

   
        public ICollection<AccountManagerOrganizationProjectManagerOutput> ProjectManagers { get; set; }
        public ICollection<AccountManagerOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<AccountManagerOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<AccountManagerContractOutput> Contracts { get; set; }
        public ICollection<AccountManagerProjectOutput> Projects { get; set; }
        public ICollection<AccountManagerStoryOutput> Stories { get; set; }
    }
}