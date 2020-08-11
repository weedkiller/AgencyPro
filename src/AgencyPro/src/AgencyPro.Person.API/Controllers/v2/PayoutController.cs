// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Commission.Services;
using AgencyPro.Core.Commission.ViewModels;
using AgencyPro.Core.DisperseFunds.Services;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.PayoutIntents.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("payout")]
    [Produces("application/json")]
    public class PayoutController : ControllerBase
    {
        private readonly IDisperseFundsService _payoutService;
        private readonly ICommissionService _commissionService;
        private readonly IOrganizationPerson _person;

        public PayoutController(
            IOrganizationPerson person,
            IDisperseFundsService payoutService,
            ICommissionService commissionService,
            IServiceProvider serviceProvider)
        {
            _person = person;
            _payoutService = payoutService;
            _commissionService = commissionService;
        }

        [HttpGet("{organizationId}/pending")]
        [ProducesResponseType(typeof(List<IndividualPayoutIntentOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPayouts([FromRoute]Guid organizationId, [FromQuery]PayoutFilters filters)
        {
            var payouts = await _payoutService
                .GetPending(_person, filters);

            return Ok(payouts);
        }

        /// <summary>
        /// Get commission
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet("{organizationId}/commission")]
        [ProducesResponseType(typeof(StreamOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Commission([FromRoute]Guid organizationId)
        {
            var commission = await _commissionService.GetCommission(_person);

            return Ok(commission);
        }
    }
}
