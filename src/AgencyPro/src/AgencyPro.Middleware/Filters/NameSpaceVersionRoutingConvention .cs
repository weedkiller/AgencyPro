// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AgencyPro.Middleware.Filters
{
    /// <summary>
    /// </summary>
    public class NameSpaceVersionRoutingConvention : IApplicationModelConvention
    {
        private const string UrlTemplate = "{0}/{1}/{2}";

        private readonly string _apiPrefix;

        public NameSpaceVersionRoutingConvention(string apiPrefix = "api")
        {
            _apiPrefix = apiPrefix;
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var hasRouteAttribute = controller.Selectors.Any(x => x.AttributeRouteModel != null);
                if (hasRouteAttribute) continue;
                var nameSpace = controller.ControllerType.Namespace.Split('.');
                var version = nameSpace.FirstOrDefault(x => Regex.IsMatch(x, @"[v][\d*]"));
                if (string.IsNullOrEmpty(version)) continue;
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel
                {
                    Template = string.Format(UrlTemplate, _apiPrefix, version, controller.ControllerName)
                };
            }
        }
    }
}