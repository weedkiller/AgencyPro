// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Positions.Models;

namespace AgencyPro.Core.Positions
{
    public class CategoryPosition : BaseObjectState
    {
        public Category Category { get; set; }
        public Position Position { get; set; }

        public int CategoryId { get; set; }
        public int PositionId { get; set; }
    }
}