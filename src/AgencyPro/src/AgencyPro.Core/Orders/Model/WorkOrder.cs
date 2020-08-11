// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Orders.Model
{
    public class WorkOrder : AuditableEntity, IWorkOrder
    {
        public ICollection<ProposalWorkOrder> Proposals { get; set; }
        public ICollection<WorkOrderNotification> WorkOrderNotifications { get; set; }
        public CustomerAccount CustomerAccount { get; set; }

        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }

        public AccountManager AccountManager { get; set; }
        public OrganizationAccountManager OrganizationAccountManager { get; set; }

        public ProviderOrganization ProviderOrganization { get; set; }
        public Organization BuyerOrganization { get; set; }

        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }

        public Customer Customer { get; set; }

        public Guid Id { get; set; }

        public int BuyerNumber { get; set; }
        public int ProviderNumber { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? ProviderResponseTime { get; set; }
        
        public OrderStatus Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}