// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Agreements.Models;

namespace AgencyPro.Core.Agreements.Services
{
    public interface IMarketingAgreement
    {
        Guid ProviderOrganizationId { get; set; }
        Guid MarketingOrganizationId { get; set; }
        decimal MarketerBonus { get; set; }
        decimal MarketingAgencyStream { get; set; }
        AgreementStatus Status { get; set; }
        bool InitiatedByProvider { get; set; }
        decimal MarketingAgencyBonus { get; set; }
        decimal MarketerStream { get; set; }
    }
}