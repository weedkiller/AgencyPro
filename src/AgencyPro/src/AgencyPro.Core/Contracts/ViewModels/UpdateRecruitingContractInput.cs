// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class UpdateRecruitingContractInput
    {

        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? RecruiterStream { get; set; }


        [Range(0, 100)]
        [DataType(DataType.Currency)]
        public virtual decimal? RecruitingAgencyStream { get; set; }

    }
}