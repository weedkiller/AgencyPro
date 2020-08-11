// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Contracts.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class ContractNotification : Notification
    {
        public Guid ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}