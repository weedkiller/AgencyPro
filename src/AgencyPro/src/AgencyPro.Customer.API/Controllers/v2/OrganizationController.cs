// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class OrganizationController : OrganizationUserController
    {

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationController)}.{callerName}] - {message}";
        }

        private readonly IOrganizationCustomerService _customerService;
        private readonly IOrganizationCustomer _customer;
        private readonly IOrganizationService _organizationService;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(
            IOrganizationCustomerService customerService,
            IOrganizationService organizationService, 
            ILogger<OrganizationController> logger,
            IOrganizationCustomer customer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customerService = customerService;
            _customer = customer;
            _organizationService = organizationService;
            _logger = logger;
        }

        /// <summary>
        ///     Gets an organization
        /// </summary>
        /// <param name="organizationId">The Organization ID</param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(CustomerOrganizationDetailsOutput), 200)]
        public async Task<IActionResult> GetOrganization([FromRoute] Guid organizationId)
        {
            var vm = await _organizationService.GetOrganization<CustomerOrganizationDetailsOutput>(organizationId);

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of providers that allow customers to create an account
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ProducesResponseType(typeof(List<CustomerProviderOrganizationOutput>), 200)]
        public async Task<IActionResult> GetProviderOrganizationList([FromRoute] Guid organizationId, [FromQuery]OrganizationFilters filters)
        {
            var vm = await _organizationService
                .GetProviderOrganizations<CustomerProviderOrganizationOutput>(_customer, filters);

            return Ok(vm);
        }

        /// <summary>
        /// Provider org summary 
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(CustomerProviderOrganizationSummary), 200)]
        public async Task<IActionResult> GetProviderOrganizationSummary([FromRoute] Guid organizationId, [FromQuery]OrganizationFilters filters)
        {
            var vm = await _organizationService
                .GetProviderOrganizationSummary(_customer, filters);

            return Ok(vm);
        }

        /// <summary>
        ///     Upgrades an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("upgrade")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpgradeOrganization([FromRoute] Guid organizationId,
            [FromBody] OrganizationUpgradeInput input)
        {

            var result = await _organizationService.UpgradeOrganization(_customer, input);

            _logger.LogDebug(GetLogMessage("Organization Upgraded: {@result}"), result);

            if (!result.Succeeded || !result.OrganizationId.HasValue)
                throw new ApplicationException("Unable to upgrade");

            var viewModel = await _organizationService
                .GetOrganization<AgencyOwnerOrganizationDetailsOutput>(result.OrganizationId.Value);

            return Ok(viewModel);
        }

        /// <summary>
        /// updates an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("")]
        [ProducesResponseType(typeof(CustomerOrganizationOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrganization([FromRoute] Guid organizationId,
            [FromBody] OrganizationUpdateInput input)
        {
            var vm = await _organizationService.UpdateBuyerOrganization(_customer, input);
            if (vm.Succeeded)
            {
                return await GetOrganization(organizationId);
            }

            return Ok(vm);
        }

        [HttpGet("counts")]
        [ProducesResponseType(typeof(CustomerCounts), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts([FromRoute] Guid organizationId)
        {
            var counts = await _customerService.GetCounts(_customer);
            return Ok(counts);
        }

        /// <summary>
        ///     Updates an organization logo (200x200px is best)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="logo">The logo to upload</param>
        /// <returns></returns>
        [HttpPatch("logo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateOrganizationPic([FromRoute] Guid organizationId,
            [FromForm] IFormFile logo)
        {
            var result = await _organizationService.UpdateOrganizationPic(_customer, logo);

            if (result.Succeeded)
            {
                return Accepted();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return new UnprocessableEntityObjectResult(ModelState);
        }
    }
}