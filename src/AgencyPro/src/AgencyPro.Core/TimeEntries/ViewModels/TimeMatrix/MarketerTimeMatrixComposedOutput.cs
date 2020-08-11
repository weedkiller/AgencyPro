// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class MarketerTimeMatrixComposedOutput
    {
        public ICollection<MarketerTimeMatrixOutput> Matrix { get; set; }

        public ICollection<MarketerOrganizationCustomerOutput> Customers { get; set; }
        public ICollection<MarketerOrganizationContractorOutput> Contractors { get; set; }
        public ICollection<MarketerContractOutput> Contracts { get; set; }
    }
}