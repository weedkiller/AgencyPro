// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Identity.API.Controllers.Webhooks
{
    public partial class WebhooksController
    {
        [HttpPost("slack")]
        public async Task<IActionResult> Slack()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            _logger.LogDebug(GetLogMessage(json));

            return NoContent();
        }
    }
}