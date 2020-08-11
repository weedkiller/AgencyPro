// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgencyPro.Core.Chart;
using AgencyPro.Core.Chart.Services;
using AgencyPro.Core.Chart.ViewModels;

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly IOrganizationContractor _contractor;
        private readonly ITimeEntryService _timeService;
        private readonly ITimeMatrixService _timeMatrix;
        private readonly IChartService _chartService;

        public TimeController(ITimeEntryService service, ITimeMatrixService timeMatrix,
            IOrganizationContractor contractor,
            IServiceProvider provider,
            IChartService chartService)  : base(provider)
        {
            _contractor = contractor;
            _timeService = service;
            _timeMatrix = timeMatrix;
            _chartService = chartService;

        }

        /// <summary>
        ///     Track time
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("")]
        
        [ProducesResponseType(typeof(TimeEntryResult), 200)]
        public async Task<IActionResult> TrackHours([FromRoute] Guid organizationId, [FromBody] TimeTrackingModel model
        )
        {
            var result = await _timeService.TrackTime(_contractor, model);

            return Ok(result);
        }

        /// <summary>
        ///     Tracks a full day (8 hours)
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("day")]
        
        [ProducesResponseType(typeof(TimeEntryResult), 200)]
        public async Task<IActionResult> TrackDay([FromRoute] Guid organizationId, [FromBody] DayTimeTrackingModel model
        )
        {
            var result = await _timeService.TrackDay(_contractor, model);

            return Ok(result);
        }

        /// <summary>
        ///     Tracks a half day (4 hours)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("halfday")]
        
        [ProducesResponseType(typeof(TimeEntryResult), 200)]
        public async Task<IActionResult> TrackHalfDay([FromRoute] Guid organizationId, [FromBody] DayTimeTrackingModel model
        )
        {
            var result = await _timeService.TrackHalfDay(_contractor, model);

            return Ok(result);
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(ContractorTimeMatrixComposedOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _timeMatrix.GetComposedOutput(_contractor, filters);
            return Ok(result);
        }

        /// <summary>
        /// Gets chart from contractor perspective
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="chartParams"></param>
        /// <returns></returns>
        [HttpGet("charts")]
        [ProducesResponseType(typeof(ContractorChartOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_contractor, filters, chartParams);
            return Ok(data);
        }
        /// <summary>
        ///     Gets time entries from contractor perspective
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("entries")]
        [ProducesResponseType(typeof(List<ContractorTimeEntryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntries(
            [FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters
        )
        {
            var segments = await _timeService
                .GetTimeEntries<ContractorTimeEntryOutput>(_contractor, filters);
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
        [ProducesResponseType(typeof(ContractorTimeEntryDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId)
        {
            var entry = await _timeService.GetTimeEntry<ContractorTimeEntryDetailsOutput>(_contractor, entryId);
            return Ok(entry);
        }

        /// <summary>
        /// updates a time entry
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("entries/{entryId}")]
        [ProducesResponseType(typeof(TimeEntryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId,
            [FromBody] TimeEntryInput input)
        {
            var entry = await _timeService.UpdateTimeEntry(_contractor, entryId, input);
            return Ok(entry);
        }

        /// <summary>
        /// delete a time entry
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        [HttpDelete("entries/{entryId}")]
        [ProducesResponseType(typeof(TimeEntryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId)
        {
            var entry = await _timeService.DeleteTimeEntry(_contractor, entryId);
            return Ok(entry);
        }
    }
}