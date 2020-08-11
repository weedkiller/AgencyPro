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

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IContractService _contractService;

        public ContractController(
            ICommentService commentService,
            IContractService contractService, IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _commentService = commentService;
            _accountManager = accountManager;
            _contractService = contractService;
        }

        /// <summary>
        ///     Get contracts that belong to account manager
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService.GetContracts<AccountManagerContractOutput>(_accountManager, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        ///     Get specific contract that belongs to account manager
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("{contractId}")]
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.GetContract<AccountManagerContractDetailsOutput>(_accountManager, contractId);

            return Ok(contract);
        }

        /// <summary>
        ///     Creates a new contract
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        
        [HttpPost("")]
        public async Task<IActionResult> CreateContract([FromRoute] Guid organizationId, [FromBody] ContractInput model)
        {
            var contract = await _contractService.CreateContract(_accountManager, model);
            if (contract.Succeeded)
            {
                return await GetContract(organizationId, contract.ContractId.Value);

            }

            return BadRequest();
        }
        
        /// <summary>
        ///     Updates a contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}")]
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId, [FromBody] UpdateProviderContractInput model)
        {
            var contract = await _contractService.UpdateContract(_accountManager,
                contractId, model);

            return Ok(contract);

        }

        /// <summary>
        ///     Pauses a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/pause")]
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Pause([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.PauseContract(_accountManager, contractId);
            if (contract.Succeeded)
            {
                return await GetContract(organizationId, contract.ContractId.Value);

            }

            return BadRequest();
        }

        /// <summary>
        ///     Ends a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/end")]
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> End([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.EndContract(_accountManager, contractId);
            if (contract.Succeeded)
            {
                return await GetContract(organizationId, contract.ContractId.Value);

            }

            return BadRequest();

        }

        /// <summary>
        ///    Restarts a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/restart")]
        [ProducesResponseType(typeof(AccountManagerContractDetailsOutput), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Restart([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.RestartContract(_accountManager, contractId);
            if (contract.Succeeded)
            {
                return await GetContract(organizationId, contract.ContractId.Value);

            }

            return BadRequest();
        }
        
        /// <summary>
        ///     Adds a comment to a contract
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
            var result = await _commentService.CreateContractComment(_accountManager, contractId, input);
            return Ok(result);
        }
    }
}