// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Retainers.ViewModels;

namespace AgencyPro.Services.Retainers
{
    public partial class RetainerService
    {
        public Task<RetainerResult> ApplyRetainerToInvoice(IOrganizationCustomer customer, Guid retainerId,
            string invoiceId)
        {
            throw new NotImplementedException();
        }
    }
}