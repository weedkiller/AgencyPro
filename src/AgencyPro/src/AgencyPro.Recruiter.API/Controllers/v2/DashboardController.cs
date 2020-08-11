// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Widgets.Services;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.Recruiter.API.Controllers.v2
{
    public class DashboardController : OrganizationUserController
    {
        private readonly IWidgetManager _widgetManager;
        private readonly IOrganizationRecruiter _recruiter;

        public DashboardController(IWidgetManager widgetManager,
            IOrganizationRecruiter recruiter,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _recruiter = recruiter;
            _widgetManager = widgetManager;
        }
        
    }
}