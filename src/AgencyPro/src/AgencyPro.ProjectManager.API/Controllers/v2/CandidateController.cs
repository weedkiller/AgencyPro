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

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class CandidateController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly ICandidateService _candidateService;
        private readonly IOrganizationProjectManager _projectManager;

        public CandidateController(
            ICommentService commentService,
            ICandidateService candidateService, 
            IOrganizationProjectManager projectManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _candidateService = candidateService;
            _projectManager = projectManager;
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
        
        public async Task<IActionResult> UpdateCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId,
            [FromBody] CandidateInput model)
        {
            var candidate =
                await _candidateService.UpdateCandidate(_projectManager, candidateId,
                    model);
            return Ok(candidate);
        }

        /// <summary>
        ///     Mark a candidate as rejected by project manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{candidateId}/reject")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId,
            [FromBody] CandidateRejectionInput input)
        {
            var candidate =
                await _candidateService.RejectCandidate(_projectManager, candidateId,
                    input);

            return Ok(candidate);
        }

        /// <summary>
        ///     get candidate
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        [HttpGet("{candidateId}")]
        [ProducesResponseType(typeof(ProjectManagerCandidateDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId)
        {
            var candidate =
                await _candidateService.GetCandidate<ProjectManagerCandidateDetailsOutput>(_projectManager, candidateId);

            return Ok(candidate);
        }

        /// <summary>
        ///     Hires the candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{candidateId}/accept")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> AcceptCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId,
            [FromBody] CandidateAcceptInput model)
        {
            var co = await _candidateService.Promote(_projectManager,
                candidateId, model);

            return Ok(co);
        }

        /// <summary>
        ///     Gets candidates for a recruiter
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProjectManagerCandidateOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidates([FromRoute] Guid organizationId,
            [FromQuery] CommonFilters filters)
        {
            var candidates = await _candidateService.GetCandidates<ProjectManagerCandidateOutput>(_projectManager, filters);
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
            var result = await _candidateService.DeleteCandidate(_projectManager, candidateId);

            return Ok(result);
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
            var result = await _commentService.CreateCandidateComment(_projectManager, candidateId, input);
            return Ok(result);
        }
    }
}