// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;

namespace AgencyPro.Core.Contracts
{
    public partial class ContractProjections
    {
        private void CreateProviderProjections()
        {
            CreateMap<Contract, ProviderContractOutput>()
                .IncludeAllDerived();

            #region "Account Managers"

            CreateMap<Contract, AccountManagerContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, AccountManagerContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Stories,
                    opt => opt.MapFrom(
                        x => x.TimeEntries.Where(z => z.StoryId.HasValue).Select(y => y.Story).Distinct()))
                .ForMember(x => x.TimeEntries, opt => opt.MapFrom(x => x.TimeEntries));

            #endregion

            #region "Agency Owners" 

            CreateMap<Contract, AgencyOwnerProviderContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, AgencyOwnerProviderContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Stories,
                    opt => opt.MapFrom(
                        x => x.TimeEntries.Where(z => z.StoryId.HasValue && z.Story.IsDeleted == false).Select(y => y.Story).Distinct()))
                .ForMember(x => x.TimeEntries, opt => opt.MapFrom(x => x.TimeEntries));

            #endregion

            #region "Project Managers" 

            CreateMap<Contract, ProjectManagerContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, ProjectManagerContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Stories,
                    opt => opt.MapFrom(
                        x => x.TimeEntries.Where(z => z.StoryId.HasValue).Select(y => y.Story).Distinct()))
                .ForMember(x => x.TimeEntries, opt => opt.Ignore());

            #endregion

            #region "Contractors" 

            CreateMap<Contract, ContractorContractOutput>()
                .IncludeAllDerived();

            CreateMap<Contract, ContractorContractDetailsOutput>()
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments.OrderBy(y => y.Created)))
                .ForMember(x => x.Stories,
                    opt => opt.MapFrom(
                        x => x.TimeEntries.Where(z => z.StoryId.HasValue).Select(y => y.Story).Distinct()))
                .ForMember(x => x.TimeEntries, opt => opt.MapFrom(x => x.TimeEntries));

            #endregion
        }

        
    }
}