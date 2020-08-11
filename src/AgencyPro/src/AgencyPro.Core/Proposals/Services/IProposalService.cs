// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Filters;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.Core.Proposals.Services
{
    public interface IProposalService : IService<FixedPriceProposal>
    {
        Task<FixedPriceProposalOutput> GetProposal(Guid id);
        Task<T> GetProposal<T>(Guid id) where T : FixedPriceProposalOutput;

        Task<PackedList<T>> GetFixedPriceProposals<T>(IOrganizationAccountManager am, ProposalFilters filters) where T : AccountManagerFixedPriceProposalOutput;
        Task<T> GetProposal<T>(IOrganizationAccountManager am, Guid proposalId) where T : AccountManagerFixedPriceProposalOutput;

        Task<ProposalResult> AcceptFixedPriceProposal(Guid proposalId);
        Task<ProposalResult> AcceptFixedPriceProposal(IOrganizationCustomer cu, Guid proposalId);
        Task<ProposalResult> AcceptFixedPriceProposal(IProviderAgencyOwner ao, Guid proposalId);
        Task<ProposalResult> AcceptFixedPriceProposal(IOrganizationAccountManager ao, Guid proposalId);

        Task<ProposalResult> Reject(IOrganizationCustomer organizationCustomer, Guid proposalId,
            ProposalRejectionInput input);

        Task<ProposalResult> Reject(Guid proposalId, ProposalRejectionInput input);

        Task<PackedList<T>> GetFixedPriceProposals<T>(IOrganizationCustomer cu, ProposalFilters filters) where T : CustomerFixedPriceProposalOutput;
        Task<T> GetProposal<T>(IOrganizationCustomer cu, Guid proposalId) where T : CustomerFixedPriceProposalOutput;

        Task<T> GetProposal<T>(IProviderAgencyOwner ao, Guid proposalId) where T : AgencyOwnerFixedPriceProposalOutput;

        Task<PackedList<T>> GetFixedPriceProposals<T>(IProviderAgencyOwner ao, ProposalFilters filters) where T : AgencyOwnerFixedPriceProposalOutput;

        Task<ProposalResult> Create(IAgencyOwner agencyOwner, Guid projectId, ProposalOptions input);
        
        Task<ProposalResult> Create(IOrganizationAccountManager am, Guid projectId, ProposalOptions input);

        Task<bool> DeleteProposal(IAgencyOwner agencyOwner, Guid proposalId);
        Task<bool> DeleteProposal(IOrganizationAccountManager am, Guid proposalId);

        Task<ProposalResult> SendProposal(IProviderAgencyOwner agencyOwner, Guid proposalId);
        Task<ProposalResult> RevokeProposal(IProviderAgencyOwner agencyOwner, Guid proposalId);
        Task<ProposalResult> SendProposal(IOrganizationAccountManager am, Guid proposalId);
        Task<ProposalResult> RevokeProposal(IOrganizationAccountManager am, Guid proposalId);

        Task<ProposalResult> Update(IProviderAgencyOwner agencyOwner, Guid proposalId, ProposalOptions input);

        Task<ProposalResult> Update(IOrganizationAccountManager am, Guid proposalId, ProposalOptions input);
    }
}