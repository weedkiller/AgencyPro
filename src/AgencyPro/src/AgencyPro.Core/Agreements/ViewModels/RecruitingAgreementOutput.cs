// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Agreements.Services;
using System;

namespace AgencyPro.Core.Agreements.ViewModels
{
    public abstract class RecruitingAgreementOutput : IRecruitingAgreement
    {
        public Guid ProviderOrganizationId { get; set; }
        public Guid RecruitingOrganizationId { get; set; }
        public AgreementStatus Status { get; set; }
        public virtual bool InitiatedByProvider { get; set; }
        
        public virtual decimal RecruitingStream => RecruitingAgencyStream + RecruiterStream;

        public virtual decimal RecruitingBonus => RecruiterBonus + RecruitingAgencyBonus;

        public string RecruitingOrganizationName { get; set; }
        public string RecruiterOrganizationImageUrl { get; set; }

        public string ProviderOrganizationName { get; set; }
        public string ProviderOrganizationImageUrl { get; set; }
        public virtual decimal RecruiterStream { get; set; }
        public virtual decimal RecruitingAgencyBonus { get; set; }
        public virtual decimal RecruiterBonus { get; set; }
        public virtual decimal RecruitingAgencyStream { get; set; }


    }
}