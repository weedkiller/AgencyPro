// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stories.Emails;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.Stories
{
    public partial class StoryProjections
    {
        private void EmailProjections()
        {
            CreateMap<Story, AccountManagerStoryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.AccountManager.Person.DisplayName));

            CreateMap<Story, ContractorStoryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName));

            CreateMap<Story, ProjectManagerStoryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.DisplayName));

            CreateMap<Story, AgencyOwnerStoryEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));


        }
    }
}