// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Leads.Emails;
using AgencyPro.Core.Leads.Models;

namespace AgencyPro.Core.Leads
{
    public partial class LeadProjections
    {
        private void EmailMaps()
        {
            CreateMap<Lead, AgencyOwnerLeadEmail>()
                .ForMember(x=>x.SendMail, opt=>opt.MapFrom(x=>x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<Lead, MarketerLeadEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Marketer.Person.DisplayName));

            CreateMap<Lead, AccountManagerLeadEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x =>x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName));

            CreateMap<Lead, MarketingAgencyOwnerLeadEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.MarketerOrganization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.MarketerOrganization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.MarketerOrganization.Customer.Person.ApplicationUser.Email));
        }
    }
}