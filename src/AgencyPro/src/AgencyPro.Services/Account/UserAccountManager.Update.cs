// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountManager
    {
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            try
            {
                user.Email = user.Email;
                user.ObjectState = ObjectState.Modified;
                user.LastUpdated = DateTimeOffset.UtcNow;

                var output = Repository.InsertOrUpdateGraph(user, true);
                if (output > 0)
                {
                    return await Task.FromResult(IdentityResult.Success);
                }
                else
                {
                    return await Task.FromResult(IdentityResult.Failed());
                }

            }
            catch (Exception)
            {
                return await Task.FromResult(IdentityResult.Failed());
            }
        }
    }
}