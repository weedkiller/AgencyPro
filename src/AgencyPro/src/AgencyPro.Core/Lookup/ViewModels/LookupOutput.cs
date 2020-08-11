// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.Lookup.ViewModels
{
    public class LookupOutput
    {
        public IEnumerable<EnumOutput<int>> OrganizationTypes { get; set; }
        public IEnumerable<EnumOutput<byte>> InvoiceStatus { get; set; }
        public IEnumerable<EnumOutput<byte>> ContractStatus { get; set; }
        public IEnumerable<EnumOutput<int>> EstimatorStatus { get; set; }
        public IEnumerable<EnumOutput<int>> IntakeStatus { get; set; }
        public IEnumerable<EnumOutput<int>> ApplicationStatus { get; set; }
        public IEnumerable<EnumOutput<int>> OpeningStatus { get; set; }
        public IEnumerable<EnumOutput<int>> PaymentStatus { get; set; }
        public IEnumerable<EnumOutput<byte>> PaymentTypes { get; set; }
        public IEnumerable<EnumOutput<int>> AffiliateTypes { get; set; }
        public IEnumerable<EnumOutput<int>> ProjectMemberTypes { get; set; }
        public IEnumerable<EnumOutput<int>> TimeStatuses { get; set; }
        public IEnumerable<EnumOutput<int>> TimeTypes { get; set; }
        public IEnumerable<EnumOutput<int>> Roles { get; set; }
        public IEnumerable<EnumOutput<int>> ProjectStatus { get; set; }
        public IEnumerable<EnumOutput<int>> LeadStatus { get; set; }
        public IEnumerable<EnumOutput<byte>> CandidateStatus { get; set; }
        public IEnumerable<EnumOutput<int>> AccountStatus { get; set; }
        public IEnumerable<EnumOutput<int>> ProposalStatus { get; set; }
        public IEnumerable<EnumOutput<int>> ProposalTypes { get; set; }
        public IEnumerable<EnumOutput<int>> DocumentTypes { get; set; }
        public IEnumerable<EnumOutput<int>> StoryStatus { get; set; }
        public IEnumerable<EnumOutput<int>> DistributionStatus { get; set; }
        public IEnumerable<EnumOutput<int>> DistributionType { get; set; }
        public IEnumerable<EnumOutput<int>> PersonStatus { get; set; }
        public IEnumerable<EnumOutput<int>> StreamType { get; set; }
        public IEnumerable<EnumOutput<int>> OrderStatus { get; set; }

    }
}