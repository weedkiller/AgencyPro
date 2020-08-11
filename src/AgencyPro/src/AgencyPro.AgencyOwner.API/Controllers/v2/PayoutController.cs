// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.DisperseFunds.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.PayoutIntents.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class PayoutController : OrganizationUserController
    {
        private readonly IDisperseFundsService _payoutService;
        private readonly IAgencyOwner _agencyOwner;
        private readonly IProviderAgencyOwner _providerAgencyOwner;

        public PayoutController(
            IAgencyOwner agencyOwner,
            IProviderAgencyOwner providerAgencyOwner,
            IServiceProvider serviceProvider,
            IDisperseFundsService payoutService) : base(serviceProvider)
        {
            _agencyOwner = agencyOwner;
            _providerAgencyOwner = providerAgencyOwner;
            _payoutService = payoutService;
        }
        
        /// <summary>
        /// gets pending payouts from the platform to the organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("pending")]
        [ProducesResponseType(typeof(List<OrganizationPayoutIntentOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayouts([FromRoute]Guid organizationId, [FromQuery]PayoutFilters filters)
        {
            var result = await _payoutService.GetPending(_agencyOwner, filters);
            return Ok(result);
        }

        /// <summary>
        /// Disperse funds from invoice
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpPost("invoices/{invoiceId}/disperse")]
        [ProducesResponseType(typeof(List<OrganizationPayoutIntentOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Disperse([FromRoute]Guid organizationId, [FromRoute] string invoiceId)
        {
            var result = await _payoutService.DisperseInvoice(_providerAgencyOwner, invoiceId);
            return Ok(result);
        }
    }
}
