// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public partial class OrganizationController
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(OrganizationController)}.{callerName}] - {message}";
        }

        [HttpPost("provider")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Upgrade([FromRoute] Guid organizationId,
            [FromBody] ProviderOrganizationUpgradeInput input)
        {
            _logger.LogInformation(GetLogMessage("Provider") );


            if (_agencyOwner.IsProviderOwner)
            {
                throw new ApplicationException("Agency is already a provider agency");
            }

            var result = await _organizationService
                .UpgradeOrganization(_agencyOwner, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }

        [HttpPost("marketing")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Upgrade([FromRoute] Guid organizationId,
            [FromBody] MarketingOrganizationUpgradeInput input)
        {
            if (_agencyOwner.IsMarketingOwner)
                throw new ApplicationException("Agency is already a marketing agency");

            var result = await _organizationService
                .UpgradeOrganization(_agencyOwner, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }

        [HttpPost("recruiting")]
        [ProducesResponseType(typeof(AgencyOwnerOrganizationDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Upgrade([FromRoute] Guid organizationId,
            [FromBody] RecruitingOrganizationUpgradeInput input)
        {
            if (_agencyOwner.IsRecruitingOwner) 
                throw new ApplicationException("Agency is already a recruiting agency");

            var result = await _organizationService
                .UpgradeOrganization(_agencyOwner, input);

            if (result.Succeeded)
            {
                return await Get(organizationId);
            }

            return Ok(result);
        }
    }
}