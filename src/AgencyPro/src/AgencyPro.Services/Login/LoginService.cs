// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Login.Events;
using AgencyPro.Core.Login.Services;
using AgencyPro.Core.Login.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Services.Login.EventHandlers;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AgencyPro.Services.Login
{
    public class LoginService : Service<ApplicationUser>, ILoginService
    {
        private readonly IRepositoryAsync<ApplicationUser> _users;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEventService _events;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<LoginService> _logger;

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(LoginService)}.{callerName}] - {message}";
        }

        public async Task<SignInResult> Login(LoginInputModel model)
        {
            _logger.LogInformation(GetLogMessage("Login User: {0}"), model.Username);

            var user = await _userManager.FindByEmailAsync(model.Username);

            // validate username/password against in-memory store
            // var canSignIn = await _signInManager.CanSignInAsync(user);

            if (user != null && await _signInManager.CanSignInAsync(user))
            {
                var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                if (checkPassword.Succeeded)
                {
                    user.LastLogin = DateTimeOffset.UtcNow;
                    user.LastUpdated = DateTimeOffset.UtcNow;
                    
                    await _signInManager.SignInAsync(user, model.RememberLogin, "pwd");
                    await _events.RaiseAsync(new UserLoginSuccessEvent("pwd", user.UserName, user.Id.ToString(), user.UserName));

                    var records = await Repository.UpdateAsync(user, true);
                    
                    _logger.LogDebug(GetLogMessage("{0} records updated"), records);

                    RaiseEvent(new LoginEvent()
                    {
                        PersonId = user.Id
                    });

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    // issue authentication cookie with subject ID and username
                    await _contextAccessor.HttpContext.SignInAsync(user.Id.ToString(), user.UserName, props);
                    
                    return checkPassword;
                }

            }

            return SignInResult.Failed;
        }

        public LoginService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEventService events,
            IUrlHelper urlHelper,
            IHttpContextAccessor contextAccessor,
            LoginEventHandlers loginEvents,
            ILogger<LoginService> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            _contextAccessor = contextAccessor;
            _logger = logger;
            AddEventHandler(loginEvents);
        }
    }
}
