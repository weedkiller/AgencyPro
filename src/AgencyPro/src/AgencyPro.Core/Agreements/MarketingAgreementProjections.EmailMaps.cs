// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Emails;
using AgencyPro.Core.Agreements.Models;

namespace AgencyPro.Core.Agreements
{
    public partial class MarketingAgreementProjections
    {
        private void CreateEmailMaps()
        {
            CreateMap<MarketingAgreement, MarketingAgencyAgreementEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.MarketingOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.MarketingOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.MarketingOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<MarketingAgreement, ProviderMarketingAgreementEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

        }
    }
}