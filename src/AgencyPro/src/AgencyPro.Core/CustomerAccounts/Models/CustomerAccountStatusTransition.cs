// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Core.CustomerAccounts.Models
{
    public class CustomerAccountStatusTransition : IObjectState
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }
        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }
        public AccountStatus Status { get; set; }
        public ObjectState ObjectState { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
