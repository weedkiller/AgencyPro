﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.ProjectManager.API.Controllers.v2
{
    public class CustomerController : OrganizationUserController
    {
        private readonly IOrganizationProjectManager _projectManager;
        private readonly IOrganizationCustomerService _customerService;

        public CustomerController(
            IOrganizationProjectManager projectManager,
            IOrganizationCustomerService customerService,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _projectManager = projectManager;
            _customerService = customerService;
        }

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProjectManagerOrganizationCustomerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerList([FromRoute] Guid organizationId)
        {
            var result = await _customerService.GetCustomers<ProjectManagerOrganizationCustomerOutput>(_projectManager);
            return Ok(result);
        }
    }
    
}