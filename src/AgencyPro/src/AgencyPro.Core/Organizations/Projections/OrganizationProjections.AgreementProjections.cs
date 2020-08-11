// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.ViewModels;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.Organizations.Projections
{
    public partial class OrganizationProjections
    {
        private void AgreementProjections()
        {
            CreateMap<Organization, RecruitingAgreementOutput>()
                .ForMember(x => x.RecruitingOrganizationName, y => y.MapFrom(x => x.Name))
                .ForMember(x => x.RecruiterOrganizationImageUrl, y => y.MapFrom(x => x.ImageUrl))
                .IncludeAllDerived();
        }
    }
}