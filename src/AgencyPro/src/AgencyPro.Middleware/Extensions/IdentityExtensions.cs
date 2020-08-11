// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using IdentityModel;

namespace AgencyPro.Middleware.Extensions
{
    public static class IdentityExtensions
    {
        [DebuggerStepThrough]
        public static Guid GetUserId(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity) identity;
            return new Guid(claimsIdentity
                .Claims
                .First(x => x.Type == JwtClaimTypes.Subject).Value);
        }
    }
}