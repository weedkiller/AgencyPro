// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Common;
using AgencyPro.Core.Leads.Services;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class LeadController : OrganizationUserController
    {
        private readonly ILeadService _leadService;
        private readonly ILeadMatrixService _leadMatrixService;
        private readonly IOrganizationMarketer _marketer;
        private readonly ICommentService _commentService;

        public LeadController(ILeadService leadService,
            ILeadMatrixService leadMatrixService,
            IOrganizationMarketer marketer,
            ICommentService commentService,
            IServiceProvider provider) : base(provider)
        {
            _marketer = marketer;
            _commentService = commentService;
            _leadService = leadService;
            _leadMatrixService = leadMatrixService;
        }

        /// <summary>
        ///     MA creates a internal lead
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateLead(
            [FromRoute] Guid organizationId,
            [FromBody] LeadInput model)
        {
            var result = await _leadService.CreateInternalLead(_marketer, model);
            return Ok(result);
        }

        /// <summary>
        ///     MA creates an external lead
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="externalId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{externalId}")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateExternalLead(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid externalId,
            [FromBody] LeadInput model)
        {


            var result = await _leadService.CreateExternalLead(_marketer, externalId, model);
            return Ok(result);
        }

        /// <summary>
        ///     ma get leads
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<MarketerLeadOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads([FromRoute] Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var leads = await _leadService.GetLeads<MarketerLeadOutput>(_marketer, filters);
            AddPagination(filters, leads.Total);
            return Ok(leads.Data);
        }

        /// <summary>
        ///     updates a lead
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="leadId">the lead id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{leadId}")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateLead(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid leadId,
            [FromBody] LeadInput model
        )
        {
            var lead = await _leadService.UpdateLead(_marketer, leadId, model);

            return Ok(lead);
        }

        /// <summary>
        ///     ma get lead
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpGet("{leadId}")]
        [ProducesResponseType(typeof(MarketerLeadDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLead([FromRoute] Guid organizationId,
            [FromRoute] Guid leadId
        )
        {
            var lead = await _leadService.GetLead<MarketerLeadDetailsOutput>(_marketer, leadId);
            return Ok(lead);
        }

        /// <summary>
        ///     deletes a lead
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpDelete("{leadId}")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId)
        {
            var result = await _leadService.DeleteLead(_marketer, leadId);

            return Ok(result);
        }

        /// <summary>
        ///     gets a matrix of lead information
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(MarketerLeadMatrixOutput), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] LeadMatrixFilters filters)
        {
            var result = await _leadMatrixService.GetResults<MarketerLeadMatrixOutput>(_marketer, filters);
            return Ok(result);
        }

        /// <summary>
        ///     Add comment to a lead
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="leadId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{leadId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromRoute] Guid organizationId, [FromRoute] Guid leadId,
            [FromBody] CommentInput input)
        {
            var result = await _commentService.CreateLeadComment(_marketer, leadId, input);
            return Ok(result);
        }
    }
}