// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.Models
{
    public class AuditLog : BaseObjectState
    {
        public Guid Id { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Event { get; set; }
        public DateTimeOffset DataTime { get; set; }

        public Guid? UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}