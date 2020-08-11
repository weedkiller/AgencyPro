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

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class ProjectController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly ICommentService _commentService;
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService,
            IOrganizationCustomer customer,
            ICommentService commentService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {

            _customer = customer;
            _commentService = commentService;
            _projectService = projectService;
        }

        /// <summary>
        ///     Gets all the customer's projects within an organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="customerId">the customer id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<CustomerProjectOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjectsForCustomer(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid customerId,
            [FromQuery] ProjectFilters filters
        )
        {
            var projects =
                await _projectService.GetProjects<CustomerProjectOutput>(_customer, filters);
            AddPagination(filters, projects.Total);
            return Ok(projects.Data);
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
           await _projectService
                .PauseProject(_customer, projectId);

            return await GetProject(organizationId, projectId);
        }



        /// <summary>
        ///     get project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet("{projectId}")]
        [ProducesResponseType(typeof(CustomerProjectDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProject(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid projectId
        )
        {
            var project = await _projectService
                .GetProject<CustomerProjectDetailsOutput>(_customer, projectId);

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
            var result = await _commentService.CreateProjectComment(_customer, projectId, input);
            return Ok(result);
        }

        /// <summary>
        /// Ends the project
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPatch("{projectId}/end")]
        [ProducesResponseType(typeof(ProjectResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> EndProject(Guid organizationId, Guid projectId)
        {
            var result = await _projectService
                .EndProject(_customer, projectId);

            return Ok(result);
        }
    }
}