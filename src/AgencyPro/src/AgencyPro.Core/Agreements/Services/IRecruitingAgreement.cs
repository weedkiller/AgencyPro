// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Agreements.Models;

namespace AgencyPro.Core.Agreements.Services
{
    public interface IRecruitingAgreement
    {
        Guid ProviderOrganizationId { get; set; }
        Guid RecruitingOrganizationId { get; set; }
        decimal RecruiterStream { get; set; }
        AgreementStatus Status { get; set; }
        bool InitiatedByProvider { get; set; }
        decimal RecruitingAgencyBonus { get; set; }
        decimal RecruiterBonus { get; set; }
        decimal RecruitingAgencyStream { get; set; }
    }
}