// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.DisperseFunds.ViewModels;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.PayoutIntents.ViewModels;
using Stripe;

namespace AgencyPro.Core.DisperseFunds.Services
{
    public interface IDisperseFundsService
    {
        Task<DisperseFundsResult> DisperseInvoice(IProviderAgencyOwner ao, string invoiceId);
        Task<List<IndividualPayoutIntentOutput>> GetPending(IOrganizationPerson principal, PayoutFilters filters);
        Task<List<OrganizationPayoutIntentOutput>> GetPending(IAgencyOwner principal, PayoutFilters filters);
        Task<DisperseFundsResult> TransferCreated(Transfer transfer);
    }
}
