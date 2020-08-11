// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.ViewModels
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public abstract class BaseResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
