// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.BillingCategories.ViewModels;

namespace AgencyPro.Core.BillingCategories
{
    public class BillingCategoryProjections : Profile
    {
        public BillingCategoryProjections()
        {
            CreateMap<BillingCategory, BillingCategoryOutput>()
                .ForMember(x=>x.Id, opt=>opt.MapFrom(x=>x.Id))
                .ForMember(x=>x.IsEnabled, opt=>opt.MapFrom(x=>x.IsEnabled))
                .ForMember(x=>x.IsPrivate, opt=>opt.MapFrom(x=>x.IsPrivate))
                .ForMember(x=>x.IsStoryBucket, opt=>opt.MapFrom(x=>x.IsStoryBucket))
                .ForMember(x=>x.Name, opt=>opt.MapFrom(x=>x.Name));

            CreateMap<OrganizationBillingCategory, BillingCategoryOutput>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.BillingCategory.Id))
                .ForMember(x => x.IsEnabled, opt => opt.MapFrom(x => x.BillingCategory.IsEnabled))
                .ForMember(x => x.IsPrivate, opt => opt.MapFrom(x => x.BillingCategory.IsPrivate))
                .ForMember(x => x.IsStoryBucket, opt => opt.MapFrom(x => x.BillingCategory.IsStoryBucket))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.BillingCategory.Name));

            CreateMap<ProjectBillingCategory, BillingCategoryOutput>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.BillingCategory.Id))
                .ForMember(x => x.IsEnabled, opt => opt.MapFrom(x => x.BillingCategory.IsEnabled))
                .ForMember(x => x.IsPrivate, opt => opt.MapFrom(x => x.BillingCategory.IsPrivate))
                .ForMember(x => x.IsStoryBucket, opt => opt.MapFrom(x => x.BillingCategory.IsStoryBucket))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.BillingCategory.Name));
        }
    }
}
