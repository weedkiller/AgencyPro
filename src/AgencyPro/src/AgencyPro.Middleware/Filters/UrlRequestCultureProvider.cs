﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace AgencyPro.Middleware.Filters
{
    public class UrlRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var url = httpContext.Request.Path;

            //Quick and dirty parsing of language from url path, which looks like /api/es-ES/hello-world
            var parts = url.Value.Split('/').Where(p => !string.IsNullOrWhiteSpace(p)).ToList();

            //if (parts.Count == 0)
            //{
            //    return Task.FromResult<ProviderCultureResult>(null);
            //}

            var culture = parts.FirstOrDefault(p => Regex.IsMatch(p, @"^[a-z]{2}(?:-[A-Z]{2})?$"));
            //var cultureSegmentIndex = parts.Contains("api") ? 1 : 0;
            //var hasCulture = Regex.IsMatch(parts[cultureSegmentIndex], @"^[a-z]{2}(?:-[A-Z]{2})?$");
            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}