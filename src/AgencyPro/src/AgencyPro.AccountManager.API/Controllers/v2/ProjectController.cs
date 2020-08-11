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

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class ProjectController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IProjectService _projectService;

        public ProjectController(
            ICommentService commentService,
            IServiceProvider serviceProvider,
            IOrganizationAccountManager accountManager,
            IProjectService projectService) : base(serviceProvider)
        {
            _commentService = commentService;
            _accountManager = accountManager;
            _projectService = projectService;
        }

        /// <summary>
        ///     Get projects for the currently logged-in account manager
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerProjectOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsForOrganizationAccountManager(
            [FromRoute] Guid organizationId,
            [FromQuery] ProjectFilters filters
        )
        {
            var projects = await _projectService
                .GetProjects<AccountManagerProjectOutput>(_accountManager, filters);
            AddPagination(filters, projects.Total);
            return Ok(projects.Data);
        }

        /// <summary>
        ///     Gets a project for currently logged-in account manager
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(AccountManagerProjectDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProject(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid projectId
        )
        {
            var project = await _projectService
                .GetProject<AccountManagerProjectDetailsOutput>(_accountManager, projectId);

            return Ok(project);
        }

       
        /// <summary>
        /// Adds a new project
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        
        public async Task<dynamic> CreateProject(
            [FromRoute] Guid organizationId,
            [FromBody] ProjectInput input
        )
        {
            var project = await _projectService
                .CreateProject(
                    _accountManager,
                    input);

            return Ok(project);

        }

        /// <summary>
        /// Updates a project details
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
                .UpdateProject(_accountManager, projectId, model);

            return Ok(p);

        }

        /// <summary>
        /// Pauses a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/pause")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> PauseProject(Guid organizationId, Guid projectId)
        {
            var project = await _projectService.PauseProject(_accountManager, projectId);

            return Ok(project);
        }

        /// <summary>
        /// Restarts a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/restart")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> RestartProject(Guid organizationId, Guid projectId)
        {
            var result = await _projectService
                .RestartProject(_accountManager, projectId);

            return Ok(result);
        }

        /// <summary>
        /// Ends a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/end")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EndProject(Guid organizationId, Guid projectId)
        {
            var result = await _projectService
                .EndProject(_accountManager, projectId);

            return Ok(result);
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
            var result = await _commentService.CreateProjectComment(_accountManager, projectId, input);
            return Ok(result);
        }
    }
}