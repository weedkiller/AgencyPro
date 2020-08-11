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

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly ITimeMatrixService _timeService;
        private readonly IOrganizationCustomer _orgCustomer;
        private readonly IChartService _chartService;

        public TimeController(IServiceProvider serviceProvider, 
            IOrganizationCustomer orgCustomer, 
            ITimeMatrixService timeService,
            IChartService chartService) : base(serviceProvider)
        {
            _orgCustomer = orgCustomer;
            _timeService = timeService;
            _chartService = chartService;
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(CustomerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _timeService
                .GetComposedOutput(_orgCustomer, filters);

            return Ok(result);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="chartParams"></param>
        /// <returns></returns>
        [HttpGet("charts")]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_orgCustomer, filters, chartParams);
            return Ok(data);
        }
    }
}