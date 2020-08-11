// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Candidates.Emails;
using AgencyPro.Core.Candidates.Models;

namespace AgencyPro.Core.Candidates
{
    public partial class CandidateProjections
    {
        private void CreateEmailsMaps()
        {
            CreateMap<Candidate, AgencyOwnerCandidateEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Customer.Person.ApplicationUser.Email));

            CreateMap<Candidate, RecruiterCandidateEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.Recruiter.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.Recruiter.Person.DisplayName));

            CreateMap<Candidate, ProjectManagerCandidateEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName));

            CreateMap<Candidate, RecruitingAgencyOwnerCandidateEmail>()
                .ForMember(x => x.SendMail, opt => opt.MapFrom(x => x.RecruiterOrganization.Customer.Person.ApplicationUser.SendMail))
                .ForMember(x => x.RecipientName, opt => opt.MapFrom(x => x.RecruiterOrganization.Customer.Person.DisplayName))
                .ForMember(x => x.RecipientEmail, opt => opt.MapFrom(x => x.RecruiterOrganization.Customer.Person.ApplicationUser.Email));

        }
    }
}