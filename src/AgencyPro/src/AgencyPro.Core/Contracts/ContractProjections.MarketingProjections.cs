// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections
    {
        private void CreateMarketingProjections()
        {
            CreateMap<Contract, MarketingContractOutput>()
                .ForMember(x => x.AccountManagerId, o => o.Ignore())
                .ForMember(x => x.AccountManagerName, o => o.Ignore())
                .ForMember(x => x.AccountManagerImageUrl, o => o.Ignore())
                .ForMember(x => x.AccountManagerStream, o => o.Ignore())
                .ForMember(x => x.MaxAccountManagerWeekly, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationImageUrl, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationId, o => o.Ignore())
                .ForMember(x => x.AccountManagerOrganizationName, o => o.Ignore())
                .ForMember(x => x.ProjectManagerId, o => o.Ignore())
                .ForMember(x => x.ProjectManagerName, o => o.Ignore())
                .ForMember(x => x.ProjectManagerImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerStream, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationId, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationName, o => o.Ignore())
                .ForMember(x => x.RecruiterStream, o => o.Ignore())
                .ForMember(x => x.RecruitingAgencyStream, o => o.Ignore())
                .ForMember(x => x.MaxRecruiterWeekly, o => o.Ignore())
                .ForMember(x => x.MaxRecruitingAgencyWeekly, o => o.Ignore())
                .ForMember(x => x.ProjectId, op => op.Ignore())
                .ForMember(x => x.ProjectName, op => op.Ignore())
                .ForMember(x => x.ProjectAbbreviation, op => op.Ignore())
                .ForMember(x => x.ProjectStatus, op => op.Ignore())
                .ForMember(x => x.ContractorId, o => o.Ignore())
                .ForMember(x => x.ContractorName, o => o.Ignore())
                .ForMember(x => x.ContractorImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerStream, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationImageUrl, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationId, o => o.Ignore())
                .ForMember(x => x.ProjectManagerOrganizationName, o => o.Ignore())
                .ForMember(x => x.MaxContractorWeekly, o => o.Ignore())
                .IncludeAllDerived();


            CreateMap<Contract, MarketerContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, MarketerContractDetailsOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, AgencyOwnerMarketingContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, AgencyOwnerMarketingContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)));
        }
    }
}