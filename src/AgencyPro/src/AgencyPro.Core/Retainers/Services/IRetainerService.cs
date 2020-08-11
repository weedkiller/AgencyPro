// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Retainers.ViewModels;

namespace AgencyPro.Core.Retainers.Services
{
    public interface IRetainerService
    {
        Task<RetainerOutput> GetRetainer(IOrganizationCustomer customer, Guid retainerId);

        Task<RetainerResult> SetupRetainer(IOrganizationCustomer customer, Guid projectId,
            CreateRetainerOptions options);

        Task<RetainerResult> TopOffRetainer(IOrganizationCustomer customer, Guid retainerId,
            TopoffRetainerOptions options);

        Task<RetainerResult> ApplyRetainerToInvoice(IOrganizationCustomer customer, Guid retainerId, string invoiceId);


    }
}