// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AutoMapper;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.ProviderOrganizations
{
    public class ProviderOrganizationProjections : Profile
    {
        public ProviderOrganizationProjections()
        {
            CreateMap<ProviderOrganization, ProviderOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.DefaultAccountManagerId, opt => opt.MapFrom(x => x.DefaultAccountManagerId))
                .ForMember(x => x.DefaultProjectManagerId, opt => opt.MapFrom(x => x.DefaultProjectManagerId))
                .ForMember(x => x.DefaultContractorId, opt => opt.MapFrom(x => x.DefaultContractorId))
               
                .IncludeAllDerived();
            
            CreateMap<ProviderOrganization, ProviderOrganizationDetailsOutput>()
                .ForMember(x => x.AvailableBillingCategories, opt => opt.MapFrom(x => x.Organization.Category.AvailableBillingCategories.Select(y => y.BillingCategory)))
                .ForMember(x => x.AvailablePositions, opt => opt.MapFrom(x => x.Organization.Category.Positions.Select(y => y.Position)))
                .ForMember(x => x.AvailableSkills, opt => opt.MapFrom(x => x.Organization.Category.AvailableSkills.Select(y => y.Skill)))
                .ForMember(x => x.AvailablePaymentTerms, opt => opt.MapFrom(x => x.Organization.Category.AvailablePaymentTerms.Select(y => y.PaymentTerm)))
                .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Organization.ProviderOrganization.Skills.ToDictionary(z => z.SkillId, y => y.Priority)))
                .ForMember(x => x.PaymentTerms, opt => opt.MapFrom(x => x.Organization.PaymentTerms.ToDictionary(z => z.PaymentTermId, y => y.IsDefault)))
                .ForMember(x => x.BillingCategories, opt => opt.MapFrom(x => x.Organization.BillingCategories.Select(y => y.BillingCategory.Id)))
                .ForMember(x => x.Positions, opt => opt.MapFrom(x => x.Organization.Positions.Select(y => y.PositionId)))
               .IncludeAllDerived();

            CreateMap<ProviderOrganization, AgencyOwnerProviderOrganizationDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<ProviderOrganization, MarketerProviderOrganizationOutput>()
                .IncludeAllDerived();

            CreateMap<ProviderOrganization, MarketerProviderOrganizationOutput>()
                .IncludeAllDerived();

            CreateMap<ProviderOrganization, MarketingAgencyOwnerProviderOrganizationOutput>()
                .IncludeAllDerived();

            CreateMap<ProviderOrganization, RecruitingAgencyOwnerProviderOrganizationOutput>();

            CreateMap<ProviderOrganization, MarketingAgreementOutput>()
                .ForMember(x => x.ProviderOrganizationName, x => x.MapFrom(y => y.Organization.Name))
                .ForMember(x => x.ProviderOrganizationImageUrl, x => x.MapFrom(y => y.Organization.ImageUrl))
                .ForMember(x => x.ProviderOrganizationId, x => x.MapFrom(y => y.Id));
        }

    }
}