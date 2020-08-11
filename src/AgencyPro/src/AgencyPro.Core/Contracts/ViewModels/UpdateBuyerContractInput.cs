// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class UpdateBuyerContractInput
    {
        [Range(1, 100)]
        public virtual int MaxWeeklyHours { get; set; }

    }
}