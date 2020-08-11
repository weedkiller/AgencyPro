// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using System.Collections.Generic;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class RecruiterTimeMatrixComposedOutput
    {
        public ICollection<RecruiterTimeMatrixOutput> Matrix { get; set; }

        public ICollection<RecruiterOrganizationAccountManagerOutput> AccountManagers { get; set; }
        public ICollection<RecruiterOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<RecruiterContractOutput> Contracts { get; set; }
    }
}