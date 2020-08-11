// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Recruiters;

namespace AgencyPro.Core.Roles.Projections
{
    public class RecruiterProjections : Profile
    {
        public RecruiterProjections()
        {
            CreateMap<Recruiter, RecruiterOutput>()
                .IncludeAllDerived();

            CreateMap<Recruiter, RecruiterDetailsOutput>();
        }
    }
}