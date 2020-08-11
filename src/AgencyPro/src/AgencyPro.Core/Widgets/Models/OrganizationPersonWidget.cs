// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.Widgets.Services;

namespace AgencyPro.Core.Widgets.Models
{
    public class OrganizationPersonWidget : BaseObjectState, IOrganizationPersonWidget
    {
        public Guid OrganizationId { get; set; }
        public Guid PersonId { get; set; }
        public int CategoryId { get; set; }
        public int WidgetId { get; set; }

        public virtual OrganizationPerson OrganizationPerson { get; set; }
        public virtual CategoryWidget CategoryWidget { get; set; }
    }
}