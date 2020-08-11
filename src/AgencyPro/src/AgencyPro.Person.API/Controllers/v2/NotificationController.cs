// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Notifications;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    public class NotificationController : OrganizationUserController
    {

        public NotificationController(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        /// <summary>
        /// Get notifications
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        public IActionResult Get([FromRoute] Guid organizationId, [FromQuery] NotificationFilters filters)
        {
            return Ok();
        }
    }
}