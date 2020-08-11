// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.ActivityFeed.Services;
using AgencyPro.Core.ActivityFeed.ViewModels;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class ActivityFeedController : OrganizationUserController
    {
        private readonly Lazy<IProviderAgencyOwner> _providerAgencyOwner;
        private readonly IActivityFeedService _feedService;

        public ActivityFeedController(
            Lazy<IProviderAgencyOwner> providerAgencyOwner,
            IActivityFeedService feedService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _providerAgencyOwner = providerAgencyOwner;
            _feedService = feedService;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetActivityFeed([FromRoute]Guid organizationId, [FromQuery]ActivityFeedFilters filters)
        {
            
            if (filters.Type.Count == 0)
            {
                filters.Type.Add(NotificationType.Lead);
                filters.Type.Add(NotificationType.Candidate);
                filters.Type.Add(NotificationType.Contract);
                filters.Type.Add(NotificationType.Project);
                filters.Type.Add(NotificationType.Proposal);
                filters.Type.Add(NotificationType.Invoice);
                filters.Type.Add(NotificationType.TimeEntry);
                filters.Type.Add(NotificationType.Story);

            }


            var ao = _providerAgencyOwner.Value;
            var output = _feedService.GetActivityFeed(ao, filters);

            return Ok(output);
        }
    }
}