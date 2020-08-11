// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using AgencyPro.Core.ForgotPassword.Services;
using AgencyPro.Core.Geo.Services;
using AgencyPro.Core.Login.Services;
using AgencyPro.Core.Registration.Services;
using AgencyPro.Core.Registration.ViewModels;
using AgencyPro.Core.ResetPassword.Services;
using AgencyPro.Core.ResetPassword.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AgencyPro.Identity.API.Controllers.Account
{
    /// <summary>
    /// This sample controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    [AllowAnonymous]
    public partial class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly IRegistrationService _registrationService;

        private readonly IForgotPasswordService _forgotPasswordService;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly ILoginService _loginService;
        private readonly IGeoService _geoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<AppSettings> _settings;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events, 
            IRegistrationService registrationService,
            IForgotPasswordService forgotPasswordService,
            IResetPasswordService resetPasswordService,
            ILoginService loginService,
            IGeoService geoService,
            IOptions<AppSettings> settings)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)

            _interaction = interaction;
            _clientStore = clientStore;
            _signInManager = signInManager;
            _userManager = userManager;
            _schemeProvider = schemeProvider;
            _settings = settings;
            _events = events;
            _registrationService = registrationService;
            _forgotPasswordService = forgotPasswordService;
            _resetPasswordService = resetPasswordService;
            _loginService = loginService;
            _geoService = geoService;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            var model = new ResetPasswordViewModel() { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                var result = await _resetPasswordService.ResetPassword(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}