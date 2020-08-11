// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Widgets.Services;

namespace AgencyPro.Core.Widgets.ViewModels
{
    public class CategoryWidgetOutput : ICategoryWidget
    {
        public int WidgetId { get; set; }
        public int CategoryId { get; set; }
        public int Priority { get; set; }
        public bool Sticky { get; set; }
        public string CategoryConfiguration { get; set; }
        public WidgetOutput Widget { get; set; }
    }
}