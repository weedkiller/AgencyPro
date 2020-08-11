// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.ForgotPassword.Events;
using AgencyPro.Core.ForgotPassword.Services;
using AgencyPro.Core.ForgotPassword.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Services.ForgotPassword.EventHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.ForgotPassword
{


    public class ForgotPasswordService : Service<ApplicationUser>, IForgotPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordService> _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpContextAccessor _contextAccessor;

        public ForgotPasswordService(
            UserManager<ApplicationUser> userManager,
            ILogger<ForgotPasswordService> logger,
            IUrlHelper urlHelper,
            IHttpContextAccessor contextAccessor,
            ForgotPasswordEventHandlers events,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _logger = logger;
            _urlHelper = urlHelper;

            AddEventHandler(events);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ForgotPasswordService)}.{callerName}] - {message}";
        }

        public async Task<ForgotPasswordResult> ForgotPassword(ForgotPasswordInputModel model)
        {
            _logger.LogInformation(GetLogMessage("Forgot Password:{0};"), model.Email);

            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = new ForgotPasswordResult();
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = _urlHelper.Action(
                "ResetPassword",
                "Account",
                values: new { code = code },
                protocol: _contextAccessor.HttpContext.Request.Scheme);

            if (code!= null)
            {
                result.Succeeded = true;

                await Task.Run(() =>
                {
                    RaiseEvent(new ForgotPasswordEvent()
                    {
                        PersonId = user.Id,
                        CallbackUrl = callbackUrl
                    });
                });
            }

            return result;
        }
    }
}
