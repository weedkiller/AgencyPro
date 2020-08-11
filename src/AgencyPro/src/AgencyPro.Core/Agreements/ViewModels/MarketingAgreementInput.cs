// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Agreements.ViewModels
{
    public class MarketingAgreementInput
    {
        public decimal? MarketerStream { get; set; }
        public decimal? MarketingAgencyBonus { get; set; }
        public decimal? MarketerBonus { get; set; }
        public decimal? MarketingAgencyStream { get; set; }
    }
}