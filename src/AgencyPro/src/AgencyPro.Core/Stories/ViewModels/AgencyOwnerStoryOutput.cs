// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Stories.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "stories")]
    public class AgencyOwnerStoryOutput : StoryOutput
    {
        public override Guid TargetOrganizationId => this.ProviderOrganizationId;
        public override Guid TargetPersonId => this.ProviderOrganizationOwnerId;
    }
}