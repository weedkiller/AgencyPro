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

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly IOrganizationContractor _contractor;
        private readonly ICommentService _commentService;
        private readonly IContractService _contractService;

        public ContractController(
            ICommentService commentService,
            IContractService contractService, IOrganizationContractor contractor,
            IServiceProvider provider) : base(provider)
        {
            _commentService = commentService;
            _contractService = contractService;
            _contractor = contractor;
        }

        /// <summary>
        ///     Contractor can get their contracts
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<ContractorContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService.GetContracts<ContractorContractOutput>(_contractor, filters);
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
        [ProducesResponseType(typeof(ContractorContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId
        )
        {
            var contract =
                await _contractService.GetContract<ContractorContractDetailsOutput>(_contractor, contractId);

            return Ok(contract);
        }


        /// <summary>
        ///     Updates a contract details
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId, [FromBody] UpdateProviderContractInput model)
        {
            var c = await _contractService.UpdateContract(_contractor,
                contractId, model);

            return Ok(c);

        }

        /// <summary>
        ///     Pauses a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/pause")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Pause([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.PauseContract(_contractor, contractId);

            return Ok(contract);

        }

        /// <summary>
        ///     ends a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/end")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> End([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.EndContract(_contractor, contractId);

            return Ok(contract);

        }

        /// <summary>
        ///     restarts a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/restart")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Restart([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.RestartContract(_contractor, contractId);

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
            var result = await _commentService.CreateContractComment(_contractor, contractId, input);
            return Ok(result);
        }
    }
}