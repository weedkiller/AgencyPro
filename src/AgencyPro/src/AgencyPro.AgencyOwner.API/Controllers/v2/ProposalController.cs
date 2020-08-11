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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ProposalController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IProposalService _proposalService;
        private readonly IProviderAgencyOwner _providerAgencyOwner;
        private readonly IProposalPDFService _proposalPdfService;

        public ProposalController(
            IAgencyOwner agencyOwner,
            IProposalService proposalService,
            IProviderAgencyOwner providerAgencyOwner,
            IProposalPDFService proposalPdfService,
            IServiceProvider provider) : base(provider)
        {
            _agencyOwner = agencyOwner;
            _proposalService = proposalService;
            _providerAgencyOwner = providerAgencyOwner;
            _proposalPdfService = proposalPdfService;
        }

        /// <summary>
        ///     get all proposals
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerFixedPriceProposalOutput>), 200)]
        public async Task<IActionResult> GetProposals([FromRoute] Guid organizationId,
            [FromQuery] ProposalFilters filters)
        {
            var p = await _proposalService
                .GetFixedPriceProposals<AgencyOwnerFixedPriceProposalOutput>((IProviderAgencyOwner)_agencyOwner, filters);
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
        [ProducesResponseType(typeof(AgencyOwnerFixedPriceProposalDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            var p = await _proposalService.GetProposal<AgencyOwnerFixedPriceProposalDetailsOutput>
                    ((IProviderAgencyOwner) _agencyOwner, proposalId);

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
            return await _proposalPdfService.GenerateProposal((IProviderAgencyOwner) _agencyOwner, proposalId);
        }

        /// <summary>
        ///     submits a proposal
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{projectId}")]
        [ProducesResponseType(typeof(ProposalResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromRoute] Guid organizationId, [FromRoute] Guid projectId,
            [FromBody] ProposalOptions model
        ){
            var p = await _proposalService
                .Create(_agencyOwner, projectId, model);

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
                .DeleteProposal(_agencyOwner, projectId);

            return Ok(result);
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
            var p = await _proposalService.SendProposal((IProviderAgencyOwner) _agencyOwner, projectId);
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
            var p = await _proposalService.RevokeProposal((IProviderAgencyOwner) _agencyOwner, projectId);
            return Ok(p);
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
                await _proposalService.Update((IProviderAgencyOwner) _agencyOwner,
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
                .AcceptFixedPriceProposal(_providerAgencyOwner, proposalId);

            return Ok(p);
        }
    }
}