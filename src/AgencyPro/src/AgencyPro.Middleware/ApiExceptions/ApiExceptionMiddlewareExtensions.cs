// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Builder;

namespace AgencyPro.Middleware.ApiExceptions
{
    public static class ApiExceptionMiddlewareExtensions
    {        
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
            Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);

            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}
