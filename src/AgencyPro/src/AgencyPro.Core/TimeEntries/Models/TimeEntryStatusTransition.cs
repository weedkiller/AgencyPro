// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.TimeEntries.Enums;

namespace AgencyPro.Core.TimeEntries.Models
{
    public class TimeEntryStatusTransition : IObjectState
    {
        public int Id { get; set; }
        
        public Guid TimeEntryId { get; set; }

        public TimeStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}