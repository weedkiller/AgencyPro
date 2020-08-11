// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers
{
    public class OrganizationCustomerOutput : OrganizationCustomerInput, IOrganizationCustomer, IAgencyOwner, IMarketingAgencyOwner, IRecruitingAgencyOwner, IProviderAgencyOwner
    {
        public virtual string OrganizationImageUrl { get; set; }
        public virtual string OrganizationName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ImageUrl { get; set; }
        public bool IsMarketingOwner { get; set; }
        public bool IsRecruitingOwner { get; set; }
        public bool IsProviderOwner { get; set; }

        [JsonIgnore]
        public string StripeCustomerId { get; }
    }
}