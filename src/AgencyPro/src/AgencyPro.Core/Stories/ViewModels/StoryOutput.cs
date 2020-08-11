﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stories.Enums;
using AgencyPro.Core.Stories.Services;
using System;
using System.Collections.Generic;
using AgencyPro.Core.Organizations.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.Stories.ViewModels
{
    public abstract class StoryOutput : StoryInput, IStory, IOrganizationPersonTarget
    {
        public virtual int TotalMinutesLogged { get; set; }

        public virtual string ProjectName { get; set; }
        public virtual string ProjectAbbreviation { get; set; }
        public virtual Guid ProjectManagerId { get; set; }
        public virtual string ProjectManagerName { get; set; }
        public virtual string ProjectManagerImageUrl { get; set; }
        public virtual string ProjectManagerEmail { get; set; }
        public virtual string ProjectManagerPhoneNumber { get; set; }
        public virtual string ProjectManagerOrganizationName { get; set; }
        public virtual string ProjectManagerOrganizationImageUrl { get; set; }
        public virtual Guid Id { get; set; }
        public virtual string StoryId { get; set; }

        public virtual Guid? ContractorId { get; set; }

        public virtual Guid ProviderOrganizationOwnerId { get; set; }
        public virtual Guid AccountManagerId { get; set; }
        public Dictionary<DateTimeOffset, StoryStatus> StatusTransitions { get; set; }


        [JsonIgnore]
        public virtual Guid? ContractorOrganizationId { get; set; }
        
        public virtual string ContractorName { get; set; }
        public virtual string ContractorImageUrl { get; set; }
        public virtual string ContractorEmail { get; set; }
        public virtual string ContractorPhoneNumber { get; set; }

        public virtual DateTimeOffset? DueDate { get; set; }
        public virtual StoryStatus Status { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Updated { get; set; }

        public virtual bool IsCustomerApproved { get; set; }
        public virtual decimal? CustomerApprovedHours { get; set; }

        public virtual decimal TotalHoursLogged { get; set; }

        [JsonIgnore]
        public abstract Guid TargetOrganizationId { get; }

        [JsonIgnore]
        public abstract Guid TargetPersonId { get;  }

        public virtual string ProviderOrganizationName { get; set; }
        public virtual string ProviderOrganizationImageUrl { get; set; }
        public virtual Guid ProviderOrganizationId { get; set; }

        public virtual string CustomerOrganizationName { get; set; }
        public virtual string CustomerOrganizationImageUrl { get; set; }
        public virtual Guid CustomerOrganizationId { get; set; }
        public virtual Guid CustomerId { get; set; }
    }
}