// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Middleware.Controllers;

namespace AgencyPro.AgencyOwner.API.Controllers.v2
{
    public class BonusController : OrganizationUserController
    {
        public BonusController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
