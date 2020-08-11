// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Middleware.Features;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgencyPro.Middleware.Filters
{
    public class ModelStateFeatureFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var state = context.ModelState;
            context.HttpContext.Features.Set(new ModelStateFeature(state));
            await next();
        }
    }
}