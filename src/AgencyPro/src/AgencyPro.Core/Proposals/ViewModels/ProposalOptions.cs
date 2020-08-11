﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Proposals.ViewModels
{
    public class ProposalOptions
    {
        public virtual int? StoryPointBasis { get; set; }
        public virtual int? EstimationBasis { get; set; }
        public virtual decimal OtherPercentBasis { get; set; }
        public virtual decimal? CustomerRateBasis { get; set; }
        public virtual decimal VelocityBasis { get; set; } = 1m;
        public virtual decimal? WeeklyMaxHourBasis { get; set; }
        public virtual string AgreementText { get; set; }
        public virtual decimal? BudgetBasis { get; set; }
        public virtual bool RequiresRetainer { get; set; }
        public virtual decimal? RetainerPercent { get; set; }

        public int[] WorkOrderIds { get; set; }
    }
}