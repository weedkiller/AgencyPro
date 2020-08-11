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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class LeadController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly Lazy<IMarketingAgencyOwner> _marketingAgencyOwner;
        private readonly ILeadService _leadService;
        private readonly Lazy<IProviderAgencyOwner> _agencyOwner;

        public LeadController(
            ICommentService commentService,
            Lazy<IMarketingAgencyOwner> marketingAgencyOwner,
            ILeadService leadService, 
            Lazy<IProviderAgencyOwner> agencyOwner,
            IServiceProvider provider) : base(provider)
        {
            _commentService = commentService;
            _marketingAgencyOwner = marketingAgencyOwner;
            _leadService = leadService;
            _agencyOwner = agencyOwner;
        }

        /// <summary>
        ///     rejects a lead
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="leadId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{leadId}/reject")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId,
            [FromBody] LeadRejectInput input)
        {
            var ao = _agencyOwner.Value;
            var response = await _leadService.RejectLead(ao, leadId, input);

            return Ok(response);
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
            var ao = _agencyOwner.Value;
            var result  =await _leadService.DeleteLead(ao, leadId);

            return Ok(result);
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
        
        public async Task<IActionResult> UpdateLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId,
            [FromBody] LeadInput model)
        {
            var ao = _agencyOwner.Value;
            var lead = await _leadService.UpdateLead(ao, leadId, model);

            return Ok(lead);
        }

        /// <summary>
        ///     qualifies a lead
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="leadId">the lead id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{leadId}/qualify")]
        [ProducesResponseType(typeof(LeadResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> QualifyLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId,
            [FromBody] LeadQualifyInput input)
        {

            var ao = _agencyOwner.Value;
            var r = await _leadService.QualifyLead(ao, leadId, input);

            return Ok(r);
        }

        /// <summary>
        ///     Provider agency leads
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize]
        [ProducesResponseType(typeof(List<AgencyOwnerLeadOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads([FromRoute] Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var ao = _agencyOwner.Value;
            var leads = await _leadService.GetLeads<AgencyOwnerLeadOutput>(ao, filters);
            AddPagination(filters, leads.Total);
            return Ok(leads.Data);
        }

        /// <summary>
        /// Marketing agency Leads
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("marketing")]
        [Authorize]
        [ProducesResponseType(typeof(List<AgencyOwnerLeadOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingAgencyLeads([FromRoute] Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var ao = _marketingAgencyOwner.Value;
            var leads = await _leadService.GetLeads<AgencyOwnerLeadOutput>(ao, filters);
            AddPagination(filters, leads.Total);
            return Ok(leads.Data);
        }

        /// <summary>
        ///     get lead details for provider agency owner
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpGet("{leadId}")]
        [ProducesResponseType(typeof(AgencyOwnerLeadDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId)
        {
            var ao = _agencyOwner.Value;

            var lead = await _leadService.GetLead<AgencyOwnerLeadDetailsOutput>(ao, leadId);
            return Ok(lead);
        }

        /// <summary>
        /// Get lead details for marketing agency owner
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpGet("marketing/{leadId}")]
        [ProducesResponseType(typeof(AgencyOwnerLeadDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId)
        {
            var ao = _marketingAgencyOwner.Value;

            var lead = await _leadService.GetLead<AgencyOwnerLeadDetailsOutput>(ao, leadId);
            return Ok(lead);
        }

        /// <summary>
        /// Add comment to a lead
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
            var ao = _agencyOwner.Value;

            var result = await _commentService.CreateLeadComment(ao, leadId, input);
            return Ok(result);
        }
    }
}