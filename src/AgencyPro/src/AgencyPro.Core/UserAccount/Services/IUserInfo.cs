// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.UserAccount.ViewModels;

namespace AgencyPro.Core.UserAccount.Services
{
    public interface IUserInfo
    {
        IEnumerable<string> Roles { get; }
        Guid UserId { get; }
        string Username { get; }
        string Email { get; }
        bool IsAuthenticated { get; }
        Guid Guid { get; }
        DateTime? AuthenticationInstant { get; }
        bool IsAdmin { get; }
        IEnumerable<UserClaimOutput> Claims { get; }
        bool HasClaim(string type);
        bool HasClaimValue(string type, string value);
        bool HasClaimValue(string type, List<string> values);
        void ValidateClaim(string type, string[] resources);
    }
}