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

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly IOrganizationProjectManager _projectManager;
        private readonly ITimeMatrixService _timeMatrixService;
        private readonly ITimeEntryService _timeService;
        private readonly IChartService _chartService;

        public TimeController(ITimeEntryService service,
            IOrganizationProjectManager projectManager,
            ITimeMatrixService timeMatrixService,
            IChartService chartService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _projectManager = projectManager;
            _timeMatrixService = timeMatrixService;
            _timeService = service;
            _chartService = chartService;
        }

        /// <summary>
        ///     Gets time entries for contractor
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("entries")]
        [ProducesResponseType(typeof(List<ProjectManagerTimeEntryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntries(
            [FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters
        )
        {
            var segments = await _timeService
                .GetTimeEntries<ProjectManagerTimeEntryOutput>(_projectManager, filters);
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
        [ProducesResponseType(typeof(ProjectManagerTimeEntryDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId)
        {
            var entry = await _timeService.GetTimeEntry<ProjectManagerTimeEntryDetailsOutput>(_projectManager, entryId);
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
            var entry = await _timeService
                .UpdateTimeEntry(_projectManager, entryId, input);

            return Ok(entry);
        }

        /// <summary>
        ///     Approves Time Entry(ies)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("approve")]
        [ProducesResponseType(typeof(TimeEntryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Approve(
            [FromRoute] Guid organizationId,
            [FromBody] Guid[] model
        )
        {
            var result = await _timeService
                .Approve(_projectManager, model);

            return Ok(result);
        }

        /// <summary>
        ///     Approves Time Entry and updates at the same time
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPatch("{entryId}/approve")]
        [ProducesResponseType(typeof(TimeEntryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveAndApprove(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid entryId,
            [FromBody] TimeEntryInput input
        )
        {
            var result = await _timeService
                .SaveAndApprove(_projectManager, entryId, input);

            return Ok(result);
        }

        /// <summary>
        ///     Rejects Time Entry(ies)
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("reject")]
        [ProducesResponseType(typeof(TimeEntryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Reject(
            [FromRoute] Guid organizationId,
            [FromBody] Guid[] model
        )
        {
            var result = await _timeService
                .Reject(_projectManager, model);

            return Ok(result);
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(ProjectManagerTimeMatrixComposedOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {

            var result = await _timeMatrixService
                .GetComposedOutput(_projectManager, filters);

            return Ok(result);
        }

        [HttpGet("charts")]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_projectManager, filters, chartParams);
            return Ok(data);
        }
        /// <summary>
        /// delete a time entry
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        [HttpDelete("entries/{entryId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEntry([FromRoute] Guid organizationId, [FromRoute] Guid entryId)
        {
            var entry = await _timeService.DeleteTimeEntry(_projectManager, entryId);
            return Ok(entry);
        }
    }
}