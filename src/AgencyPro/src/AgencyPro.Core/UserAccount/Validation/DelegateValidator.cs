// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.Services;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Core.UserAccount.Validation
{
    public class DelegateValidator : IValidator<ApplicationUser>
    {
        private readonly Func<UserManager<ApplicationUser>, ApplicationUser, string, ValidationResult> func;

        public DelegateValidator(Func<UserManager<ApplicationUser>, ApplicationUser, string, ValidationResult> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            this.func = func;
        }

        public ValidationResult Validate(IService<ApplicationUser> service, ApplicationUser account, string value)
        {
            return func((UserManager<ApplicationUser>)service, account, value);
        }
    }
}