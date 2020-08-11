// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Middleware.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AgencyPro.Middleware.ApiExceptions
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next,
            ILogger<ApiExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _options = options;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(ApiExceptionMiddleware)}.{callerName}] - {message}";
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogInformation(GetLogMessage("Handling exception {Exception}"), exception.Message);

            var d = context.Features.Get<ModelStateFeature>()?.ModelState;

            var error = new ApiError(d, exception.Message)
            {
                Id = Guid.NewGuid().ToString()
            };

            _options.AddResponseDetails?.Invoke(context, exception, error);

            var innerExMessage = GetInnermostExceptionMessage(exception);

            var level = _options.DetermineLogLevel?.Invoke(exception) ?? LogLevel.Error;
            _logger.Log(level, exception, GetLogMessage(innerExMessage + " -- {ErrorId}."), error.Id);

            var result = JsonConvert.SerializeObject(error, new JsonSerializerSettings()
            {
                DateParseHandling = DateParseHandling.DateTimeOffset,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.Status;

            return context.Response.WriteAsync(result);
        }

        private string GetInnermostExceptionMessage(Exception exception)
        {
            if (exception.InnerException != null)
                return GetInnermostExceptionMessage(exception.InnerException);

            return exception.Message;
        }


        //Mappings = new Dictionary<Type, HttpStatusCode>
        //{
        //    {typeof(ArgumentNullException), HttpStatusCode.BadRequest
        //    },
        //    {typeof(ArgumentException), HttpStatusCode.BadRequest
        //    },
        //    {typeof(IndexOutOfRangeException), HttpStatusCode.BadRequest},
        //    {typeof(DivideByZeroException), HttpStatusCode.BadRequest},
        //    {typeof(InvalidOperationException), HttpStatusCode.BadRequest},
        //    {typeof(Core.Exceptions.InvalidOperationException), HttpStatusCode.BadRequest},
        //    {typeof(ValidationException), HttpStatusCode.BadRequest},
        //    {typeof(DbUpdateException), HttpStatusCode.InternalServerError},
        //    {typeof(ForbiddenException), HttpStatusCode.Forbidden},
        //    {typeof(NotFoundException), HttpStatusCode.NotFound},
        //    {typeof(SecurityTokenExpiredException), HttpStatusCode.Unauthorized},
        //    {typeof(NotImplementedException), HttpStatusCode.BadRequest}
        //};
    }
}
