// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Templates.Engine;
using AgencyPro.Core.Templates.Services;
//using AgencyPro.Mailer;

namespace AgencyPro.Middleware
{
    public class TemplateParser : ITemplateParser
    {
        public async Task<string> RenderAsync(string name)
        {
            return await RenderAsync<object>(name, null);
        }

        public async Task<string> RenderAsync<TModel>(string name, TModel model)
        {
            //var engine = new RazorMailerRazorEngineBuilder()
            //    .UseEmbeddedResourcesProject(typeof(IAmARazorAssembly).Assembly)
            //    .Build();

            //return await engine.RenderAsync(name, model);
            throw new NotImplementedException();
        }
    }
}