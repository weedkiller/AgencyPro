// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class AddExistingPersonInput
    {
        public bool IsContractor { get; set; }
        public bool IsProjectManager { get; set; }
        public bool IsAccountManager { get; set; }
        public bool IsRecruiter { get; set; }
        public bool IsMarketer { get; set; }

        public decimal? AccountManagerStream { get; set; }
        public decimal? ProjectManagerStream { get; set; }
        public decimal? ContractorStream { get; set; }
        public decimal? RecruiterStream { get; set; }
        public decimal? MarketerStream { get; set; }

        public decimal? RecruiterBonus { get; set; }
        public decimal? MarketerBonus { get; set; }

        public string EmailAddress { get; set; }
    }
}