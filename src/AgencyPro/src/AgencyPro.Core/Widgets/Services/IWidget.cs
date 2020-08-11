// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Widgets.Services
{
    public interface IWidget
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        long AccessFlag { get; set; }
        string Schema { get; set; }
        string DisplayMetadata { get; set; }
    }
}