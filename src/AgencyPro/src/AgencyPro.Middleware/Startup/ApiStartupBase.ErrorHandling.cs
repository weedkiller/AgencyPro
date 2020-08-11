// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using AgencyPro.Core.Exceptions;
using AgencyPro.Middleware.ApiExceptions;
using AgencyPro.Middleware.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Stripe;

namespace AgencyPro.Middleware.Startup
{
    public abstract partial class ApiStartupBase
    {
        public void ConfigureErrorHandlingServices(IServiceCollection services)
        {
            Log.Information(GetLogMessage("Configuring Error Handling Services"));
            services.AddScoped<ValidateMimeMultipartContentFilter>();
        }

        public void ConfigureErrorHandling(IApplicationBuilder builder)
        {
            Log.Information(GetLogMessage("Configuring Error Handling"));
            builder.UseApiExceptionHandler(d =>
            {
                d.AddResponseDetails = UpdateApiErrorResponse;
                d.DetermineLogLevel = DetermineLogLevel;
            });
        }



        private static LogLevel DetermineLogLevel(Exception ex)
        {
            if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
            {
                return LogLevel.Critical;
            }
            return LogLevel.Error;
        }

        private static void UpdateApiErrorResponse(HttpContext context, Exception ex, ApiError error)
        {
            error.Status = StatusCodes.Status500InternalServerError;
            error.Message = ex.Message;

            if (ex.GetType().Name == typeof(SqlException).Name) error.Status = StatusCodes.Status502BadGateway;
            if (ex.GetType().Name == typeof(ArgumentNullException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(ArgumentException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(DivideByZeroException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(InvalidOperationException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(ValidationException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(DbUpdateException).Name) error.Status = StatusCodes.Status500InternalServerError;
            if (ex.GetType().Name == typeof(ForbiddenException).Name) error.Status = StatusCodes.Status403Forbidden;
            if (ex.GetType().Name == typeof(NotFoundException).Name) error.Status = StatusCodes.Status404NotFound;
            if (ex.GetType().Name == typeof(SecurityTokenExpiredException).Name) error.Status = StatusCodes.Status401Unauthorized;
            if (ex.GetType().Name == typeof(NotImplementedException).Name) error.Status = StatusCodes.Status400BadRequest;
            if (ex.GetType().Name == typeof(StripeException).Name)
            {
                error.Status = StatusCodes.Status502BadGateway;
                error.Message = ex.Message;
            }

            if (ex.GetType().Name == typeof(ApplicationException).Name)
            {
                error.Message = ex.Message;
                error.Status = StatusCodes.Status400BadRequest;
            }

            error.SupportLink = "support@agencypro.com";
        }
    }
}