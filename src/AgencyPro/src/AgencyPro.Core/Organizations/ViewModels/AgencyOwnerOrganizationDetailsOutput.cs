// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BuyerAccounts.ViewModels;
using AgencyPro.Core.FinancialAccounts.ViewModels;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class AgencyOwnerOrganizationDetailsOutput : OrganizationOutput
    {
        public AgencyOwnerProviderOrganizationDetailsOutput ProviderOrganizationDetails { get; set; }
        public AgencyOwnerMarketingOrganizationDetailsOutput MarketingOrganizationDetails { get; set; }
        public AgencyOwnerRecruitingOrganizationDetailsOutput RecruitingOrganizationDetails { get; set; }

        public FinancialAccountOutput FinancialAccount { get; set; }
        public BuyerAccountOutput BuyerAccount { get; set; }
    }
}