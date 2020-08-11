// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.Organizations.Services;

namespace AgencyPro.Core.Orders.ViewModels
{
    public abstract class WorkOrderOutput : WorkOrderInput, IWorkOrder, IOrganizationPersonTarget
    {
        public Guid Id { get; set; }
        public virtual int BuyerNumber { get; set; }
        public virtual int ProviderNumber { get; set; }
       
        public OrderStatus Status { get; set; }
        

        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }
        public DateTimeOffset? ProviderResponseTime { get; set; }

        public virtual string CustomerName { get; set; }
        public virtual string CustomerEmail { get; set; }
        public virtual string CustomerPhoneNumber { get; set; }
        public virtual string CustomerImageUrl { get; set; }
        public virtual string CustomerOrganizationImageUrl { get; set; }
        public virtual string CustomerOrganizationName { get; set; }

        public virtual string AccountManagerName { get; set; }
        public virtual string AccountManagerEmail { get; set; }
        public virtual string AccountManagerPhoneNumber { get; set; }
        public virtual string AccountManagerImageUrl { get; set; }
        public virtual string AccountManagerOrganizationImageUrl { get; set; }
        public virtual string AccountManagerOrganizationName { get; set; }

        public abstract Guid TargetOrganizationId { get; }
        public abstract Guid TargetPersonId { get; }
    }
}
