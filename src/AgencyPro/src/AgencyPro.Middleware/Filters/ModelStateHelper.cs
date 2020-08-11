// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Middleware.Filters
{
    public static class ModelStateHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string[]>> Errors(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;

            var errors = modelState.ToDictionary(kvp => kvp.Key.Replace("model.", ""),
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                .Where(m => m.Value.Any());

            return errors;
        }
    }
}