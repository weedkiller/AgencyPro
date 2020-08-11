// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common;
using System;

namespace AgencyPro.Core.Orders
{
    public class WorkOrderFilters : CommonFilters
    {
        public Guid? AccountManagerOrganizationId { get; set; }
    }
}