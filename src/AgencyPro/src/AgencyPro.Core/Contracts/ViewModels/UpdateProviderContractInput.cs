// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class UpdateProviderContractInput
    {
        [Range(1,100)]
        public virtual int MaxWeeklyHours { get; set; }

        [Range(1, 999)]
        [DataType(DataType.Currency)]
        public virtual decimal? ContractorStream { get; set; }

        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? ProjectManagerStream { get; set; }

        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? AccountManagerStream { get; set; }

        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? AgencyStream { get; set; }
    }
}