// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;

namespace AgencyPro.Core.TimeEntries
{
    public class TimeMatrixProjectionMap : Profile
    {
        public TimeMatrixProjectionMap()
        {
            CreateMap<TimeMatrix, TimeMatrixOutput>()
                
                .IncludeAllDerived();

            CreateMap<TimeMatrix, ProviderAgencyOwnerTimeMatrixOutput>();
            CreateMap<TimeMatrix, MarketingAgencyOwnerTimeMatrixOutput>();
            CreateMap<TimeMatrix, RecruitingAgencyOwnerTimeMatrixOutput>();
            CreateMap<TimeMatrix, ProjectManagerTimeMatrixOutput>();
            CreateMap<TimeMatrix, AccountManagerTimeMatrixOutput>();
            CreateMap<TimeMatrix, ContractorTimeMatrixOutput>();
            CreateMap<TimeMatrix, CustomerTimeMatrixOutput>();
            CreateMap<TimeMatrix, RecruiterTimeMatrixOutput>();
            CreateMap<TimeMatrix, MarketerTimeMatrixOutput>();
            CreateMap<TimeMatrix, ProviderTimeMatrixOutput>();

            //CreateMap<TimeMatrix, ProjectManagerTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //    .ForMember(x => x.OrganizationAccountManager, opt => opt.MapFrom(x => x.OrganizationAccountManager))
            //    .ForMember(x => x.OrganizationCustomer, opt => opt.MapFrom(x => x.OrganizationCustomer))
            //    .ForMember(x => x.OrganizationProjectManager, opt => opt.MapFrom(x => x.OrganizationProjectManager))
            //    .ForMember(x => x.Project, opt => opt.MapFrom(x => x.Project))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));

            //CreateMap<TimeMatrix, AccountManagerTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //    .ForMember(x => x.OrganizationAccountManager, opt => opt.MapFrom(x => x.OrganizationAccountManager))
            //    .ForMember(x => x.OrganizationCustomer, opt => opt.MapFrom(x => x.OrganizationCustomer))
            //    .ForMember(x => x.OrganizationMarketer, opt => opt.MapFrom(x => x.OrganizationMarketer))
            //    .ForMember(x => x.OrganizationRecruiter, opt => opt.MapFrom(x => x.OrganizationRecruiter))
            //    .ForMember(x => x.OrganizationProjectManager, opt => opt.MapFrom(x => x.OrganizationProjectManager))
            //    .ForMember(x => x.Project, opt => opt.MapFrom(x => x.Project))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));

            //CreateMap<TimeMatrix, ContractorTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //    .ForMember(x => x.OrganizationAccountManager, opt => opt.MapFrom(x => x.OrganizationAccountManager))
            //    .ForMember(x => x.OrganizationCustomer, opt => opt.MapFrom(x => x.OrganizationCustomer))
            //    .ForMember(x => x.OrganizationProjectManager, opt => opt.MapFrom(x => x.OrganizationProjectManager))
            //    .ForMember(x => x.Project, opt => opt.MapFrom(x => x.Project))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));

            //CreateMap<TimeMatrix, RecruiterTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //     .ForMember(x => x.OrganizationRecruiter, opt => opt.MapFrom(x => x.OrganizationRecruiter))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));

            //CreateMap<TimeMatrix, MarketerTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //    .ForMember(x => x.OrganizationCustomer, opt => opt.MapFrom(x => x.OrganizationCustomer))
            //    .ForMember(x => x.OrganizationMarketer, opt => opt.MapFrom(x => x.OrganizationMarketer))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));

            //CreateMap<TimeMatrix, CustomerTimeMatrixOutput>()
            //    .ForMember(x => x.OrganizationContractor, opt => opt.MapFrom(x => x.OrganizationContractor))
            //    .ForMember(x => x.OrganizationAccountManager, opt => opt.MapFrom(x => x.OrganizationAccountManager))
            //    .ForMember(x => x.OrganizationCustomer, opt => opt.MapFrom(x => x.OrganizationCustomer))
            //    .ForMember(x => x.OrganizationProjectManager, opt => opt.MapFrom(x => x.OrganizationProjectManager))
            //    .ForMember(x => x.Project, opt => opt.MapFrom(x => x.Project))
            //    .ForMember(x => x.Contract, opt => opt.MapFrom(x => x.Contract));
        }
    }
}