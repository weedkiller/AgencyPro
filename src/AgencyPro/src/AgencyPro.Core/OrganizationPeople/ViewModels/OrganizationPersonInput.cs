// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class OrganizationPersonInput
    {
        public Guid PersonId { get; set; }
        
        public virtual bool IsAccountManager { get; set; }
        public virtual bool IsProjectManager { get; set; }
        public virtual bool IsContractor { get; set; }
        public virtual bool IsCustomer { get; set; }
        public virtual bool IsRecruiter { get; set; }
        public virtual bool IsMarketer { get; set; }

        public virtual decimal? ContractorStream { get; set; }

        public virtual decimal? MarketerStream { get; set; }
        public virtual decimal? MarketerBonus { get; set; }

        public virtual decimal? RecruiterStream { get; set; }
        public virtual decimal? RecruiterBonus { get; set; }

        public virtual decimal? ProjectManagerStream { get; set; }
        public virtual decimal? AccountManagerStream { get; set; }

    }
}