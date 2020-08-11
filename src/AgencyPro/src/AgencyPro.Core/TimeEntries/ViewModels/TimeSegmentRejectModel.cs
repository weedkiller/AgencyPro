// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.TimeEntries.ViewModels
{
    public class TimeSegmentRejectModel
    {
        [Required] public string Reason { get; set; }
    }
}