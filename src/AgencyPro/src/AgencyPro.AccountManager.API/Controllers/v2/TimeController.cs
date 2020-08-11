// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Chart;
using AgencyPro.Core.Chart.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly ITimeEntryService _timeEntryService;
        private readonly IChartService _chartService;
        private readonly IOrganizationAccountManager _am;
        private readonly ITimeMatrixService _matrixService;

        public TimeController(
            ITimeEntryService timeEntryService,
            IChartService chartService,
            IServiceProvider serviceProvider, IOrganizationAccountManager am, ITimeMatrixService matrixService) : base(serviceProvider)
        {
            _timeEntryService = timeEntryService;
            _chartService = chartService;
            _am = am;
            _matrixService = matrixService;
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(AccountManagerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetComposedOutput(_am, filters);

            return Ok(result);
        }

        /// <summary>
        ///     Gets time entries
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("entries")]
        [ProducesResponseType(typeof(List<AccountManagerTimeEntryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntries(
            [FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters
        )
        {
            var segments = await _timeEntryService
                .GetTimeEntries<AccountManagerTimeEntryOutput>(_am, filters);
            AddPagination(filters, segments.Total);
            return Ok(segments.Data);
        }

        /// <summary>
        /// get entry
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        [HttpGet("entries/{entryId}")]
        [ProducesResponseType(typeof(AccountManagerTimeEntryOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId)
        {
            var entry = await _timeEntryService.GetTimeEntry<AccountManagerTimeEntryOutput>(_am, entryId);
            return Ok(entry);
        }

        [HttpGet("charts")]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_am, filters, chartParams);
            return Ok(data);
        }
    }
}