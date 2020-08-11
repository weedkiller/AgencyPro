// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.ForgotPassword.Emails;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Registration.Emails;
using AgencyPro.Core.ResetPassword.Emails;

namespace AgencyPro.Core.People
{
    public class PersonProjections : Profile
    {
        public PersonProjections()
        {
           
            CreateMap<Person, PersonOutput>()
                .ForMember(x => x.Iso2, o => o.MapFrom(y => y.Iso2))
                .ForMember(x => x.ProvinceState, o => o.MapFrom(y => y.ProvinceState))
                .ForMember(x => x.IsCustomer, o => o.MapFrom(y => y.Customer != null))
                .ForMember(x => x.IsContractor, o => o.MapFrom(y => y.Contractor != null))
                .ForMember(x => x.IsAccountManager, o => o.MapFrom(y => y.AccountManager != null))
                .ForMember(x => x.IsProjectManager, o => o.MapFrom(y => y.ProjectManager != null))
                .ForMember(x => x.IsRecruiter, o => o.MapFrom(y => y.Recruiter != null))
                .ForMember(x => x.IsMarketer, o => o.MapFrom(y => y.Marketer != null))
                .ForMember(x => x.EmailAddress, o => o.MapFrom(y => y.ApplicationUser.Email))
                .ForMember(x => x.PhoneNumber, o => o.MapFrom(y => y.ApplicationUser.PhoneNumber)).IncludeAllDerived();

            CreateMap<Person, PersonDetailsOutput>().IncludeAllDerived();
            CreateMap<Person, AgencyOwnerPersonOutput>().IncludeAllDerived();
            CreateMap<Person, AccountManagerPersonOutput>().IncludeAllDerived();
            CreateMap<Person, ProjectManagerPersonOutput>().IncludeAllDerived();

            CreateMap<Person, AccountManagerOrganizationPersonDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<Person, AgencyOwnerOrganizationPersonDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<Person, WelcomeEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => true))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.FirstName));

            CreateMap<Person, ForgotPasswordEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => true))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.FirstName));

            CreateMap<Person, ResetPasswordEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => true))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.FirstName));
        }
    }
}