// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.ViewModels;

namespace AgencyPro.Core.Leads
{
    public class LeadMatrixProjectionMap : Profile
    {
        public LeadMatrixProjectionMap()
        {
            CreateMap<LeadMatrix, LeadMatrixOutput>()
                .IncludeAllDerived();

            CreateMap<LeadMatrix, AgencyOwnerLeadMatrixOutput>();
            CreateMap<LeadMatrix, MarketerLeadMatrixOutput>();
        }
    }
}