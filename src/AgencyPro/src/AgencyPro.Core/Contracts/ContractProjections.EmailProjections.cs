// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Emails;
using AgencyPro.Core.Contracts.Models;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections
    {
        private void CreateEmailProjections()
        {
            CreateMap<Contract, AgencyOwnerContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<Contract, ContractorContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Contractor.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Contractor.Person.DisplayName));


            CreateMap<Contract, ProjectManagerContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName));

            CreateMap<Contract, RecruiterContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName));



            CreateMap<Contract, MarketerContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Marketer.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Marketer.Person.DisplayName));

            CreateMap<Contract, RecruitingAgencyContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.RecruiterOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.RecruiterOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.RecruiterOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<Contract, MarketingAgencyContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.MarketerOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<Contract, AccountManagerContractEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.AccountManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.AccountManager.Person.DisplayName));

        }
    }
}