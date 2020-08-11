// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Middleware.Features
{
    public class ModelStateFeature
    {
        public ModelStateDictionary ModelState { get; set; }

        public ModelStateFeature(ModelStateDictionary state)
        {
            this.ModelState = state;
        }
    }
}