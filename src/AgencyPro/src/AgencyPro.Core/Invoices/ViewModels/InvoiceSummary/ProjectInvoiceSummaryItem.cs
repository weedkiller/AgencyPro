// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class ProjectInvoiceSummaryItem
    {
        
        public virtual string ProjectName { get; set; }
        public virtual Guid ProjectId { get; set; }
        public virtual string ProjectAbbreviation { get; set; }
        public virtual decimal ApprovedHours { get; set; }
        public virtual decimal ApprovedCustomerAmount { get; set; }
     
        public virtual Guid CustomerOrganizationId { get; set; }
        public virtual Guid CustomerId { get; set; }
        public virtual string CustomerImageUrl { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerOrganizationName { get; set; }
        public virtual string CustomerOrganizationImageUrl { get; set; }

    }
}