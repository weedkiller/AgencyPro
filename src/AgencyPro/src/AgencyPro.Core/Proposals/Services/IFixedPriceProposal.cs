// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Proposals.Services
{
    public interface IFixedPriceProposal : IProposal
    {
        int StoryPointBasis { get; set; }
        int EstimationBasis { get; set; }
        decimal OtherPercentBasis { get; set; }
        int ExtraDayBasis { get; set; }
        decimal CustomerRateBasis { get; set; }
        int StoryHours { get; set; }
        decimal TotalHours { get; set; }
        decimal TotalPriceQuoted { get; set; }
    }
}