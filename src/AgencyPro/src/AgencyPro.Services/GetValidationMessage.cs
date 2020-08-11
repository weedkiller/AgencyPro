// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.EventHandling;

namespace AgencyPro.Services
{
    public class GetValidationMessage : ICommand
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}