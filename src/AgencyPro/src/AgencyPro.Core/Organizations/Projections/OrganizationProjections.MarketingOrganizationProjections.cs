// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.Projections
{
    public partial class OrganizationProjections
    {
        private void MarketingOrganizationProjections()
        {
            CreateMap<Organization, MarketingOrganizationOutput>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(y => y.ImageUrl))
                .ForMember(x => x.Marketers, opt => opt.MapFrom(x => x.Marketers.Count))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .IncludeAllDerived();

            CreateMap<Organization, MarketerOrganizationOutput>()
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Name))
                .ForMember(x => x.Description, x => x.MapFrom(y => y.Description))
                .ForMember(x => x.ImageUrl, x => x.MapFrom(y => y.ImageUrl))
                .ForMember(x => x.Marketers, opt => opt.MapFrom(x => x.Marketers.Count))
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
                .IncludeAllDerived();

            CreateMap<Organization, MarketingAgencyOwnerOrganizationOutput>();

            CreateMap<Organization, ProviderAgencyOwnerMarketingOrganizationOutput>()
                .IncludeAllDerived();
        }
    }
}