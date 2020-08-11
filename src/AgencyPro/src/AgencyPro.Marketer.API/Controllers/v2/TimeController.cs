// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;
using AgencyPro.Core.Chart;
using AgencyPro.Core.Chart.Services;
using AgencyPro.Core.TimeEntries.ViewModels;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly IOrganizationMarketer _marketer;
        private readonly ITimeMatrixService _timeMatrix;
        private readonly IChartService _chartService;

        public TimeController(ITimeMatrixService timeMatrix,
            IOrganizationMarketer marketer,
            IServiceProvider serviceProvider,
            IChartService chartService) : base(serviceProvider)
        {
            _marketer = marketer;
            _timeMatrix = timeMatrix;
            _chartService = chartService;
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(MarketerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _timeMatrix.GetComposedOutput(_marketer, filters);
            return Ok(result);
        }
        [HttpGet("charts")]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_marketer, filters, chartParams);
            return Ok(data);
        }
    }
}