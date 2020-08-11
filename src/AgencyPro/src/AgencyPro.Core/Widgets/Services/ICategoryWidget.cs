// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Widgets.Services
{
    public interface ICategoryWidget
    {
        int WidgetId { get; set; }
        int CategoryId { get; set; }
        int Priority { get; set; }
        bool Sticky { get; set; }
        string CategoryConfiguration { get; set; }
    }
}