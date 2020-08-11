// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Services;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class CandidateController : OrganizationUserController
    {
        private readonly ICandidateService _candidateService;
        private readonly ICommentService _commentService;
        private readonly IOrganizationRecruiter _recruiter;

        public CandidateController(ICandidateService candidateService,
            ICommentService commentService,
            IOrganizationRecruiter recruiter,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _candidateService = candidateService;
            _commentService = commentService;
            _recruiter = recruiter;
        }

        /// <summary>
        ///     creates a new candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> CreateCandidate([FromRoute] Guid organizationId,[FromBody] CandidateInput model
        )
        {
            var candidate =
                await _candidateService.CreateInternalCandidate(_recruiter, model);
            return Ok(candidate);
        }

        /// <summary>
        /// Create external candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="externalId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{externalId}")] [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateExternalCandidate(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid externalId,
            [FromBody] CandidateInput model)
        {


            var am = await _candidateService.CreateExternalCandidate(_recruiter, externalId, model);
            return Ok(am);
        }

        /// <summary>
        ///     updates a candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{candidateId}")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateCandidate(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid candidateId,
            [FromBody] CandidateInput model
        )
        {
            var candidate = await _candidateService.UpdateCandidate(_recruiter, candidateId, model);

            return Ok(candidate);
        }

        /// <summary>
        ///     get candidate
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        [HttpGet("{candidateId}")]
        [ProducesResponseType(typeof(RecruiterCandidateDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidate([FromRoute] Guid organizationId,
            [FromRoute] Guid candidateId
        )
        {
            var candidate = await _candidateService.GetCandidate<RecruiterCandidateDetailsOutput>(_recruiter,
                candidateId);
            return Ok(candidate);
        }

        /// <summary>
        ///     Gets candidates for a recruiter
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<RecruiterCandidateOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidates([FromRoute] Guid organizationId,
            [FromQuery] CommonFilters filters
        )
        {
            var candidates = await _candidateService.GetCandidates<RecruiterCandidateOutput>(_recruiter, filters);
            AddPagination(filters, candidates.Total);
            return Ok(candidates.Data);
        }

        /// <summary>
        ///     deletes a candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        [HttpDelete("{candidateId}")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId)
        {
            var candidate = await _candidateService.DeleteCandidate(_recruiter, candidateId);

            return Ok(candidate);
        }


        /// <summary>
        /// Add comment to a candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{candidateId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromRoute] Guid organizationId, [FromRoute] Guid candidateId,
            [FromBody] CommentInput input)
        {
            var result = await _commentService.CreateCandidateComment(_recruiter, candidateId, input);
            return Ok(result);
        }
    }
}