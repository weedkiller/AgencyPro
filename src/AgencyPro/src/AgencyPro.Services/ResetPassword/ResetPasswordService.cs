// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AgencyPro.Core.ResetPassword.Events;
using AgencyPro.Core.ResetPassword.Services;
using AgencyPro.Core.ResetPassword.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Services.ForgotPassword;
using AgencyPro.Services.ResetPassword.EventHandlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.ResetPassword
{
    public class ResetPasswordService : Service<ApplicationUser>, IResetPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordService> _logger;

        public ResetPasswordService(
            UserManager<ApplicationUser> userManager,
            ILogger<ForgotPasswordService> logger,
            ResetPasswordEventHandlers resetPasswordEvents,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _userManager = userManager;
            _logger = logger;

            AddEventHandler(resetPasswordEvents);
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ForgotPasswordService)}.{callerName}] - {message}";
        }
        
        public async Task<IdentityResult> ResetPassword(ResetPasswordInputModel model)
        {
            _logger.LogInformation(GetLogMessage("Email: {0}; Code: {1}"), model.Email, model.Code);

            var user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                await Task.Run(() =>
                {
                    RaiseEvent(new ResetPasswordEvent()
                    {
                        PersonId = user.Id
                    });
                });
            }

            return result;
        }
    }
}
