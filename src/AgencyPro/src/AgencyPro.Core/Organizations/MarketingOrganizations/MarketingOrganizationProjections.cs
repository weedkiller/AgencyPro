// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.MarketingOrganizations
{
    public class MarketingOrganizationProjections : Profile
    {
        public MarketingOrganizationProjections()
        {
            CreateMap<MarketingOrganization, MarketingOrganizationOutput>()
                .IncludeMembers(x => x.Organization)
                .ForMember(x => x.DefaultMarketerId, opt => opt.MapFrom(x => x.DefaultMarketerId))
                .ForMember(x => x.ServiceFeePerLead, opt => opt.MapFrom(x => x.ServiceFeePerLead))
                .IncludeAllDerived();


            CreateMap<MarketingOrganization, AgencyOwnerMarketingOrganizationDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<MarketingOrganization, ProviderAgencyOwnerMarketingOrganizationOutput>()
                .IncludeAllDerived();

            CreateMap<MarketingOrganization, MarketingAgreementOutput>()
                .ForMember(x => x.MarketingOrganizationName, x => x.MapFrom(y => y.Organization.Name))
                .ForMember(x => x.MarketingOrganizationImageUrl, x => x.MapFrom(y => y.Organization.ImageUrl))
                .ForMember(x => x.MarketingOrganizationId, x => x.MapFrom(y => y.Id))
                .IncludeAllDerived();

        }
    }
}