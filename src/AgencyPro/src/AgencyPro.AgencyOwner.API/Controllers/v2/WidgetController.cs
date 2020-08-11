// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.Widgets.Services;
using AgencyPro.Core.Widgets.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class WidgetController : OrganizationUserController
    {
        private readonly IAgencyOwner _agencyOwner;
        private readonly IWidgetManager _widgetManager;

        public WidgetController(IWidgetManager widgetManager,
            IAgencyOwner agencyOwner,
            IServiceProvider provider) : base(provider)
        {
            _widgetManager = widgetManager;
            _agencyOwner = agencyOwner;
        }

        /// <summary>
        ///     Agency Owner Dashboard
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<CategoryWidgetOutput>), StatusCodes.Status200OK)]
        [HttpGet("AgencyStream")]
        public async Task<IActionResult> AgencyStreamWidget(
            [FromRoute] Guid organizationId,
            [FromBody] TimeMatrixFilters filters
        )
        {
            var vm = await _widgetManager.AgencyStreamsWidget(_agencyOwner, filters);
            return Ok(vm);
        }
    }
}