// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Stories.ViewModels;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public partial class OrganizationContractorProjections
    {
        private void StoryOutputMappings()
        {
            CreateMap<OrganizationContractor, StoryOutput>()
                .ForMember(x => x.ContractorId, x => x.MapFrom(y => y.ContractorId))
                .ForMember(x => x.ContractorName, x => x.MapFrom(y => y.Contractor.Person.DisplayName))
                .ForMember(x => x.ContractorEmail, x => x.MapFrom(y => y.Contractor.Person.ApplicationUser.Email))
                .ForMember(x => x.ContractorPhoneNumber, x => x.MapFrom(y => y.Contractor.Person.ApplicationUser.PhoneNumber))
                .ForMember(x => x.ContractorImageUrl, x => x.MapFrom(y => y.Contractor.Person.ImageUrl));
        }
    }
}