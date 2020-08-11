// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Invoices.ViewModels
{
    public class ContractInvoiceSummaryItem
    {
        public Guid ContractId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorOrganizationName { get; set; }
        public string ContractorImageUrl { get; set; }
        public string ContractorOrganizationImageUrl { get; set; }
        public Guid ContractorId { get; set; }
        public Guid ContractorOrganizationId { get; set; }
        public decimal ApprovedHours { get; set; }
        public decimal ApprovedCustomerAmount { get; set; }
    }
}