﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.Orders.ViewModels;
using AgencyPro.Core.PaymentTerms.ViewModels;
using AgencyPro.Core.Projects.Enums;
using AgencyPro.Core.Projects.ViewModels;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class AgencyOwnerCustomerAccountDetailsOutput : AgencyOwnerCustomerAccountOutput
    {
        public ICollection<AgencyOwnerProjectOutput> Projects { get; set; }
        public ICollection<AgencyOwnerProviderContractOutput> Contracts { get; set; }
        public ICollection<AgencyOwnerProjectInvoiceOutput> Invoices { get; set; }
        public ICollection<ProviderWorkOrderOutput> WorkOrders { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }

       

        public ICollection<PaymentTermOutput> AvailablePaymentTerms { get; set; }
        public IDictionary<ProjectStatus, int> ProjectSummary { get; set; }
        public IDictionary<OrderStatus, int> WorkOrderSummary { get; set; }
        public IDictionary<string,int> InvoiceSummary { get; set; }
        public IDictionary<ContractStatus, int> ContractSummary { get; set; }
    }
}