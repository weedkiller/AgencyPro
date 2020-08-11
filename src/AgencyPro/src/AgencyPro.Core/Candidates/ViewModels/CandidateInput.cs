// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Candidates.ViewModels
{
    public class CandidateInput
    {
        [Required]
        public virtual string FirstName { get; set; }

        [Required]
        public virtual string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string EmailAddress { get; set; }

        [Required]
        public virtual string PhoneNumber { get; set; }
        
        public virtual bool IsContacted { get; set; }

        public virtual string Description { get; set; }
        public virtual string ReferralCode { get; set; }
        public virtual string Iso2 { get; set; }
        public virtual string ProvinceState { get; set; }
    }
}