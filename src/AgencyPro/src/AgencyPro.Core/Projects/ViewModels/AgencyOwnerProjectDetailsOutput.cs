// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.Invoices.ViewModels;
using AgencyPro.Core.PaymentTerms.ViewModels;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.Projects.ViewModels
{
    public class AgencyOwnerProjectDetailsOutput : AgencyOwnerProjectOutput
    {
        public AgencyOwnerFixedPriceProposalModel Proposal { get; set; }
        public ICollection<AgencyOwnerProviderContractOutput> Contracts { get; set; }
        public ICollection<AgencyOwnerStoryOutput> Stories { get; set; }
        public ICollection<AgencyOwnerProjectInvoiceOutput> Invoices { get; set; }
        public ICollection<CommentOutput> Comments { get; set; }

        public IDictionary<TimeStatus, int> TimeEntryStatus { get; set; }
        public IDictionary<StoryStatus, int> StoryStatus { get; set; }
        public IDictionary<ContractStatus, int> ContractStatus { get; set; }

        public ICollection<BillingCategoryOutput> AvailableBillingCategories { get; set; }
        public ICollection<PaymentTermOutput> AvailablePaymentTerms { get; set; }
        public ICollection<int> BillingCategoryIds { get; set; }
    }
}