// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using System;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;

namespace AgencyPro.Core.Agreements.Models
{
    public class MarketingAgreement : AuditableEntity, IMarketingAgreement
    {
        public Guid ProviderOrganizationId { get; set; }
        public ProviderOrganization ProviderOrganization { get; set; }

        public Guid MarketingOrganizationId { get; set; }
        public MarketingOrganization MarketingOrganization { get; set; }

        public bool InitiatedByProvider { get; set; }

        public decimal MarketerBonus { get; set; }
        public decimal MarketingAgencyStream { get; set; }
        public decimal MarketingAgencyBonus { get; set; }
        public decimal MarketerStream { get; set; }
        public bool RequireUniqueEmail { get; set; }

        public string MarketerInformation { get; set; }

        public decimal MarketingStream
        {
            get { return MarketerStream + MarketingAgencyStream; }
            set { }
        }

        public decimal MarketingBonus
        {
            get { return MarketerBonus + MarketingAgencyBonus; }
            set { }
        }

        public AgreementStatus Status { get; set; }
    }
}