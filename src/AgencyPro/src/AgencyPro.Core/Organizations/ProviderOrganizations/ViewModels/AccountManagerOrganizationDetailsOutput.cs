// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.OrganizationPeople.ViewModels;

namespace AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels
{
    public class AccountManagerOrganizationDetailsOutput : AccountManagerOrganizationOutput
    {
        public ICollection<AccountManagerOrganizationPersonOutput> OrganizationPeople { get; set; }

        public ICollection<BillingCategoryOutput> AvailableBillingCategories { get; set; }
        public ICollection<BillingCategoryOutput> BillingCategories { get; set; }

    }
}