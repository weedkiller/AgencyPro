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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class CandidateController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IProviderAgencyOwner _agencyOwner;
        private readonly ICandidateService _candidateService;

        public CandidateController(
            ICommentService commentService,
            ICandidateService candidateService,
            IProviderAgencyOwner agencyOwner,
            IServiceProvider mapper) : base(mapper)
        {
            _commentService = commentService;
            _agencyOwner = agencyOwner;
            _candidateService = candidateService;
        }

        /// <summary>
        ///     Gets all pending candidates
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerCandidateOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidates([FromRoute] Guid organizationId,
            [FromQuery] CommonFilters filters
        )
        {
            var candidates = await _candidateService
                .GetActiveCandidates<AgencyOwnerCandidateOutput>(_agencyOwner, filters);
            AddPagination(filters, candidates.Total);
            return Ok(candidates.Data);
        }

        /// <summary>
        ///     Gets a candidate by Id
        /// </summary>
        /// <param name="organizationId">the org id</param>
        /// <param name="candidateId"></param>
        /// <returns></returns>
        [HttpGet("{candidateId}")]
        [ProducesResponseType(typeof(AgencyOwnerCandidateDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCandidate([FromRoute] Guid organizationId, [FromRoute] Guid candidateId)
        {
            var candidate =
                await _candidateService.GetCandidate<AgencyOwnerCandidateDetailsOutput>(_agencyOwner, candidateId);
            return Ok(candidate);
        }

        /// <summary>
        ///     Updates a candidate
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
            var candidate =
                await _candidateService.UpdateCandidate(_agencyOwner, candidateId, model);
            return Ok(candidate);
        }

        /// <summary>
        ///     Approves a candidate 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{candidateId}/qualify")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> QualifyCandidate(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid candidateId,
            [FromBody] CandidateQualifyInput input
        )
        {
            var candidate =
                await _candidateService.QualifyCandidate(_agencyOwner, candidateId, input);
            return Ok(candidate);
        }


        /// <summary>
        ///     Rejects a candidate
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="candidateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{candidateId}/reject")]
        [ProducesResponseType(typeof(CandidateResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectCandidate(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid candidateId,
            [FromBody] CandidateRejectionInput input)
        {
            var candidate = await _candidateService
                .RejectCandidate(_agencyOwner, candidateId, input);

            return Ok(candidate);
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
            var result = await _candidateService.DeleteCandidate(_agencyOwner, candidateId);

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
            var result = await _commentService.CreateCandidateComment(_agencyOwner, candidateId, input);
            return Ok(result);
        }
    }
}