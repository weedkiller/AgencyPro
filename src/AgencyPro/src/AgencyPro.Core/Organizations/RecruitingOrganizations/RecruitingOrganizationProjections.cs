// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations
{
    public class RecruitingOrganizationProjections : Profile
    {
        public RecruitingOrganizationProjections()
        {
            CreateMap<RecruitingOrganization, RecruitingOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.DefaultRecruiterId, o => o.MapFrom(x => x.DefaultRecruiterId))
                .IncludeAllDerived();

            CreateMap<RecruitingOrganization, RecruitingOrganizationDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<RecruitingOrganization, AgencyOwnerRecruitingOrganizationDetailsOutput>()
                .IncludeMembers(x => x.Organization)
                .IncludeAllDerived();

            CreateMap<RecruitingOrganization, ProviderAgencyOwnerRecruitingOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .IncludeAllDerived();
            
        }

    }
}