// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;
using Newtonsoft.Json;

namespace AgencyPro.Core.Stories.ViewModels
{
    [FlowDirective(FlowRoleToken.AccountManager, "stories")]
    public class AccountManagerStoryOutput : StoryOutput
    {

        public override Guid ProjectId { get; set; }
        [JsonIgnore] public override DateTimeOffset? AssignedDateTime { get; set; }
        [JsonIgnore] public override DateTimeOffset? ProjectManagerAcceptanceDate { get; set; }
        [JsonIgnore] public override DateTimeOffset? CustomerAcceptanceDate { get; set; }
        public override Guid TargetOrganizationId => this.ProviderOrganizationId;
        public override Guid TargetPersonId => this.AccountManagerId;
    }
}