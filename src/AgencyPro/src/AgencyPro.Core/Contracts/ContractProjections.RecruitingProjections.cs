// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections
    {
        private void CreateRecruitingProjections()
        {
            CreateMap<Contract, RecruitingContractOutput>()
                .ForMember(x => x.MarketerStream, o => o.Ignore())
                .ForMember(x => x.MarketingAgencyStream, o => o.Ignore())
                .ForMember(x => x.MaxMarketerWeekly, o => o.Ignore())
                .ForMember(x => x.MaxMarketingAgencyWeekly, o => o.Ignore())
                .ForMember(x => x.ProjectManagerId, o => o.Ignore())
                .ForMember(x => x.ProjectManagerName, o => o.Ignore())
                .ForMember(x => x.ProjectManagerImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerStream, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationId, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationName, o => o.Ignore())
                .ForMember(x => x.AccountManagerId, o => o.Ignore())
                .ForMember(x => x.AccountManagerName, o => o.Ignore())
                .ForMember(x => x.AccountManagerImageUrl, o => o.Ignore())
                .ForMember(x => x.AccountManagerStream, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationImageUrl, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationId, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationName, o => o.Ignore())
                .IncludeAllDerived();

            CreateMap<Contract, RecruiterContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, RecruiterContractDetailsOutput>();

            CreateMap<Contract, AgencyOwnerRecruitingContractOutput>().IncludeAllDerived();

            CreateMap<Contract, AgencyOwnerRecruitingContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));
        }
    }
}