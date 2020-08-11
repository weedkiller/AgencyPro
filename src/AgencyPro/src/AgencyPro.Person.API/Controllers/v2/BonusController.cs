// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.BonusIntents.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("bonus")]
    [Produces("application/json")]
    public class BonusController : ControllerBase
    {
        private readonly IOrganizationPerson _person;
        private readonly IIndividualBonusIntentService _bonusService;

        public BonusController(IOrganizationPerson person, IIndividualBonusIntentService bonusService)
        {
            _person = person;
            _bonusService = bonusService;
        }

        [HttpGet("{organizationId}/pending")]
        [ProducesResponseType(typeof(List<IndividualBonusIntentOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBonuses([FromRoute]Guid organizationId, [FromQuery]BonusFilters filters)
        {
            var payouts = await _bonusService
                .GetPending(_person, filters);

            return Ok(payouts);
        }
    }
}