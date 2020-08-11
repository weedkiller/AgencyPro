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

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class TimeController : OrganizationUserController
    {
        private readonly ITimeEntryService _timeEntryService;
        private readonly IAgencyOwner _agencyOwner;
        private readonly ITimeMatrixService _matrixService;
        private readonly Lazy<IMarketingAgencyOwner> _marketingAgencyOwner;
        private readonly Lazy<IProviderAgencyOwner> _providerAgencyOwner;
        private readonly Lazy<IRecruitingAgencyOwner> _recruitingAgencyOwner;
        private readonly IChartService _chartService;

        public TimeController(IServiceProvider serviceProvider, 
            ITimeEntryService timeEntryService,
            IAgencyOwner agencyOwner, ITimeMatrixService matrixService,
            Lazy<IMarketingAgencyOwner> marketingAgencyOwner,
            Lazy<IProviderAgencyOwner> providerAgencyOwner,
            Lazy<IRecruitingAgencyOwner> recruitingAgencyOwner,
            IChartService chartService) : base(serviceProvider)
        {
            _timeEntryService = timeEntryService;
            _agencyOwner = agencyOwner;
            _matrixService = matrixService;
            _marketingAgencyOwner = marketingAgencyOwner;
            _providerAgencyOwner = providerAgencyOwner;
            _recruitingAgencyOwner = recruitingAgencyOwner;
            _chartService = chartService;
        }

        /// <summary>
        ///     Gets a searchable matrix of time segments
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("matrix")]
        [ProducesResponseType(typeof(ProviderAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> GetProvidingAgencyMatrix([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetProviderAgencyComposedOutput(_providerAgencyOwner.Value, filters);

            return Ok(result);
        }

        /// <summary>
        /// Gets a searchable matrix of time segments for the recruiting agency
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("recruiting-matrix")]
        [ProducesResponseType(typeof(RecruitingAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> GetRecruitingAgencyMatrix([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetRecruitingAgencyComposedOutput(_recruitingAgencyOwner.Value, filters);

            return Ok(result);
        }

        /// <summary>
        /// Gets searchable matrix of time segments for the marketing agency
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("marketing-matrix")]
        [ProducesResponseType(typeof(MarketingAgencyOwnerTimeMatrixComposedOutput), 200)]
        public async Task<IActionResult> GetMarketingAgencyMatrix([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters)
        {
            var result = await _matrixService
                .GetMarketingAgencyComposedOutput(_marketingAgencyOwner.Value, filters);

            return Ok(result);
        }


        /// <summary>
        ///     Gets time entries for an agency
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("entries")]
        [ProducesResponseType(typeof(List<ProviderAgencyOwnerTimeEntryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntries(
            [FromRoute] Guid organizationId,
            [FromQuery] TimeMatrixFilters filters
        )
        {
            var segments = await _timeEntryService
                .GetTimeEntries<ProviderAgencyOwnerTimeEntryOutput>(_agencyOwner, filters);
            AddPagination(filters, segments.Total);
            return Ok(segments.Data);
        }

        /// <summary>
        /// get entry details
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="entryId"></param>
        /// <returns></returns>
        [HttpGet("entries/{entryId}")]
        [ProducesResponseType(typeof(List<ProviderAgencyOwnerTimeEntryDetailsOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntry(
            [FromRoute] Guid organizationId,
            [FromRoute] Guid entryId
        )
        {
            var entry =
                await _timeEntryService.GetTimeEntry<ProviderAgencyOwnerTimeEntryDetailsOutput>(_agencyOwner, entryId);

            return Ok(entry);
        }

        /// <summary>
        /// gets the provider charts
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="chartParams"></param>
        /// <returns></returns>
        [HttpGet("charts")]
        public async Task<IActionResult> GetCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetProviderChartData(_providerAgencyOwner.Value, filters, chartParams);
            return Ok(data);
        }

        /// <summary>
        /// Gets the recruiting charts
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="chartParams"></param>
        /// <returns></returns>
        [HttpGet("charts-recruiting")]
        public async Task<IActionResult> GetRecruitingCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetRecruitingChartData(_recruitingAgencyOwner.Value, filters, chartParams);
            return Ok(data);
        }

        /// <summary>
        /// Get the marketing charts
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <param name="chartParams"></param>
        /// <returns></returns>
        [HttpGet("charts-marketing")]
        public async Task<IActionResult> GetMarketingCharts([FromRoute] Guid organizationId, [FromQuery] TimeMatrixFilters filters, [FromQuery] ChartParams chartParams)
        {
            var data = await _chartService.GetMarketingChartData(_marketingAgencyOwner.Value, filters, chartParams);
            return Ok(data);
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
            var entry = await _timeEntryService.DeleteTimeEntry(_agencyOwner, entryId);
            return Ok(entry);
        }

        /// <summary>
        ///     Approves Time Segment
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
            var result = await _timeEntryService
                .Approve(_agencyOwner, model);

            return Ok(result);
        }

        /// <summary>
        ///     Approves Time Entry and updates at teh same time
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
            var result = await _timeEntryService
                .SaveAndApprove(_agencyOwner, entryId, input);

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
            var result = await _timeEntryService
                .Reject(_agencyOwner, model);

            return Ok(result);
        }
    }
}