// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Identity;

namespace AgencyPro.Services.Account
{
    public partial class UserAccountManager
    {
        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}