// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Levels;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;

namespace AgencyPro.Core.Positions.Models
{
    public class Position : BaseObjectState
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Level> Levels { get; set; }

        public ICollection<CategoryPosition> Categories { get; set; }
        public ICollection<OrganizationContractor> Contractors { get; set; }
        public ICollection<OrganizationPosition> Organizations { get; set; }
    }


}
