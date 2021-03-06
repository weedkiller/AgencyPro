﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;

namespace AgencyPro.Core.Templates.Engine
{
    public class RazorMailerRazorEngine
    {
        private readonly Assembly _viewAssembly;

        public RazorMailerRazorEngine(Assembly viewAssembly)
        {
            _viewAssembly = viewAssembly;
        }

        public async Task<string> RenderAsync<TModel>(string key, TModel model)
        {
            var razorCompiledItem = new RazorCompiledItemLoader().LoadItems(_viewAssembly)
                .FirstOrDefault(item => item.Identifier == key);

            if (razorCompiledItem == null) throw new Exception("Compilation failed");

            return await GetOutput(_viewAssembly, razorCompiledItem, model);
        }

        private static async Task<string> GetOutput<TModel>(Assembly assembly, RazorCompiledItem razorCompiledItem, TModel model)
        {
            using (var output = new StringWriter())
            {
                var compiledTemplate = assembly.GetType(razorCompiledItem.Type.FullName);
                var razorPage = (RazorPage)Activator.CreateInstance(compiledTemplate);

                if (razorPage is RazorPage<TModel> page)
                {
                    AddViewData(page, model);
                }

                razorPage.ViewContext = new ViewContext
                {
                    Writer = output
                };
                
                razorPage.DiagnosticSource = new DiagnosticListener("GetOutput");
                razorPage.HtmlEncoder = HtmlEncoder.Default;
             

                await razorPage.ExecuteAsync();

                return output.ToString();
            }
        }

        private static void AddViewData<TModel>(RazorPage<TModel> razorPage, TModel model)
        {
            razorPage.ViewData = new ViewDataDictionary<TModel>(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = model,
                
            };
        }
    }
}