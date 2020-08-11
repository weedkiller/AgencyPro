// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Positions.Models;

namespace AgencyPro.Core.Levels
{
    public class Level : BaseObjectState
    {
        public int Id { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public string Name { get; set; }

        public ICollection<OrganizationContractor> Contractors { get; set; }

    }
}
