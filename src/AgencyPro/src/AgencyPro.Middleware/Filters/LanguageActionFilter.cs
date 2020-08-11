// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Middleware.Filters
{
    /// <summary>
    /// </summary>
    public class LanguageActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LanguageActionFilter()
        {
        }

        public LanguageActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LanguageActionFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cult = context.RouteData.Values["culture"];
            _logger?.LogInformation($"Setting the culture from the URL: {cult}");

            if (cult == null) return;

            var culture = cult.ToString();
#if NET451
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
#elif NET46
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
#else
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);
#endif
            base.OnActionExecuting(context);
        }
    }
}