// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Contracts.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "contracts/marketing-contracts")]
    public class AgencyOwnerMarketingContractOutput : MarketingContractOutput
    {
        public override Guid TargetOrganizationId => MarketerOrganizationId;
        public override decimal MarketingAgencyStream { get; set; }
        public override decimal MaxMarketerWeekly { get; set; }
        public override decimal MaxMarketingAgencyWeekly { get; set; }

        public override int MarketingNumber { get; set; }
        public override Guid MarketerId { get; set; }
        public override Guid MarketerOrganizationId { get; set; }
        public override string CustomerName { get; set; }
        public override string CustomerImageUrl { get; set; }
        public override string CustomerOrganizationName { get; set; }
        public override string CustomerOrganizationImageUrl { get; set; }
        public override Guid CustomerOrganizationId { get; set; }
        public override Guid CustomerId { get; set; }
        public override string MarketerName { get; set; }
        public override string MarketerImageUrl { get; set; }
        public override string MarketerOrganizationName { get; set; }
        public override string MarketerOrganizationImageUrl { get; set; }
        public override decimal TotalHoursLogged { get; set; }
        public override decimal TotalApprovedHours { get; set; }
        public override decimal MarketerStream { get; set; }

        public override Guid TargetPersonId => Guid.Empty;
    }
}