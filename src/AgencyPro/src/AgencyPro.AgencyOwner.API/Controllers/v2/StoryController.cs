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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class StoryController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IProviderAgencyOwner _agencyOwner;
        private readonly IStoryService _storyService;

        public StoryController(
            ICommentService commentService,
            IStoryService storyService, IProviderAgencyOwner agencyOwner,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _agencyOwner = agencyOwner;
            _storyService = storyService;
        }

        /// <summary>
        ///     Gets stories
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerStoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStories([FromRoute] Guid organizationId, [FromQuery] StoryFilters filters)
        {
            var stories = await _storyService.GetStories<AgencyOwnerStoryOutput>(_agencyOwner, filters);
            AddPagination(filters, stories.Total);
            return Ok(stories.Data);
        }

        /// <summary>
        ///     Get story as agency owner
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <returns></returns>
        [HttpGet("{storyId}")]
        [ProducesResponseType(typeof(List<AgencyOwnerStoryDetailsOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStory([FromRoute] Guid organizationId,
            [FromRoute] Guid storyId
        )
        {
            var s2 = await _storyService.GetStory<AgencyOwnerStoryDetailsOutput>(_agencyOwner, storyId);

            return Ok(s2);
        }

        /// <summary>
        ///     Creates a story for a project
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(StoryResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> CreateStory([FromRoute] Guid organizationId, [FromBody] CreateStoryInput input)
        {
            var story = await _storyService.CreateStory(_agencyOwner, input);

            return Ok(story);
        }

        /// <summary>
        ///     Updates a story
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{storyId}")]
        [ProducesResponseType(typeof(StoryResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateStory([FromRoute] Guid organizationId, [FromRoute] Guid storyId,
            [FromBody] UpdateStoryInput model)
        {
            var story = await _storyService.UpdateStory(_agencyOwner,
                storyId, model);
            return Ok(story);
        }

        /// <summary>
        /// assign a story to a contractor within the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{storyId}/assign")]
        [ProducesResponseType(typeof(StoryResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> AssignStory([FromRoute] Guid organizationId, [FromRoute] Guid storyId,
            [FromBody] AssignStoryInput input)
        {

            var story = await _storyService.AssignStory(_agencyOwner,
                storyId, input);

            return Ok(story);
        }

        /// <summary>
        ///     deletes a story
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <returns></returns>
        [HttpDelete("{storyId}")]
        [ProducesResponseType(typeof(StoryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStory([FromRoute] Guid organizationId, [FromRoute] Guid storyId)
        {
            var result = await _storyService.DeleteStory(_agencyOwner, storyId);
            return Ok(result);
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
            var result = await _commentService.CreateStoryComment(_agencyOwner, storyId, input);
            return Ok(result);
        }
    }
}