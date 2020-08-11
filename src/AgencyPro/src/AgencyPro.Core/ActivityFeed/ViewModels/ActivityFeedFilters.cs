// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Notifications.Models;

namespace AgencyPro.Core.ActivityFeed.ViewModels
{
    public class ActivityFeedFilters
    {
        public ActivityFeedFilters()
        {
            this.Type = new List<NotificationType>();
        }

        public List<NotificationType> Type { get; set; }
        
        public DateTime? MaxDate { get; set; }

        public Guid? ProjectId { get; set; }
        public Guid? LeadId { get; set; }

        public Guid? ContractId { get; set; }
        public Guid? CandidateId { get; set; }
        public Guid? ProposalId { get; set; }
        public Guid? StoryId { get; set; }
    }
}