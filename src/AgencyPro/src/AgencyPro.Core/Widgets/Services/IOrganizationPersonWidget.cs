// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Widgets.Services
{
    public interface IOrganizationPersonWidget
    {
        Guid OrganizationId { get; set; }
        Guid PersonId { get; set; }
        int CategoryId { get; set; }
        int WidgetId { get; set; }
    }
}