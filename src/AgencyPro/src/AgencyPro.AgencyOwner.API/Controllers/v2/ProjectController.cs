// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Filters;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Projects.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ProjectController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IProviderAgencyOwner _agencyOwner;
        private readonly IBillingCategoryService _billingCategoryService;
        private readonly IProjectService _projectService;

        public ProjectController(
            ICommentService commentService,
            IProviderAgencyOwner agencyOwner,
            IBillingCategoryService billingCategoryService,
            IProjectService projectService,
            IServiceProvider provider) : base(provider)
        {
            _commentService = commentService;
            _agencyOwner = agencyOwner;
            _billingCategoryService = billingCategoryService;
            _projectService = projectService;
        }

        /// <summary>
        ///     Get projects
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AgencyOwnerProjectOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsForAgencyOwner(
            [FromRoute] Guid organizationId,
            [FromQuery] ProjectFilters filters
        )
        {
            var projects = await _projectService
                .GetProjects<AgencyOwnerProjectOutput>(_agencyOwner, filters);
            AddPagination(filters, projects.Total);
            return Ok(projects.Data);
        }

        /// <summary>
        ///     Updates a project
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
                .UpdateProject(_agencyOwner, projectId, model);

            return Ok(p);
        }

        /// <summary>
        ///     Adds a project
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
            var output = await _projectService
                .CreateProject(_agencyOwner, input);

            return Ok(output);
        }

        /// <summary>
        ///     deletes a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid organizationId, [FromRoute] Guid projectId)
        {
            var result = await _projectService.DeleteProject(_agencyOwner, projectId);

            return Ok(result);
        }

        /// <summary>
        ///     Gets a project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(AgencyOwnerProjectDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProject(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid projectId
        )
        {
            var project = await _projectService
                .GetProject<AgencyOwnerProjectDetailsOutput>(_agencyOwner, projectId);

            return Ok(project);
        }

        /// <summary>
        ///     Pause a project
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="projectId">the project id</param>
        /// <returns></returns>
        [HttpPatch("{projectId}/pause")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> PauseProject(Guid organizationId, Guid projectId)
        {
           var project = await _projectService.PauseProject(_agencyOwner, projectId);

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
                .RestartProject(_agencyOwner, projectId);

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
                .EndProject(_agencyOwner, projectId);

            return Ok(result);
        }

        [HttpDelete("{projectId}/BillingCategory/{billingCategoryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]Guid projectId, [FromRoute]int billingCategoryId)
        {
            var org =
                await _billingCategoryService
                    .RemoveBillingCategoryFromProject
                        (_agencyOwner, projectId, billingCategoryId);

            return Ok(org);
        }

        [HttpPut("{projectId}/BillingCategory/{billingCategoryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBillingCategory(
            [FromRoute]Guid organizationId, [FromRoute]Guid projectId, [FromRoute]int billingCategoryId)
        {
            var org =
                await _billingCategoryService
                    .AddBillingCategoryToProject
                        (_agencyOwner, projectId, billingCategoryId);

            return Ok(org);
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
            var result = await _commentService.CreateProjectComment(_agencyOwner, projectId, input);
            return Ok(result);
        }

    }
}