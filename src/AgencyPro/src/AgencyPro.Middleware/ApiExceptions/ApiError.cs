// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace AgencyPro.Middleware.ApiExceptions
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ApiError
    {
        public ApiError() : this ( "Application Error")
        {
           
        }

        public ApiError(string message)
        {
            Message = Message;
        }

        public ApiError(ModelStateDictionary modelState, string message) : this(message)
        {
        }

        public string Id { get; set; }
        
        [JsonIgnore]
        public int Status { get; set; }

        [JsonIgnore]
        public string Code { get; set; }
       
        public string SupportLink { get; set; }

        public string Message { get; set; }
        
        public string Detail { get; set; }
    }
}
