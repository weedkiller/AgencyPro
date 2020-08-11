﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Widgets.Services;

namespace AgencyPro.Core.Widgets.ViewModels
{
    public class WidgetOutput : IWidget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AccessFlag { get; set; }
        public string Schema { get; set; }
        public string DisplayMetadata { get; set; }
    }
}
