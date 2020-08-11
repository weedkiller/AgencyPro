// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.Projects.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "projects")]
    public class AgencyOwnerProjectOutput : ProjectOutput
    {
        public override Guid TargetOrganizationId => this.ProjectManagerOrganizationId;
        public override Guid TargetPersonId => this.ProjectManagerId;
    }
}