// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.ViewModels;

namespace AgencyPro.Core.Agreements
{
    public partial class RecruitingAgreementProjections : Profile
    {
        public RecruitingAgreementProjections()
        {
            CreateMap<RecruitingAgreement, RecruitingAgreementOutput>()
                .ForMember(x => x.ProviderOrganizationId, opt => opt.MapFrom(x => x.ProviderOrganizationId))
                .ForMember(x => x.ProviderOrganizationName, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.Name))
                .ForMember(x => x.ProviderOrganizationImageUrl, opt => opt.MapFrom(x => x.ProviderOrganization.Organization.ImageUrl))
                .ForMember(x => x.RecruitingOrganizationId, opt => opt.MapFrom(x => x.RecruitingOrganizationId))
                .ForMember(x => x.RecruitingOrganizationName, opt => opt.MapFrom(x => x.RecruitingOrganization.Organization.Name))
                .ForMember(x => x.RecruiterOrganizationImageUrl, opt => opt.MapFrom(x => x.RecruitingOrganization.Organization.ImageUrl))
                .ForMember(x => x.RecruiterStream, opt => opt.MapFrom(x => x.RecruiterStream))
                .ForMember(x => x.RecruitingAgencyStream, opt => opt.MapFrom(x => x.RecruitingAgencyStream))
                .ForMember(x => x.RecruitingAgencyBonus, opt => opt.MapFrom(x => x.RecruitingAgencyBonus))
                .ForMember(x => x.RecruiterBonus, opt => opt.MapFrom(x => x.RecruiterBonus))

                .IncludeAllDerived();

            CreateMap<RecruitingAgreement, AgencyOwnerRecruitingAgreementOutput>();
            CreateMap<RecruitingAgreement, RecruiterRecruitingAgreementOutput>();

            CreateEmails();
        }
    }
}