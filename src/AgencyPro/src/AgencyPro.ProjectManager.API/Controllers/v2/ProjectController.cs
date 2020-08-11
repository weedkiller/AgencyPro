// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Filters;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class ProjectController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationProjectManager _projectManager;
        private readonly IProjectService _projectService;

        public ProjectController(
            ICommentService commentService,
            IOrganizationProjectManager projectManager,
            IProjectService projectService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _projectManager = projectManager;
            _projectService = projectService;
        }

        /// <summary>
        ///     gets all projects for the currently logged in project manager
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProjectManagerProjectOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromRoute] Guid organizationId,
            [FromQuery] ProjectFilters filters
        )
        {
            var projects =
                await _projectService.GetProjects<ProjectManagerProjectOutput>(_projectManager, filters);
            AddPagination(filters, projects.Total);
            return Ok(projects.Data);
        }


        /// <summary>
        ///     Project manager get project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ProjectManagerProjectDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProject(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid projectId
        )
        {
            var project = await _projectService
                .GetProject<ProjectManagerProjectDetailsOutput>(_projectManager, projectId);

            return Ok(project);
        }

        /// <summary>
        ///     Updates a project details
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid organizationId,
            [FromRoute] Guid projectId,
            [FromBody] UpdateProjectInput model)
        {
            var p = await _projectService
                .UpdateProject
                    (_projectManager, projectId, model);

            return Ok(p);
        }

        /// <summary>
        /// Add comment to a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{projectId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromRoute] Guid organizationId, [FromRoute] Guid projectId,
            [FromBody] CommentInput input)
        {
            var result = await _commentService.CreateProjectComment(_projectManager, projectId, input);
            return Ok(result);
        }
    }
}