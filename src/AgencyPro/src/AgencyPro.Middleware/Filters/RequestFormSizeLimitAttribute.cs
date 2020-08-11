// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;

//http://stackoverflow.com/questions/38357108/form-submit-resulting-in-invaliddataexception-form-value-count-limit-1024-exce
namespace AgencyPro.Middleware.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestFormSizeLimitAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly FormOptions _formOptions;

        public RequestFormSizeLimitAttribute(int valueCountLimit)
        {
            _formOptions = new FormOptions
            {
                ValueCountLimit = valueCountLimit
            };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var features = context.HttpContext.Features;
            var formFeature = features.Get<IFormFeature>();

            if (formFeature?.Form == null)
                features.Set<IFormFeature>(new FormFeature(context.HttpContext.Request, _formOptions));
        }
    }
}