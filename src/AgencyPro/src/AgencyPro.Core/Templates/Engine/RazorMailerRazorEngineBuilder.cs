// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AgencyPro.Core.Templates.Engine
{
    public class RazorMailerRazorEngineBuilder
    {
        private Assembly _viewAssembly;

        public RazorMailerRazorEngineBuilder UseEmbeddedResourcesProject(Assembly viewAssembly)
        {
            var relatedAssembly = RelatedAssemblyAttribute.GetRelatedAssemblies(viewAssembly, false).SingleOrDefault();

            if (relatedAssembly == null)
            {
                _viewAssembly = viewAssembly;

                return this;
            }

            _viewAssembly = relatedAssembly;

            return this;
        }

        public RazorMailerRazorEngine Build()
        {
            return new RazorMailerRazorEngine(_viewAssembly);
        }
    }
}