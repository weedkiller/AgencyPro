// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.TimeEntries.Emails;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.TimeEntries
{
    public partial class TimeEntryProjections
    {
        private void CreateEmailMaps()
        {
            CreateMap<TimeEntry, AccountManagerTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName));


            CreateMap<TimeEntry, AgencyOwnerTimeEntryEmail>()

                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contract.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contract.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contract.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));


            CreateMap<TimeEntry, ContractorTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName));


            CreateMap<TimeEntry, MarketerTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Marketer.Person.DisplayName));


            CreateMap<TimeEntry, MarketingAgencyOwnerTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contract.MarketerOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contract.MarketerOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contract.MarketerOrganization.Organization.Customer.Person.ApplicationUser.Email));


            CreateMap<TimeEntry, ProjectManagerTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName));

            
            CreateMap<TimeEntry, RecruiterTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName));


            CreateMap<TimeEntry, RecruitingAgencyOwnerTimeEntryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contract.RecruiterOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contract.RecruiterOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contract.RecruiterOrganization.Organization.Customer.Person.ApplicationUser.Email));


        }
    }
}