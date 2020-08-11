// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Proposals.Filters;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Proposals.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class ProposalController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IProposalService _proposalService;
        private readonly IProposalPDFService _proposalPdfService;

        public ProposalController(IProposalService proposalService,
            IOrganizationAccountManager accountManager,
            IProposalPDFService proposalPdfService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _proposalService = proposalService;
            _accountManager = accountManager;
            _proposalPdfService = proposalPdfService;
        }

        /// <summary>
        ///     get all proposals
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerFixedPriceProposalOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProposals([FromRoute] Guid organizationId,
            [FromQuery] ProposalFilters filters)
        {
            var p = await _proposalService.GetFixedPriceProposals<AccountManagerFixedPriceProposalOutput>(_accountManager, filters);
            AddPagination(filters, p.Total);
            return Ok(p.Data);
        }

        /// <summary>
        ///     gets proposal by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpGet("{proposalId}")]
        [ProducesResponseType(typeof(AccountManagerFixedPriceProposalDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            var p = await _proposalService.GetProposal<AccountManagerFixedPriceProposalDetailsOutput>(_accountManager,
                proposalId);
            return Ok(p);
        }

        /// <summary>
        ///     download proposal pdf
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpGet("{proposalId}/pdf")]
        [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProposalPdf([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            return await _proposalPdfService.GenerateProposal(_accountManager, proposalId);
        }

        /// <summary>
        ///     generates a proposal
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectId}")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateProposal([FromRoute] Guid organizationId, [FromRoute] Guid projectId, [FromBody] ProposalOptions input)
        {
            var p = await _proposalService
                .Create(_accountManager, projectId, input);
            return Ok(p);
        }

        /// <summary>
        /// Sends the proposal to the customer
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/send")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendProposal([FromRoute] Guid organizationId, [FromRoute] Guid projectId)
        {
            var p = await _proposalService
                .SendProposal(_accountManager, projectId);

            return Ok(p);
        }
        
        /// <summary>
        /// Revokes the proposal so it can be edited.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/revoke")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> RevokeProposal([FromRoute] Guid organizationId, [FromRoute] Guid projectId)
        {
            var p = await _proposalService
                .RevokeProposal(_accountManager, projectId);

            return Ok(p);
        }


        /// <summary>
        ///     delete a proposal
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProposal([FromRoute] Guid organizationId, [FromRoute] Guid projectId)
        {
            var result = await _proposalService
                .DeleteProposal(_accountManager, projectId);

            return Ok(result);
        }

        /// <summary>
        /// updates a proposal
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{projectId}")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProposal([FromRoute] Guid organizationId, [FromRoute] Guid projectId,
            [FromBody] ProposalOptions input)
        {
            var proposal =
                await _proposalService
                    .Update(_accountManager,
                        projectId, input);
            return Ok(proposal);
        }


        /// <summary>
        ///     accept proposal by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPatch("{proposalId}/accept")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> AcceptProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            var p = await _proposalService
                .AcceptFixedPriceProposal(_accountManager, proposalId);

            return Ok(p);
        }
    }
}