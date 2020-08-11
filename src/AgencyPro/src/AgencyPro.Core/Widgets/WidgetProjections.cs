// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Widgets.Models;
using AgencyPro.Core.Widgets.ViewModels;

namespace AgencyPro.Core.Widgets
{
    public class WidgetProjections : Profile
    {
        public WidgetProjections()
        {
            CreateMap<Widget, WidgetOutput>();

            CreateMap<CategoryWidget, CategoryWidgetOutput>()
                .ForMember(x => x.Widget, opt => opt.MapFrom(x => x.Widget));

            CreateMap<OrganizationPersonWidget, OrganizationPersonWidgetOutput>()
                .ForMember(x => x.CategoryWidget, opt => opt.MapFrom(x => x.CategoryWidget));
        }
    }
}