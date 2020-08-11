// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Candidates.Enums;

namespace AgencyPro.Core.Candidates.Services
{
    public interface ICandidate
    {
        CandidateStatus Status { get; }
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EmailAddress { get; set; }
        string PhoneNumber { get; set; }
        decimal RecruiterStream { get; set; }
        bool IsContacted { get; set; }
        RejectionReason RejectionReason { get; set; }
        string RejectionDescription { get; set; }
        Guid? ProjectManagerId { get; set; }
        Guid? ProjectManagerOrganizationId { get; set; }
        string Description { get; set; }
        Guid CreatedById { get; set; }
        Guid UpdatedById { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        decimal RecruiterBonus { get; set; }
        decimal RecruitingAgencyStream { get; set; }
        decimal RecruitingAgencyBonus { get; set; }
        Guid ProviderOrganizationId { get; set; }
        Guid RecruiterId { get; set; }
        Guid RecruiterOrganizationId { get; set; }
    }
}