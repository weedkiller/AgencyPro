// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Core.Proposals.Services
{
    public interface IProposalPDFService : IService<FixedPriceProposal>
    {
        Task<FileStreamResult> GenerateProposal(IProviderAgencyOwner ao, Guid proposalId);
        Task<FileStreamResult> GenerateProposal(IOrganizationAccountManager am, Guid proposalId);
        Task<FileStreamResult> GenerateProposal(IOrganizationCustomer cu, Guid proposalId);
        Task<FileStreamResult> GenerateProposal(Guid proposalId);
    }
}