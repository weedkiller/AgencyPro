// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Leads.ViewModels
{
    public class LeadInput
    {
        public virtual string ReferralCode { get; set; }

        [Required] public virtual string FirstName { get; set; }

        [Required] public virtual string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string EmailAddress { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string Description { get; set; }

        public virtual string OrganizationName { get; set; }

        public string Iso2 { get; set; }

        public string ProvinceState { get; set;}

        public virtual bool IsContacted { get; set; }

        public virtual DateTime? CallbackDate { get; set; }
        public virtual string MeetingNotes { get; set; }
    }
}