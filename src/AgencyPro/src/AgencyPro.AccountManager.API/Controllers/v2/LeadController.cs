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

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class LeadController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationAccountManager _accountManager;
        private readonly ILeadService _leadService;

        public LeadController(
            ICommentService commentService,
            ILeadService leadService, IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _accountManager = accountManager;
            _leadService = leadService;
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
            var result = await _leadService.DeleteLead(_accountManager, leadId);

            return Ok(result);

        }

        /// <summary>
        ///     converts a lead into a customer, organization, and creates an account link
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="leadId">the lead id</param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPatch("{leadId}/promote")]
        [ProducesResponseType(typeof(PromoteLeadResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> PromoteLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId,
            [FromBody] PromoteLeadOptions options)
        {
            var r = await _leadService.PromoteLead(_accountManager, leadId, options);

            return Ok(r);

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
            var lead = await _leadService.RejectLead(_accountManager, leadId, input);

            return Ok(lead);

        }

        /// <summary>
        ///     Get leads for organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [Authorize]
        [ProducesResponseType(typeof(List<AccountManagerLeadOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLeads([FromRoute] Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var leads = await _leadService.GetLeads<AccountManagerLeadOutput>(_accountManager, filters);
            AddPagination(filters, leads.Total);
            return Ok(leads.Data);
        }

        /// <summary>
        ///     am get lead
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [HttpGet("{leadId}")]
        [ProducesResponseType(typeof(AccountManagerLeadDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLead([FromRoute] Guid organizationId, [FromRoute] Guid leadId)
        {
            var lead = await _leadService.GetLead<AccountManagerLeadDetailsOutput>(_accountManager, leadId);
            return Ok(lead);
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
            var lead = await _leadService.UpdateLead(_accountManager, leadId, model);

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
            var result = await _commentService.CreateLeadComment(_accountManager, leadId, input);
            return Ok(result);
        }
    }
}