// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Candidates.Services;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.Candidates.Models
{
    public class Candidate : AuditableEntity, ICandidate
    {
        public OrganizationRecruiter OrganizationRecruiter { get; set; }
        public OrganizationProjectManager OrganizationProjectManager { get; set; }
        public ProviderOrganization ProviderOrganization { get; set; }
        public Guid ProviderOrganizationId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        private ICollection<CandidateStatusTransition> _statusTransitions;

        public virtual ICollection<CandidateStatusTransition> StatusTransitions
        {
            get => _statusTransitions ?? (_statusTransitions = new Collection<CandidateStatusTransition>());
            set => _statusTransitions = value;
        }

       public ICollection<CandidateNotification> CandidateNotifications { get; set; }

        public virtual CandidateStatus Status { get; set; }


        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
        public decimal RecruiterStream { get; set; }
        public decimal RecruiterBonus { get; set; }

        public decimal RecruitingAgencyStream { get; set; }
        public decimal RecruitingAgencyBonus { get; set; }

        public bool IsContacted { get; set; }
        public Guid RecruiterId { get; set; }
        public Guid RecruiterOrganizationId { get; set; }
        public Recruiter Recruiter { get; set; }

        public Organization RecruiterOrganization { get; set; }

        public RejectionReason RejectionReason { get; set; }
        public string RejectionDescription { get; set; }

        public Guid? ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public Guid? ProjectManagerOrganizationId { get; set; }

        public string Description { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public bool IsDeleted { get; set; }

        
        public IndividualBonusIntent IndividualBonusIntent { get; set; }
       
        public OrganizationBonusIntent OrganizationBonusIntent { get; set; }
    }
}