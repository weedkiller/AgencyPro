// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections
    {
        private void CreateBuyerProjections()
        {
            CreateMap<Contract, CustomerContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, CustomerContractDetailsOutput>()
                .ForMember(x => x.Stories, opt => opt.MapFrom( x => x.TimeEntries.Where(z => z.StoryId.HasValue).Select(y => y.Story).Distinct()))
                .ForMember(x => x.ApprovedTimeEntries, opt => opt.Ignore())
                .ForMember(x => x.Comments,  opt => opt.MapFrom(x => x.Comments.Where(y => y.Internal == false).OrderBy(y => y.Created)));

            CreateEmailProjections();
        }
    }
}