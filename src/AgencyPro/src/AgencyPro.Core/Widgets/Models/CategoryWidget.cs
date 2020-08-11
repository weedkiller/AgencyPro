// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Widgets.Services;

namespace AgencyPro.Core.Widgets.Models
{
    public class CategoryWidget : BaseObjectState, ICategoryWidget
    {
        public int WidgetId { get; set; }
        public int CategoryId { get; set; }
        public int Priority { get; set; }
        public bool Sticky { get; set; }

        public Widget Widget { get; set; }
        public Category Category { get; set; }

        public string CategoryConfiguration { get; set; }

        public ICollection<OrganizationPersonWidget> OrganizationPersonWidgets { get; set; }
    }
}