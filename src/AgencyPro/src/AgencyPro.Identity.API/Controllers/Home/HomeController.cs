// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AgencyPro.Identity.API.Controllers.Home
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _environment;
        private readonly ILogger _logger;

        public HomeController(
            IOptions<AppSettings> settings,
            IIdentityServerInteractionService interaction, IHostingEnvironment environment, ILogger<HomeController> logger)
        {
            _settings = settings;
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.User != null  && HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.FlowRedirectUrl = _settings.Value.Urls.FlowRedirect;
                ViewBag.Nonce = Guid.NewGuid().ToString().Substring(0, 5);
                return Redirect(_settings.Value.Urls.PortalRedirect);
            }else
            {
                return Redirect("/index.html");
            }
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }

            return View("Error", vm);
        }
    }
}