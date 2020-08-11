// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Reflection;
using AgencyPro.Middleware.Providers;
using Microsoft.Extensions.Configuration;

namespace AgencyPro.Middleware.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEmbeddedJsonFile(this IConfigurationBuilder configurationBuilder,
            Assembly assembly, string name, bool optional = false, bool reloadOnChange = false)
        {
            // reload on change is not supported, always pass in false
            return configurationBuilder.AddJsonFile(new EmbeddedFileProvider(assembly), name, optional, reloadOnChange);
        }
    }
}