// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.StoryTemplates.Services;
using AgencyPro.Core.StoryTemplates.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class StoryTemplateController : OrganizationUserController
    {
        private readonly IStoryTemplateService _storyTemplateService;
        private readonly IOrganizationProjectManager _projectManager;

        public StoryTemplateController(
            IStoryTemplateService storyTemplateService,
            IOrganizationProjectManager projectManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _storyTemplateService = storyTemplateService;
            _projectManager = projectManager;
        }

        /// <summary>
        /// Get story templates for organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<StoryTemplateOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoryTemplates([FromRoute]Guid organizationId, [FromQuery] CommonFilters filters)
        {
            var templates = await _storyTemplateService.GetStoryTemplates(_projectManager, filters);
            AddPagination(filters, templates.Total);
            return Ok(templates.Data);
        }

        /// <summary>
        /// Get story template for organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyTemplateId"></param>
        /// <returns></returns>
        [HttpGet("{storyTemplateId}")]
        [ProducesResponseType(typeof(StoryTemplateOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoryTemplate([FromRoute]Guid organizationId, [FromRoute]Guid storyTemplateId)
        {
            var templates = await _storyTemplateService.GetStoryTemplate(_projectManager, storyTemplateId);
            return Ok(templates);
        }

        /// <summary>
        /// deletes a story template for an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyTemplateId"></param>
        /// <returns></returns>
        [HttpDelete("{storyTemplateId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute]Guid organizationId, [FromRoute]Guid storyTemplateId)
        {
            var templates = await _storyTemplateService.Delete(_projectManager, storyTemplateId);
            return Ok(templates);
        }

        /// <summary>
        /// Converts story to a template
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{storyId}")]
        [ProducesResponseType(typeof(StoryTemplateOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConvertStoryToTemplate([FromRoute]Guid organizationId, [FromRoute]Guid storyId, [FromBody]ConvertStoryInput input)
        {
            var templates = await _storyTemplateService.ConvertStory(_projectManager, storyId, input);
            return Ok(templates);
        }

        /// <summary>
        /// create a new story template
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="storyId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(StoryTemplateOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromRoute]Guid organizationId, [FromRoute]Guid storyId, [FromBody]StoryTemplateInput input)
        {
            var templates = await _storyTemplateService.Create(_projectManager, input);
            return Ok(templates);
        }

        /// <summary>
        /// update story template
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="templateId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{templateId}")]
        [ProducesResponseType(typeof(StoryTemplateOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTemplate([FromRoute]Guid organizationId, [FromRoute]Guid templateId, [FromBody]StoryTemplateInput input)
        {
            var templates = await _storyTemplateService.Update(_projectManager, templateId, input);
            return Ok(templates);
        }
    }
}