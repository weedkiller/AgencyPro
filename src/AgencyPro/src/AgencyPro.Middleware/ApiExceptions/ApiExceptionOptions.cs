// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Middleware.ApiExceptions
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }  
        public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
    }
}
