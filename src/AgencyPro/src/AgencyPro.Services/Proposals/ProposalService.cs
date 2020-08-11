// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Common;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Proposals.Extensions;
using AgencyPro.Core.Proposals.Filters;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Services.Contracts.EventHandlers;
using AgencyPro.Services.Proposals.Messaging;
using AgencyPro.Services.Stories.EmailNotifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Proposals
{
    public partial class ProposalService : Service<FixedPriceProposal>, IProposalService
    {
        private readonly ILogger<ProposalService> _logger;
        private readonly IUserInfo _userInfo;
        private readonly IRepositoryAsync<Project> _projectRepository;
        private readonly IRepositoryAsync<Organization> _organizationRepository;
        private readonly IRepositoryAsync<ProposalAcceptance> _proposalAcceptance;


        public ProposalService(
            IServiceProvider serviceProvider, 
            ILogger<ProposalService> logger,
            MultiProposalEventHandler handler,
            MultiContractEventHandler contractEvents,
            StoryEventHandlers storyEvents,
            IUserInfo userInfo) : base(serviceProvider)
        {
            _organizationRepository = UnitOfWork
                .RepositoryAsync<Organization>();

            _projectRepository = UnitOfWork
                .RepositoryAsync<Project>();
            
            _proposalAcceptance = UnitOfWork
                .RepositoryAsync<ProposalAcceptance>();

            _logger = logger;
            _userInfo = userInfo;
            AddEventHandler(contractEvents, handler, storyEvents);
        }

        public Task<FixedPriceProposalOutput> GetProposal(Guid id)
        {
            return Repository.Queryable()
                .FindById(id)
                .ProjectTo<FixedPriceProposalOutput>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProposal<T>(Guid id) where T : FixedPriceProposalOutput
        {
            return Repository.Queryable().FindById(id).ProjectTo<T>(ProjectionMapping).FirstOrDefaultAsync();
        }

        public Task<PackedList<T>> GetFixedPriceProposals<T>(IOrganizationCustomer cu, ProposalFilters filters
        ) where T : CustomerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .ApplyWhereFilters(filters)
                .PaginateProjection<FixedPriceProposal, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetFixedPriceProposals<T>(IProviderAgencyOwner ao, ProposalFilters filters) where T : AgencyOwnerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .ApplyWhereFilters(filters)
                .PaginateProjection<FixedPriceProposal, T>(filters, ProjectionMapping);
        }

        public Task<PackedList<T>> GetFixedPriceProposals<T>(IOrganizationAccountManager am, ProposalFilters filters
        ) where T : AccountManagerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .ApplyWhereFilters(filters)
                .PaginateProjection<FixedPriceProposal, T>(filters, ProjectionMapping);
        }

        public Task<T> GetProposal<T>(IProviderAgencyOwner ao, Guid proposalId) where T : AgencyOwnerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForAgencyOwner(ao)
                .FindById(proposalId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        public Task<T> GetProposal<T>(IOrganizationCustomer cu, Guid proposalId) where T : CustomerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForOrganizationCustomer(cu)
                .FindById(proposalId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
        

        public Task<T> GetProposal<T>(IOrganizationAccountManager am, Guid proposalId) where T : AccountManagerFixedPriceProposalOutput
        {
            return Repository.Queryable()
                .ForOrganizationAccountManager(am)
                .FindById(proposalId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ProposalService)}.{callerName}] - {message}";
        }
    }
}