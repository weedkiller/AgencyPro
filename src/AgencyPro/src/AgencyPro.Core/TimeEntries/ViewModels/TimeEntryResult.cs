﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class TimeEntryResult : BaseResult
    {
        public Guid? TimeEntryId { get; set; }
        public Guid[] TimeEntryIds { get; set; }
    }
}