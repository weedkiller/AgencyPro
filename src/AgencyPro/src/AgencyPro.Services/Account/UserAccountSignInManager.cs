// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount;
using AgencyPro.Core.UserAccount.Models;
using IdentityServer4.Extensions;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountSignInManager : SignInManager<ApplicationUser>, IResourceOwnerPasswordValidator
    {
        //private UserAccountMessages? _accountStatus;

        private readonly ILogger<UserAccountSignInManager> _logger;

        private readonly IdentityOptions _options;

        protected override Task<bool> IsLockedOut(ApplicationUser user)
        {
            return UserManager.IsLockedOutAsync(user);
        }

        public override bool IsSignedIn(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            return principal?.Identities != null &&
                   principal.Identities.Any() && principal.IsAuthenticated();
        }

        public UserAccountSignInManager(
            UserAccountManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<UserAccountSignInManager> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
            
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[UserAccountSignInManager.{callerName}] - {message}";
        }


        protected PasswordVerificationResult VerifyPassword(ApplicationUser account, string password)
        {
            //_logger.LogInformation(GetLogMessage($"called for account: {account.UserId}"));

            if (string.IsNullOrWhiteSpace(password))
            {
                //_logger.LogError(GetLogMessage("called for account: failed -- no password"));
                //_accountStatus = UserAccountMessages.MissingPassword;
                throw new ArgumentNullException(nameof(password));
            }
            
            if (!account.HasPassword())
            {
                _logger.LogError(GetLogMessage("called for account: failed -- account does not have a password"));
                return PasswordVerificationResult.Failed;
            }

            var result = UserManager.PasswordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                _logger.LogTrace(GetLogMessage("success"));
                account.AccessFailedCount = 0;
            }
            else
            {
                _logger.LogError(GetLogMessage("failed -- invalid password"));
                //_accountStatus = UserAccountMessages.InvalidCredentials;
                RecordInvalidLoginAttempt(account);
                //AddEvent(new InvalidPasswordEvent { Account = account });
            }

            return result;
        }
        protected virtual void RecordInvalidLoginAttempt(ApplicationUser account)
        {
            //account. = UtcNow;
            if (account.AccessFailedCount <= 0)
            {
                account.AccessFailedCount = 1;
            }
            else
            {
                account.AccessFailedCount++;
            }
        }

        public override Task<SignInResult> CheckPasswordSignInAsync(ApplicationUser user, string password, bool lockoutOnFailure)
        {
            return base.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        protected override Task<SignInResult> PreSignInCheck(ApplicationUser user)
        {
            return base.PreSignInCheck(user);
        }
 
        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(ApplicationUser user)
        {
            return await ClaimsFactory.CreateAsync(user);
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var account = await UserManager.FindByNameAsync(context.UserName);
            if (account == null) throw new ApplicationException();

            var isValid = UserManager.PasswordHasher.VerifyHashedPassword(account, account.PasswordHash, context.Password);
            if (isValid == PasswordVerificationResult.Failed)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
            else
            {
                context.Result = new GrantValidationResult(
                    account.Id.ToString(),
                    "local"
                );
            }
        }

    }
}
