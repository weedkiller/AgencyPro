// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Proposals.ViewModels
{
    public class ProposalAcceptanceDetailOutput : ProposalAcceptanceOutput
    {
        public string ProjectName { get; set; }
        public string ProjectAbbreviation { get; set; }

        public string ProjectManagerName { get; set; }
        public string ProjectManagerOrganizationName { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectManagerOrganizationId { get; set; }
        public string ProjectManagerImageUrl { get; set; }
        public string ProjectManagerOrganizationImageUrl { get; set; }

        public string AccountManagerName { get; set; }
        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }
        public string AccountManagerOrganizationName { get; set; }
        public string AccountManagerImageUrl { get; set; }
        public string AccountManagerOrganizationImageUrl { get; set; }
    }
}