// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Contracts.Filters;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly IOrganizationRecruiter _recruiter;
        private readonly ICommentService _commentService;
        private readonly IContractService _contractService;

        public ContractController(
            ICommentService commentService,
            IContractService contractService, 
            IOrganizationRecruiter recruiter,
            IServiceProvider provider) : base(provider)
        {
            _commentService = commentService;
            _contractService = contractService;
            _recruiter = recruiter;
        }

        /// <summary>
        ///     recruiter can get their contracts
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<RecruiterContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService
                .GetContracts<RecruiterContractOutput>(_recruiter, filters);
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
        [ProducesResponseType(typeof(RecruiterContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId
        )
        {
            var contract =
                await _contractService.GetContract<RecruiterContractDetailsOutput>(_recruiter, contractId);

            return Ok(contract);
        }

        
    }
}