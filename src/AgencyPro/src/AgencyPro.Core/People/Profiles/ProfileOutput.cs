// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.People.ViewModels;

namespace AgencyPro.Core.People.Profiles
{
    public class ProfileOutput : PersonOutput
    {
        public override Guid Id { get; set; }
        public override string ImageUrl { get; set; }
    }
}