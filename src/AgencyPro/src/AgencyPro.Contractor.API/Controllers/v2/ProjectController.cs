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

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class ProjectController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationContractor _contractor;
        private readonly IProjectService _projectService;

        public ProjectController(
            ICommentService commentService,
            IServiceProvider serviceProvider, IOrganizationContractor contractor,
            IProjectService projectService) : base(serviceProvider)
        {
            _commentService = commentService;
            _contractor = contractor;
            _projectService = projectService;
        }

        /// <summary>
        ///     Gets all projects for the current contractor in an organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ContractorProjectOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjects([FromRoute] Guid organizationId, [FromQuery] ProjectFilters filters)
        {
            var projects = await _projectService.GetProjects<ContractorProjectOutput>(_contractor, filters);
            AddPagination(filters, projects.Total);
            return Ok(projects.Data);
        }

        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(ContractorProjectDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProject([FromRoute] Guid organizationId, [FromRoute] Guid projectId)
        {
            var project = await _projectService.GetProject<ContractorProjectDetailsOutput>(_contractor, projectId);

            return Ok(project);
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
            var result = await _commentService.CreateProjectComment(_contractor, projectId, input);
            return Ok(result);
        }
    }
}