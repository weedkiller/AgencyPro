// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Common;
using AgencyPro.Middleware.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AgencyPro.Middleware.Controllers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("{version}/{organizationId}/[controller]")]
    [Produces("application/json")]
    public abstract class OrganizationUserController : ControllerBase
    {
        protected OrganizationUserController(IServiceProvider serviceProvider)
        {
            HttpContextAccessor = (IHttpContextAccessor) serviceProvider.GetRequiredService(typeof(IHttpContextAccessor));
        }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        protected void AddHeader(string key, object value)
        {
            HttpContext.Response.Headers.Add(key, value.ToString());
        }

        protected void AddPagination(CommonFilters commonFilters, int totalRecords)
        {
            HttpContext.Response.AddPagination(commonFilters.Page ?? 1,
                commonFilters.PageSize ?? 0,
                totalRecords,
                (int) Math.Ceiling((double) totalRecords / (commonFilters.PageSize ?? 1)));
        }
    }
}