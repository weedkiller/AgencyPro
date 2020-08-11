// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Stories.Filters;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.Stories.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class StoryController : OrganizationUserController
    {
        private readonly IOrganizationCustomer _customer;
        private readonly IStoryService _storyService;

        public StoryController(
            IStoryService storyService,
            IOrganizationCustomer customer,
            IServiceProvider serviceProvider) : base(serviceProvider)

        {
            _customer = customer;
            _storyService = storyService;
        }

        /// <summary>
        ///     Gets stories
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CustomerStoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStories(
            [FromRoute] Guid organizationId,
            [FromQuery] StoryFilters filters
        )
        {
            var stories = await _storyService.GetStories<CustomerStoryOutput>(_customer, filters);
            AddPagination(filters, stories.Total);
            return Ok(stories.Data);
        }

      
    }
}