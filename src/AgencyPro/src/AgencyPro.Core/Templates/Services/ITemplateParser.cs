// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;

namespace AgencyPro.Core.Templates.Services
{
    public interface ITemplateParser
    {
        Task<string> RenderAsync(string name);
        Task<string> RenderAsync<TModel>(string name, TModel model);
    }
}