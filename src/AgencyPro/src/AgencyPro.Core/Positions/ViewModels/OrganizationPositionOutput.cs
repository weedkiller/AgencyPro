// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Levels.ViewModels;

namespace AgencyPro.Core.Positions.ViewModels
{
    public class OrganizationPositionOutput
    {
        public Guid OrganizationId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LevelOutput> Levels { get; set; }
    }
}