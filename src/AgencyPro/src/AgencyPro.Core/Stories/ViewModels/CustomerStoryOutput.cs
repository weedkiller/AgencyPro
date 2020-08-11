﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Stories.ViewModels
{
    [FlowDirective(FlowRoleToken.Customer, "stories")]
    public class CustomerStoryOutput : StoryOutput
    {
        public override Guid TargetOrganizationId => this.CustomerOrganizationId;
        public override Guid TargetPersonId => this.CustomerId;
    }
}