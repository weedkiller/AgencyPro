// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Orders.ViewModels
{
    public class WorkOrderInput
    {
        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }
        public string Description { get; set; }
        public bool IsDraft { get; set; }
    }
}