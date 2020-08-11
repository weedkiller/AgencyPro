// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Invoices.Models;

namespace AgencyPro.Core.Notifications.Models
{
    public class InvoiceNotification : Notification
    {
        public string InvoiceId { get; set; }
        public Guid ProjectId { get; set; }

        public ProjectInvoice Invoice { get; set; }
    }
}