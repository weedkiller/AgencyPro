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

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class ProposalController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly IProposalService _proposalService;
        private readonly IProposalPDFService _proposalPdfService;

        public ProposalController(
            IProposalService proposalService, 
            IOrganizationCustomer customer,
            IProposalPDFService proposalPdfService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customer = customer;
            _proposalService = proposalService;
            _proposalPdfService = proposalPdfService;
        }

        /// <summary>
        ///     get all proposals
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerFixedPriceProposalOutput>), 200)]
        public async Task<IActionResult> GetProposals([FromRoute] Guid organizationId,
            [FromQuery] ProposalFilters filters)
        {
            var p = await _proposalService
                .GetFixedPriceProposals<CustomerFixedPriceProposalOutput>(_customer, filters);
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
        [ProducesResponseType(typeof(CustomerFixedPriceProposalDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            var p = await _proposalService
                .GetProposal<CustomerFixedPriceProposalDetailsOutput>(_customer, proposalId);

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
            return await _proposalPdfService.GenerateProposal(_customer, proposalId);
        }

        /// <summary>
        ///     accept proposal by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="proposalId"></param>
        /// <returns></returns>
        [HttpPatch("{proposalId}/accept")]
        [ProducesResponseType(typeof(ProposalAcceptanceDetailOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> AcceptProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId)
        {
            var p = await _proposalService
                .AcceptFixedPriceProposal(_customer, proposalId);

            return Ok(p);
        }

        /// <summary>
        ///     reject proposal by id
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="proposalId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{proposalId}/reject")]
        [ProducesResponseType(typeof(CustomerFixedPriceProposalOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectProposal([FromRoute] Guid organizationId, [FromRoute] Guid proposalId,
            [FromBody] ProposalRejectionInput model)
        {
            var p = await _proposalService
                .Reject(_customer, proposalId, model);

            return Ok(p);
        }
    }
}