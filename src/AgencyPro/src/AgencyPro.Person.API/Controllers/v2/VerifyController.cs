// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("verification")]
    [Produces("application/json")]
    public class VerifyController : ControllerBase
    {
        public VerifyController()
        {

        }
    }
}
