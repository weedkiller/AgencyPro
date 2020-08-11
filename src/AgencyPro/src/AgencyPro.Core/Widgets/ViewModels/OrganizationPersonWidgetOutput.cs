// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Widgets.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.Widgets.ViewModels
{
    public class OrganizationPersonWidgetOutput : IOrganizationPersonWidget
    {
        [JsonIgnore]
        public Guid OrganizationId { get; set; }

        [JsonIgnore]
        public Guid PersonId { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public int WidgetId { get; set; }

        public CategoryWidgetOutput CategoryWidget { get; set; }
    }
}