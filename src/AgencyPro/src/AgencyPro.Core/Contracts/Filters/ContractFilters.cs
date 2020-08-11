// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Common;
using AgencyPro.Core.Contracts.Enums;

namespace AgencyPro.Core.Contracts.Filters
{
    public class ContractFilters : CommonFilters
    {
        public static readonly ContractFilters NoFilter = new ContractFilters();

        public ContractFilters()
        {
            Statuses = new List<ContractStatus>();
        }

        public Guid? CustomerId { get; set; } // filtered
        public Guid? CustomerOrganizationId { get; set; } // filtered
        public Guid? AccountManagerId { get; set; } // filtered
        public Guid? AccountManagerOrganizationId { get; set; } // filtered
        public Guid? ProjectManagerId { get; set; } // filtered
        public Guid? ProjectManagerOrganizationId { get; set; } // filtered
        public Guid? ContractorId { get; set; } // filtered
        public Guid? ContractorOrganizationId { get; set; } // filtered
        public Guid? ProjectId { get; set; } // filtered
        public List<ContractStatus> Statuses { get; set; }
    }
}