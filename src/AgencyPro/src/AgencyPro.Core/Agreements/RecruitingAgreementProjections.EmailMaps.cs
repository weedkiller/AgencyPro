// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Emails;
using AgencyPro.Core.Agreements.Models;

namespace AgencyPro.Core.Agreements
{
    public partial class RecruitingAgreementProjections
    {
        private void CreateEmails()
        {
            CreateMap<RecruitingAgreement, RecruitingAgencyAgreementEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.RecruitingOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.RecruitingOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.RecruitingOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<RecruitingAgreement, ProviderRecruitingAgreementEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

        }
    }
}