// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.TimeEntries.Enums;
using System.Linq;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers
{
    public class OrganizationProjectManagerProjections : Profile
    {
        public OrganizationProjectManagerProjections()
        {



            CreateMap<OrganizationProjectManager, OrganizationProjectManagerStatistics>()
                .ForMember(x => x.TotalProjects, opt => opt.MapFrom(x => x.Projects.Count))
                .ForMember(x => x.TotalContracts,
                    opt => opt.MapFrom(x => x.Contracts.Count))
                .ForMember(x => x.TotalCandidates, opt => opt.MapFrom(x => x.Candidates.Count))
                .IncludeAllDerived();



            CreateMap<OrganizationProjectManager, ProjectManagerCounts>()
                .ForMember(x => x.Stories, opt => opt.MapFrom(x => x.Projects.SelectMany(y => y.Stories).Count()))

                .ForMember(x => x.Contracts, opt => opt.MapFrom(x => x.Contracts.Count))

                .ForMember(x => x.TimeEntries, opt => opt.MapFrom(x => x.Contracts
                    .SelectMany(z => z.TimeEntries.Where(a => a.Status == TimeStatus.Logged)).Count()))

                .ForMember(x => x.Candidates, opt => opt.MapFrom(x => x.Candidates.Count(y => y.Status == CandidateStatus.Qualified)))
                .ForMember(x => x.Proposals, opt => opt.MapFrom(x => x.Projects.Count(y => y.Proposal != null && y.Proposal.Status != ProposalStatus.Rejected)))
                .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Projects.Count));

            ProjectManagerOutputs();
            OrganizationOutputs();
        }

        private void ProjectManagerOutputs()
        {
            CreateMap<OrganizationProjectManager, OrganizationProjectManagerOutput>()

                .ForMember(x => x.DisplayName, opt => opt.MapFrom(x => x.ProjectManager.Person.DisplayName))
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.ProjectManager.Person.ImageUrl))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.Email))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.ProjectManager.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Status, opt => opt.MapFrom(x => x.OrganizationPerson.Status))
                .IncludeAllDerived();

            CreateMap<OrganizationProjectManager, AgencyOwnerOrganizationProjectManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationProjectManager, AccountManagerOrganizationProjectManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationProjectManager, ContractorOrganizationProjectManagerOutput>()
                .IncludeAllDerived();

            CreateMap<OrganizationProjectManager, CustomerOrganizationProjectManagerOutput>()
                .IncludeAllDerived();


        }

        private void OrganizationOutputs()
        {
            CreateMap<OrganizationProjectManager, ProjectManagerOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.ProjectManagerStream, o => o.MapFrom(x => x.ProjectManagerStream))
                .IncludeAllDerived();
        }
    }
}