// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Comments.ViewModels;

namespace AgencyPro.Core.Comments
{
    class CommentProjections : Profile
    {
        public CommentProjections()
        {
            CreateMap<Comment, CommentOutput>()
                .ForMember(x=>x.PersonName, opt=>opt.MapFrom(x=>x.Creator.Person.DisplayName))
                .ForMember(x=>x.PersonId, opt=>opt.MapFrom(x=>x.CreatedById))
                .ForMember(x=>x.PersonImageUrl, opt=>opt.MapFrom(x=>x.Creator.Person.ImageUrl))
                .ForMember(x=>x.OrganizationName, opt=>opt.MapFrom(x=>x.Creator.Organization.Name))
                .ForMember(x=>x.OrganizationImageUrl, opt=>opt.MapFrom(x=>x.Creator.Organization.ImageUrl))
                .ForMember(x=>x.OrganizationId, opt=>opt.MapFrom(x=>x.OrganizationId))
                .ForMember(x=>x.Created, opt=>opt.MapFrom(x=>x.Created))
                .ForMember(x => x.Body, opt => opt.MapFrom(x => x.Body));
        }
    }
}
