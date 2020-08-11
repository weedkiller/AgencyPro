// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Proposals.Enums;

namespace AgencyPro.Core.Proposals.Services
{
    public interface IProposal
    {
       
        Guid Id { get; set; }
        decimal VelocityBasis { get; set; }
        decimal WeeklyMaxHourBasis { get; set; }
        string AgreementText { get; set; }
        decimal? BudgetBasis { get; set; }
        ProposalStatus Status { get; set; }
        ProposalType ProposalType { get; set; }
    }
}