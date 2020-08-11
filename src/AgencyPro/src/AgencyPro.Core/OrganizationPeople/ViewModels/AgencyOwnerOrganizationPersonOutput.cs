// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Metadata;

namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    [FlowDirective(FlowRoleToken.AgencyOwner, "people")]

    public class AgencyOwnerOrganizationPersonOutput
        : OrganizationPersonOutput
    {
        public override Guid TargetOrganizationId => this.OrganizationId;
    }
}