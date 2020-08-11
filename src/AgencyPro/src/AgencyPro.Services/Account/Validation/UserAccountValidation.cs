﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AgencyPro.Core.UserAccount;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Core.UserAccount.Validation;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Services.Account.Validation
{
    internal class UserAccountValidation
    {
        public static readonly IValidator<ApplicationUser> UsernameDoesNotContainAtSign =
            new DelegateValidator((service, account, value) =>
                !value.Contains("@") ? null : new ValidationResult("UsernameCannotContainAtSign"));

        private static readonly char[] SpecialChars = { '.', ' ', '_', '-', '\'' };

        public static readonly IValidator<ApplicationUser> UsernameOnlySingleInstanceOfSpecialCharacters =
            new DelegateValidator((service, account, value) =>
            {
                foreach (var specialChar in SpecialChars)
                {
                    var doubleChar = specialChar.ToString() + specialChar.ToString();
                    if (value.Contains(doubleChar))
                        return new ValidationResult("UsernameCannotRepeatSpecialCharacters");
                }

                return null;
            });

        public static readonly IValidator<ApplicationUser> UsernameOnlyContainsValidCharacters =
            new DelegateValidator((service, account, value) =>
            {
                if (!value.All(IsValidUsernameChar)) return new ValidationResult("UsernameOnlyContainsValidCharacters");
                return null;
            });

        public static readonly IValidator<ApplicationUser> UsernameCanOnlyStartOrEndWithLetterOrDigit =
            new DelegateValidator((service, account, value) =>
            {
                if (!char.IsLetterOrDigit(value.First()) || !char.IsLetterOrDigit(value.Last()))
                    return new ValidationResult("UsernameCanOnlyStartOrEndWithLetterOrDigit");
                return null;
            });

        public static readonly IValidator<ApplicationUser> UsernameMustNotAlreadyExist =
            new DelegateValidator((service, account, value) =>
            {
                //if (service.UsernameExists(value)) return new ValidationResult("UsernameAlreadyInUse");
                return null;
            });

        public static readonly IValidator<ApplicationUser> EmailRequired =
            new DelegateValidator((service, account, value) =>
            {
                //if (service.Settings.RequireAccountVerification &&
                //    string.IsNullOrWhiteSpace(value))
                //    return new ValidationResult("EmailRequired");
                return null;
            });

        public static readonly IValidator<ApplicationUser> EmailIsValidFormat =
            new DelegateValidator((service, account, value) =>
            {
                if (string.IsNullOrWhiteSpace(value)) return null;
                var validator = new EmailAddressAttribute();
                return !validator.IsValid(value) ? new ValidationResult("InvalidEmail") : null;
            });

        public static readonly IValidator<ApplicationUser> EmailIsRequiredIfRequireAccountVerificationEnabled =
            new DelegateValidator((service, account, value) =>
            {
                //if (service.Settings.RequireAccountVerification && string.IsNullOrWhiteSpace(value))
                //    return new ValidationResult("EmailRequired");
                return null;
            });

        public static readonly IValidator<ApplicationUser> EmailMustNotAlreadyExist =
            new DelegateValidator((service, account, value) =>
            {
                var acct = service.FindByEmailAsync(value);
                if (!string.IsNullOrWhiteSpace(value) && acct != null)
                    return new ValidationResult("EmailAlreadyInUse");
                return null;
            });

        public static readonly IValidator<ApplicationUser> PhoneNumberRequired =
            new DelegateValidator((service, account, value) =>
            {
                //if (!service.Settings.RequireAccountVerification || !string.IsNullOrWhiteSpace(value)) return null;
                //_logger.LogTrace("[UserAccountValidation.PhoneNumberRequired] validation failed: {0}", account.Username);

                return new ValidationResult("MobilePhoneRequired");
            });

        public static readonly IValidator<ApplicationUser> PhoneNumberIsRequiredIfRequireAccountVerificationEnabled =
            new DelegateValidator((service, account, value) =>
            {
                //if (service.Settings.RequireAccountVerification && string.IsNullOrWhiteSpace(value))
                //    return new ValidationResult("MobilePhoneMustBeDifferent");
                return null;
            });

        public static readonly IValidator<ApplicationUser> PhoneNumberMustNotAlreadyExist =
            new DelegateValidator((service, account, value) =>
            {
                //if (!string.IsNullOrWhiteSpace(value) && service.PhoneNumberExistsOtherThan(account, value))
                //    return new ValidationResult("MobilePhoneAlreadyInUse");
                return null;
            });

        public static readonly IValidator<ApplicationUser> PasswordMustBeDifferentThanCurrent =
            new DelegateValidator((service, account, value) =>
            {
                // Use LastLogin null-check to see if it's a new account
                // we don't want to run this logic if it's a new account
                if (account.IsNew()) return null;
                //_logger.LogTrace("[UserAccountValidation.PasswordMustBeDifferentThanCurrent] validation failed: {0}", account.Username);
                return (service.PasswordHasher.VerifyHashedPassword(account, account.PasswordHash, value) == PasswordVerificationResult.Success
                    ? new ValidationResult("New password must be different than current password")
                    : null);
            });

        public static bool IsValidUsernameChar(char c)
        {
            return
                char.IsLetterOrDigit(c) ||
                SpecialChars.Contains(c);
        }
    }
}