// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Core.StoryTemplates.ViewModels;

namespace AgencyPro.Core.StoryTemplates
{
    public class StoryTemplateProjections : Profile
    {
        public StoryTemplateProjections()
        {
            CreateMap<StoryTemplate, StoryTemplateOutput>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.StoryPoints, opt => opt.MapFrom(x => x.StoryPoints))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.Hours, opt => opt.MapFrom(x => x.Hours))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title));


        }
    }
}