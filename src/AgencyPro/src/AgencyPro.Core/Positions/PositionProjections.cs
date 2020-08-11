// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Positions.Models;
using AgencyPro.Core.Positions.ViewModels;

namespace AgencyPro.Core.Positions
{
    public class PositionProjections : Profile
    {
        public PositionProjections()
        {
            CreateMap<Position, PositionOutput>()
                .ForMember(x => x.Levels, opt => opt.MapFrom(x => x.Levels))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<OrganizationPosition, OrganizationPositionOutput>()
                .IncludeMembers(x => x.Position)
                .ForMember(x => x.OrganizationId, opt => opt.MapFrom(x => x.OrganizationId));

            CreateMap<Position, OrganizationPositionOutput>();
        }
    }
}
