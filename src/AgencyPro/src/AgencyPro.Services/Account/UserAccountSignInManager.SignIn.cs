// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountSignInManager
    {
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var account = await UserManager.FindByEmailAsync(userName);

            return await PasswordSignInAsync(account, password, isPersistent, lockoutOnFailure);
        }

        public override async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent,
            bool lockoutOnFailure)
        {
            var retVal = new SignInResult();

            var verification = VerifyPassword(user, password);
            if (verification == PasswordVerificationResult.Failed)
            {
                retVal = SignInResult.NotAllowed;
            }

            if (verification == PasswordVerificationResult.Success)
            {
                retVal = await base.SignInOrTwoFactorAsync(user, isPersistent);
            }

            return retVal;
        }

        protected override Task<SignInResult> SignInOrTwoFactorAsync(ApplicationUser user, bool isPersistent, string loginProvider = null,
            bool bypassTwoFactor = false)
        {
            return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
        }

        public override Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            return base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public override async Task SignInAsync(ApplicationUser user, AuthenticationProperties authenticationProperties,
            string authenticationMethod = null)
        {
            var userPrincipal = await CreateUserPrincipalAsync(user);
            // Review: should we guard against CreateUserPrincipal returning null?
            if (authenticationMethod != null)
            {
                userPrincipal.Identities.First()
                    .AddClaim(new Claim(ClaimTypes.AuthenticationMethod, authenticationMethod));
            }

            await Context.SignInAsync(IdentityConstants.ApplicationScheme,
                userPrincipal,
                authenticationProperties ?? new AuthenticationProperties());
        }
    }
    
}