// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Events;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountManager
    {
        private async Task<IdentityResult> CreateUserAccount(ApplicationUser user, string password)
        {
            Init(user, user.UserName, password, user.Email);

            var records = await Repository.InsertAsync(user, true);
            if (records > 0)
            {
                FireEvents();

                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

       

        protected void Init(ApplicationUser account, string username, string password, string email)
        {
            account.UserName = username;
            account.NormalizedUserName = username.ToLowerInvariant();
            account.Email = email;
            account.NormalizedEmail = email.ToLowerInvariant();
            account.PasswordHash = password != null
                ? PasswordHasher.HashPassword(account, password)
                : null;
            account.PasswordChanged = password != null ? DateTimeOffset.UtcNow : (DateTimeOffset?)null;
            account.EmailConfirmed = false;
            account.TwoFactorEnabled = false;
            account.SecurityStamp =   Guid.NewGuid().ToString();
            AddEvent(new AccountCreatedEvent
            {
                Account = account,
                InitialPassword = password,
            });
        }
        
        public override Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return CreateUserAccount(user, password);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return CreateAsync(user, string.Empty);
        }
        
    }
}