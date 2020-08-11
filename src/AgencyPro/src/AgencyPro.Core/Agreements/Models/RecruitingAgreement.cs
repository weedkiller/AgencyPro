// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;

namespace AgencyPro.Core.Agreements.Models
{
    public class RecruitingAgreement : AuditableEntity, IRecruitingAgreement
    {
        public Guid ProviderOrganizationId { get; set; }
        public ProviderOrganization ProviderOrganization { get; set; }

        public Guid RecruitingOrganizationId { get; set; }
        public RecruitingOrganization RecruitingOrganization { get; set; }

        public bool InitiatedByProvider { get; set; }

        public virtual decimal RecruiterStream { get; set; }
        public virtual decimal RecruitingAgencyBonus { get; set; }
        public virtual decimal RecruiterBonus { get; set; }
        public virtual decimal RecruitingAgencyStream { get; set; }

        public string RecruiterInformation { get; set; }


        public decimal RecruitingStream
        {
            get { return RecruiterStream + RecruitingAgencyStream; }
            set { }
        }

        public decimal RecruitingBonus
        {
            get { return RecruiterBonus + RecruitingAgencyBonus; }
            set { }
        }

        public AgreementStatus Status { get; set; }
    }
}