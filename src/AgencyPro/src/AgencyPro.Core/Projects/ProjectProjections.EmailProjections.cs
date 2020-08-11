// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Projects.Emails;
using AgencyPro.Core.Projects.Models;

namespace AgencyPro.Core.Projects
{
    public partial class ProjectProjections
    {
        private void CreateEmailProjections()
        {
            CreateMap<Project, ProjectManagerProjectEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName));

            CreateMap<Project, AccountManagerProjectEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName));


            CreateMap<Project, ProviderAgencyOwnerProjectEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));
            

        }
    }
}