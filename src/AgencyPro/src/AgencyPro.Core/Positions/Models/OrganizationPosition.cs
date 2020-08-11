// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.Positions.Models
{
    public class OrganizationPosition : BaseObjectState
    {
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

    }
}