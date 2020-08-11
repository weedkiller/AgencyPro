// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Comments.ViewModels;
using AgencyPro.Core.Contracts.Filters;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IContractService _contractService;
        private readonly IOrganizationProjectManager _projectManager;

        public ContractController(
            ICommentService commentService,
            IContractService contractService,
            IOrganizationProjectManager projectManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _contractService = contractService;
            _projectManager = projectManager;
        }

        /// <summary>
        ///     Project Manager can get their contracts
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ProjectManagerContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts(
            [FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters
        )
        {
            var contracts = await _contractService
                .GetContracts<ProjectManagerContractOutput>(_projectManager, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        ///     Get contract
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("{contractId}")]
        [ProducesResponseType(typeof(ProjectManagerContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContract(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid contractId
        )
        {
            var contract = await _contractService
                .GetContract<ProjectManagerContractDetailsOutput>(_projectManager, contractId);

            return Ok(contract);
        }

        /// <summary>
        /// Add comment to a contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{contractId}/comments")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddComment([FromRoute] Guid organizationId, [FromRoute] Guid contractId,
            [FromBody] CommentInput input)
        {
            var result = await _commentService.CreateContractComment(_projectManager, contractId, input);
            return Ok(result);
        }
    }
}