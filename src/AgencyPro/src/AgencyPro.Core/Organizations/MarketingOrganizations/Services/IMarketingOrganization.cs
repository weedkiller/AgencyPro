// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.Services
{
    public interface IMarketingOrganization
    {
        decimal MarketerStream { get; set; }
        decimal MarketingAgencyStream { get; set; }
        decimal MarketerBonus { get; set; }
        decimal MarketingAgencyBonus { get; set; }
        Guid DefaultMarketerId { get; set; }
        Guid Id { get; set; }
        decimal ServiceFeePerLead { get; set; }
    }
}