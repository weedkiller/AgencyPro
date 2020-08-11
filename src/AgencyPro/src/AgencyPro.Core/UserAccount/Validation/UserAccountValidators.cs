// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.UserAccount.Validation
{
    public class UserAccountValidators
    {
        private readonly AggregateValidator<ApplicationUser> _emailValidators = new AggregateValidator<ApplicationUser>();

        private readonly AggregateValidator<ApplicationUser> _passwordValidators = new AggregateValidator<ApplicationUser>();

        private readonly AggregateValidator<ApplicationUser> _phoneNumberValidators = new AggregateValidator<ApplicationUser>();
        private readonly AggregateValidator<ApplicationUser> _usernameValidators = new AggregateValidator<ApplicationUser>();

        public IValidator<ApplicationUser> UsernameValidator => _usernameValidators;

        public IValidator<ApplicationUser> PasswordValidator => _passwordValidators;

        public IValidator<ApplicationUser> EmailValidator => _phoneNumberValidators;

        public IValidator<ApplicationUser> PhoneNumberValidator => _phoneNumberValidators;

        public void RegisterUsernameValidator(params IValidator<ApplicationUser>[] items)
        {
            _usernameValidators.AddRange(items);
        }

        public void RegisterPasswordValidator(params IValidator<ApplicationUser>[] items)
        {
            _passwordValidators.AddRange(items);
        }

        public void RegisterEmailValidator(params IValidator<ApplicationUser>[] items)
        {
            _emailValidators.AddRange(items);
        }

        public void RegisterPhoneNumberValidator(params IValidator<ApplicationUser>[] items)
        {
            _phoneNumberValidators.AddRange(items);
        }
    }
}