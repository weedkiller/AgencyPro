// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.Marketer.API.Controllers.v2
{
    public abstract class PersonApiController : OrganizationUserController
    {
        protected PersonApiController(IServiceProvider provider) : base(provider)
        {
        }
    }
}