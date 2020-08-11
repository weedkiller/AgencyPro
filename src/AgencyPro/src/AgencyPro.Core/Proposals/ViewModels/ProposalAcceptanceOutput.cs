// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Proposals.Enums;
using System;

namespace AgencyPro.Core.Proposals.ViewModels
{
    public class ProposalAcceptanceOutput
    {
        public DateTimeOffset AcceptedCompletionDate { get; set; }

        public Guid Id { get; set; }

        public decimal TotalCost { get; set; }

        public int NetTerms { get; set; }

        public string ProposalBlob { get; set; }
        public decimal CustomerRate { get; set; }
        public decimal TotalDays { get; set; }
        public string AgreementText { get; set; }
        public ProposalType ProposalType { get; set; }
    }
}
