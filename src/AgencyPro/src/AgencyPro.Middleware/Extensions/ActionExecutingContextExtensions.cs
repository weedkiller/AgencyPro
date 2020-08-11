// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgencyPro.Middleware.Extensions
{
    public static class ActionExecutingContextExtensions
    {
        [DebuggerStepThrough]
        public static Guid GetOrganizationId(this ActionExecutingContext context)
        {
            return (Guid) context.ActionArguments["organizationId"];
        }
    }
}