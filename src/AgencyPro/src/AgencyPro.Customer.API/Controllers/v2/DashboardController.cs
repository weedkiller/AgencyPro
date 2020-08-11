// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Core.Widgets.Services;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.Customer.API.Controllers.v2
{
    public class DashboardController : OrganizationUserController
    {
        private readonly OrganizationCustomerOutput _customer;
        private readonly IWidgetManager _widgetManager;

        public DashboardController(IWidgetManager widgetManager,
            OrganizationCustomerOutput customer,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customer = customer;
            _widgetManager = widgetManager;
        }


    }
}