// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class UpdateMarketingContractInput
    {
        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? MarketerStream { get; set; }

        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? MarketingAgencyStream { get; set; }
    }
}