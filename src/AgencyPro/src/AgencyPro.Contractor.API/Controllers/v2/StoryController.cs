// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class StoryController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationContractor _contractor;
        private readonly IStoryService _storyService;

        public StoryController(
            ICommentService commentService,
            IStoryService storyService, IOrganizationContractor contractor,
            IServiceProvider provider) : base(provider)
        {
            _commentService = commentService;
            _contractor = contractor;
            _storyService = storyService;
        }

        /// <summary>
        ///     gets a specific story
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId">the story id</param>
        /// <returns></returns>
        [HttpGet("{storyId}")]
        [ProducesResponseType(typeof(ContractorStoryDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid organizationId, [FromRoute] Guid storyId
        )
        {
            var story = await _storyService.GetStory<ContractorStoryDetailsOutput>(_contractor, storyId);
            return Ok(story);
        }

        /// <summary>
        ///     Gets stories
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<ContractorStoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssignedStories([FromRoute] Guid organizationId,
            [FromQuery] StoryFilters filters)
        {
            var stories = await _storyService.GetStories<ContractorStoryOutput>(_contractor, filters);
            AddPagination(filters, stories.Total);
            return Ok(stories.Data);
        }

        /// <summary>
        /// Add comment to a story
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{storyId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromRoute] Guid organizationId, [FromRoute] Guid storyId,
            [FromBody] CommentInput input)
        {
            var result = await _commentService.CreateStoryComment(_contractor, storyId, input);
            return Ok(result);
        }
    }
}