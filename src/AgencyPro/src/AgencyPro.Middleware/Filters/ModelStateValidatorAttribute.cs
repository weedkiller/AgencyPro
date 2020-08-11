// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AgencyPro.Middleware.Filters
{
    /// <summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ModelStateValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            var el = actionContext.ActionArguments.Values.ElementAtOrDefault(0);
            if (el == null)
            {
                actionContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                actionContext.Result = new JsonResult(new {Message = "Model contains no values"});
            }

            var modelState = actionContext.ModelState;
            if (modelState.IsValid) return;
            IEnumerable errors = modelState.Errors();
            actionContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            actionContext.Result = new JsonResult(errors);
        }
    }
}