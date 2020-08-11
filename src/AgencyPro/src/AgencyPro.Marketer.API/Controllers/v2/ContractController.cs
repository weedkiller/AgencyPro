// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Contracts.Filters;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class ContractController : OrganizationUserController
    {
        private readonly IContractService _contractService;
        private readonly IOrganizationMarketer _marketer;

        public ContractController(IContractService contractService,
            IOrganizationMarketer marketer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _marketer = marketer;
            _contractService = contractService;
        }

        /// <summary>
        ///     Get contracts
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<MarketerContractOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId,
            [FromQuery] ContractFilters filters)
        {
            var contracts = await _contractService.GetContracts<MarketerContractOutput>(_marketer, filters);
            AddPagination(filters, contracts.Total);
            return Ok(contracts.Data);
        }

        /// <summary>
        ///     Gets a contract
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpGet("{contractId}")]
        [ProducesResponseType(typeof(MarketerContractDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromRoute] Guid contractId
        )
        {
            var contract =
                await _contractService.GetContract<MarketerContractDetailsOutput>(_marketer, contractId);

            return Ok(contract);
        }

    }
}