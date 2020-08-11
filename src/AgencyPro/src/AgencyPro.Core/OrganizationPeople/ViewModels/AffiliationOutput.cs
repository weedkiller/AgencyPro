// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class AffiliationOutput
    {
        public string OrganizationName { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationImageUrl { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public bool IsDefault { get; set; }
        
        public bool IsAgencyOwner { get; set; }
        public bool IsMarketingAgencyOwner { get; set; }
        public bool IsRecruitingAgencyOwner { get; set; }
        public bool IsProviderAgencyOwner { get; set; }
        public bool IsProjectManager { get; set; }
        public bool IsAccountManager { get; set; }
        public bool IsContractor { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsRecruiter { get; set; }
        public bool IsMarketer { get; set; }

        public bool IsAffiliate => IsRecruiter | IsMarketer;
        
        public string AffiliateCode { get; set; }

        public bool IsHidden { get; set; }

        public bool ProviderAgencyFeaturesEnabled { get; set; }
        public bool RecruitingAgencyFeaturesEnabled { get; set; }
        public bool MarketingAgencyFeaturesEnabled { get; set; }
        
    }
}