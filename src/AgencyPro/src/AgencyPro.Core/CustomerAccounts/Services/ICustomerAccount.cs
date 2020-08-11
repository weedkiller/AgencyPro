// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.CustomerAccounts.Enums;

namespace AgencyPro.Core.CustomerAccounts.Services
{
    public interface ICustomerAccount
    {
        int BuyerNumber { get; set; }
        int Number { get; set; }
        Guid CustomerId { get; set; }
        Guid CustomerOrganizationId { get; set; }
        AccountStatus AccountStatus { get; set; }
        Guid AccountManagerId { get; set; }
        Guid AccountManagerOrganizationId { get; set; }
        DateTimeOffset Created { get; set; }
        DateTimeOffset Updated { get; set; }
        decimal MarketerStream { get; set; }
        decimal MarketingAgencyStream { get; set; }
    }
}