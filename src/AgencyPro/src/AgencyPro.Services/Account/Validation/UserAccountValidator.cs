// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Services.Account.Validation
{
    public class UserAccountValidator : UserValidator<ApplicationUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            IdentityResult baseResult = await base.ValidateAsync(manager, user);
            return baseResult;
        }
        
    }
}