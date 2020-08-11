// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.Leads.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers
{
    public class AgencyOwnerOrganizationAccountManagerDetailsOutput : OrganizationAccountManagerStatistics
    {
        public virtual IList<AgencyOwnerCustomerAccountOutput> Accounts { get; set; }
        public virtual IList<AgencyOwnerLeadOutput> Leads { get; set; }


    }
}