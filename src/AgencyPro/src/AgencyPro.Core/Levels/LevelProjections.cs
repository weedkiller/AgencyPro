// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Levels.ViewModels;

namespace AgencyPro.Core.Levels
{
    public class LevelProjections : Profile
    {
        public LevelProjections()
        {
            CreateMap<Level, LevelOutput>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id));
        }
    }
}
