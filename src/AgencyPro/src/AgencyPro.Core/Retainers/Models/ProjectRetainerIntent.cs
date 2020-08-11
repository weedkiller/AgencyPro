// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Retainers.Models
{
    public class ProjectRetainerIntent : BaseObjectState
    {
        public CustomerAccount CustomerAccount { get; set; }
        public AccountManager AccountManager { get; set; }
        public Guid AccountManagerId { get; set; }
        public Organization ProviderOrganization { get; set; }
        public Guid ProviderOrganizationId { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }
        public OrganizationCustomer OrganizationCustomer { get; set; }
        public Organization CustomerOrganization { get; set; }
        public Customer Customer { get; set; }
        public OrganizationAccountManager OrganizationAccountManager { get; set; }
        public ICollection<StripeCharge> Credits { get; set; }
        public decimal TopOffAmount { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
