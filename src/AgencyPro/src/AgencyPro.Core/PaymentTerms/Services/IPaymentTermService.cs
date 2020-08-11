// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.PaymentTerms.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.PaymentTerms.Services
{
    public interface IPaymentTermService : IService<PaymentTerm>
    {
        Task<List<PaymentTermOutput>> GetPaymentTermsByCategory(int categoryId);
        Task<List<PaymentTermOutput>> GetPaymentTermsByOrganization(Guid organizationId);
        Task<bool> AddPaymentTermToOrganization(IAgencyOwner ao, int paymentTermId);
        Task<bool> AddPaymentTermToOrganization(IOrganizationAccountManager am, int paymentTermId);
        Task<bool> RemovePaymentFromOrganization(IAgencyOwner ao, int paymentTermId);
        Task<bool> RemovePaymentFromOrganization(IOrganizationAccountManager ao, int paymentTermId);
    }
}
