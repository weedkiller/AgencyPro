// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.Emails;
using AgencyPro.Core.OrganizationPeople.Models;

namespace AgencyPro.Core.OrganizationPeople
{
    public partial class OrganizationPersonProjections
    {
        private void EmailProjections()
        {
            CreateMap<OrganizationPerson, AgencyOwnerOrganizationPersonEmail>()
                .ForMember(x=>x.RecipientEmail, opt=>opt.MapFrom(x=>x.Organization.Customer.Person.ApplicationUser.Email))
                .ForMember(x=>x.SendMail, opt=>opt.MapFrom(x=>x.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x=>x.RecipientName, opt=>opt.MapFrom(x=>x.Organization.Customer.Person.DisplayName));

            CreateMap<OrganizationPerson, OrganizationPersonEmail>()
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Person.ApplicationUser.Email))
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Person.DisplayName));
        }
    }
}