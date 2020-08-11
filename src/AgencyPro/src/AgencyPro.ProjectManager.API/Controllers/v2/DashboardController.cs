// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Widgets.Services;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class DashboardController : OrganizationUserController
    {
        private readonly IWidgetManager _widgetManager;
        private readonly IOrganizationProjectManager _projectManager;

        public DashboardController(IWidgetManager widgetManager, IProjectManagerService pmService,
            IOrganizationProjectManager projectManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _projectManager = projectManager;
            _widgetManager = widgetManager;
        }

    }
}