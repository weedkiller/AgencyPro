// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    public class MarketingAgencyOwnerTimeMatrixComposedOutput
    {
        public ICollection<MarketingAgencyOwnerTimeMatrixOutput> Matrix { get; set; }
        public ICollection<AgencyOwnerOrganizationMarketerOutput> Marketers { get; set; }
    }
}