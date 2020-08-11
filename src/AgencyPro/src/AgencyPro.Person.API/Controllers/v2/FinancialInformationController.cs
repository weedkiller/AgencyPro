// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    public class FinancialInformationController : OrganizationUserController
    {
        public FinancialInformationController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet()]
        public IActionResult Get([FromRoute] Guid organizationId)
        {
            return Ok();
        }
    }
}
