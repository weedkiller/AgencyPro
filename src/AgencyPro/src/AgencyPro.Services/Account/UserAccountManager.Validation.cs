// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountManager
    {
        protected void ValidateUsername(ApplicationUser account, string value)
        {
            var result = _usernameValidator.Value.Validate(this, account, value);
            if (result == null || result == ValidationResult.Success) return;
            var error = GetLogMessage(result.ErrorMessage);
            _logger.LogError(error);
            throw new ValidationException(result.ErrorMessage);
        }

        protected void ValidatePassword(ApplicationUser account, string password)
        {
            // null is allowed (e.g. for external providers)
            if (password == null) return;

            var result = _passwordValidator.Value.Validate(this, account, password);
            if (result == null || result == ValidationResult.Success) return;
            var error = GetLogMessage(result.ErrorMessage);
            _logger.LogError(error);
            throw new ValidationException(result.ErrorMessage);
        }

        protected void ValidateEmail(ApplicationUser account, string value)
        {
            var result = _emailValidator.Value.Validate(this, account, value);
            if (result == null || result == ValidationResult.Success) return;
            var error = GetLogMessage(result.ErrorMessage);
            _logger.LogError(error);
            throw new ValidationException(result.ErrorMessage);
        }

        protected void ValidatePhoneNumber(ApplicationUser account, string value)
        {
            var result = _phoneNumberValidator.Value.Validate(this, account, value);
            if (result == null || result == ValidationResult.Success) return;
            var error = GetLogMessage(result.ErrorMessage);
            _logger.LogError(error);
            throw new ValidationException(result.ErrorMessage);
        }
    }
}