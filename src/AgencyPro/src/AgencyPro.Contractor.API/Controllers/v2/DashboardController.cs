// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Widgets.Services;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.Contractor.API.Controllers.v2
{
    public class DashboardController : OrganizationUserController
    {
        private readonly IOrganizationContractor _contractor;
        private readonly IWidgetManager _widgetManager;

        public DashboardController(IWidgetManager widgetManager,
            IOrganizationContractor contractor,
            IServiceProvider provider) : base(provider)
        {
            _widgetManager = widgetManager;
            _contractor = contractor;
        }

    }
}