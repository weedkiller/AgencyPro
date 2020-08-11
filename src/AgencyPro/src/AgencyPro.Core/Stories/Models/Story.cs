// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Core.TimeEntries.Models;

namespace AgencyPro.Core.Stories.Models
{
    public class Story : AuditableEntity, IStory
    {
        public Project Project { get; set; }
        public OrganizationContractor OrganizationContractor { get; set; }

        public ICollection<TimeEntry> TimeEntries { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<StoryNotification> Notifications { get; set; }

        public Guid Id { get; set; }
        public string StoryId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public Guid? ContractorOrganizationId { get; set; }
        public int? StoryPoints { get; set; }


        private ICollection<StoryStatusTransition> _statusTransitions;

        public virtual ICollection<StoryStatusTransition> StatusTransitions
        {
            get => _statusTransitions ?? (_statusTransitions = new Collection<StoryStatusTransition>());
            set => _statusTransitions = value;
        }

        /// <summary>
        /// This gets set when the proposal is accepted
        /// </summary>
        public decimal? CustomerApprovedHours { get; set; }

        public DateTimeOffset? DueDate { get; set; }
        public StoryStatus Status { get; set; }
        public DateTimeOffset? AssignedDateTime { get; set; }
        public DateTimeOffset? ProjectManagerAcceptanceDate { get; set; }
        public DateTimeOffset? CustomerAcceptanceDate { get; set; }

        [MaxLength(500)] public string Title { get; set; }

        [MaxLength(5000)] public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }

        public Guid? StoryTemplateId { get; set; }
        public StoryTemplate StoryTemplate { get; set; }
        public bool IsDeleted { get; set; }

        public decimal TotalHoursLogged { get; set; }
    }
}