// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Middleware.ApiExceptions
{
    public class ValidationResultModel
    {
        public static string ValidationFailed = "Validation Failed";

        /// <summary>
        ///     For Json Serialization
        /// </summary>
        public ValidationResultModel()
        {
        }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = ValidationFailed;
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public string Message { get; set; }

        public List<ValidationError> Errors { get; set; }
    }
}