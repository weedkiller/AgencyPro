// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Proposals.Emails;
using AgencyPro.Core.Proposals.Models;

namespace AgencyPro.Core.Proposals.Projections
{
    public partial class ProposalProjections
    {
        private void CreateEmailProjections()
        {
            CreateMap<FixedPriceProposal, AccountManagerProposalEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.AccountManager.Person.DisplayName));

            CreateMap<FixedPriceProposal, AgencyOwnerProposalEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<FixedPriceProposal, CustomerProposalEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.Customer.Person.ApplicationUser.Email));



            CreateMap<FixedPriceProposal, ProjectManagerProposalEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Project.ProjectManager.Person.DisplayName));

        }
    }
}