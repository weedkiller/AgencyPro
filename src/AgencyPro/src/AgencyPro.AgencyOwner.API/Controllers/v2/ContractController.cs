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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly ICommentService _commentService;
        private readonly IAgencyOwner _ao;
        private readonly Lazy<IMarketingAgencyOwner> _mao;
        private readonly Lazy<IProviderAgencyOwner> _pao;
        private readonly Lazy<IRecruitingAgencyOwner> _rao;

        private readonly IContractService _contractService;

        public ContractController(
            ICommentService commentService,
            IAgencyOwner ao,
            Lazy<IMarketingAgencyOwner> mao,
            Lazy<IProviderAgencyOwner> pao,
            Lazy<IRecruitingAgencyOwner> rao,
            IContractService contractService, IServiceProvider provider)
            : base(provider)
        {
            _commentService = commentService;
            _ao = ao;
            _mao = mao;
            _pao = pao;
            _rao = rao;

            _contractService = contractService;
        }

        /// <summary>
        ///     Get contracts
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("provider")]
        [ProducesResponseType(typeof(List<AgencyOwnerProviderContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProviderContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService
                .GetProviderContracts<AgencyOwnerProviderContractOutput>(_pao.Value, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        /// get marketing contracts
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("marketing")]
        [ProducesResponseType(typeof(List<AgencyOwnerMarketingContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {

            var contracts = await _contractService
                .GetMarketingContracts<AgencyOwnerMarketingContractOutput>(_mao.Value, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        /// get recruiting contracts
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("recruiting")]
        [ProducesResponseType(typeof(List<AgencyOwnerRecruitingContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruitingContracts([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService
                .GetRecruitingContracts<AgencyOwnerRecruitingContractOutput>(_rao.Value, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        /// creates a new contract
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AgencyOwnerProviderContractDetailsOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        
        [HttpPost("")]
        public async Task<IActionResult> CreateContract([FromRoute] Guid organizationId, [FromBody] ContractInput model)
        {
            var contract = await _contractService
                .CreateContract
                    (_pao.Value, model);

            return Ok(contract);
        }

        /// <summary>
        ///     Gets a contract by id
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("provider/{contractId}")]
        [ProducesResponseType(typeof(AgencyOwnerProviderContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService
                    .GetProviderContract<AgencyOwnerProviderContractDetailsOutput>
                        (_pao.Value, contractId);

            return Ok(contract);
        }

        /// <summary>
        /// Get marketing contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("marketing/{contractId}")]
        [ProducesResponseType(typeof(AgencyOwnerMarketingContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketingContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService
                    .GetMarketingContract<AgencyOwnerMarketingContractDetailsOutput>
                        (_mao.Value, contractId);

            return Ok(contract);
        }

        /// <summary>
        /// Get recruiting contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("recruiting/{contractId}")]
        [ProducesResponseType(typeof(AgencyOwnerRecruitingContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruitingContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService
                    .GetRecruitingContract<AgencyOwnerRecruitingContractDetailsOutput>
                        (_rao.Value, contractId);

            return Ok(contract);
        }

        /// <summary>
        ///     Updates a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("provider/{contractId}")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId, [FromBody] UpdateProviderContractInput model)
        {
            var contract =
                await _contractService
                    .UpdateContract
                        (_pao.Value, contractId, model);

            return Ok(contract);
        }

        /// <summary>
        /// update marketing contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("marketing/{contractId}")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId, [FromBody] UpdateMarketingContractInput model)
        {
            var contract =
                await _contractService
                    .UpdateContract
                        (_mao.Value, contractId, model);

            return Ok(contract);
        }
        [HttpPatch("recruiting/{contractId}")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> UpdateContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId, [FromBody] UpdateRecruitingContractInput model)
        {
            var contract =
                await _contractService
                    .UpdateContract
                        (_rao.Value, contractId, model);

            return Ok(contract);
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
                await _contractService
                    .PauseContract(_pao.Value, contractId);
            return Ok(contract);
        }

        /// <summary>
        ///     Deletes a contract
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpDelete("{contractId}")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContract([FromRoute] Guid organizationId, [FromRoute] Guid contractId)
        {
            var result = await _contractService
                .DeleteContract(_pao.Value, contractId);

            return Ok(result);
        }
        

        /// <summary>
        ///     ends a contract 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPatch("{contractId}/end")]
        [ProducesResponseType(typeof(ContractResult), StatusCodes.Status200OK)]
        
        public async Task<IActionResult> EndContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.EndContract(_pao.Value, contractId);

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
        
        public async Task<IActionResult> RestartContract([FromRoute] Guid organizationId,
            [FromRoute] Guid contractId)
        {
            var contract =
                await _contractService.RestartContract(_pao.Value, contractId);

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
            var result = await _commentService.CreateContractComment(_ao, contractId, input);
            return Ok(result);
        }
    }
}