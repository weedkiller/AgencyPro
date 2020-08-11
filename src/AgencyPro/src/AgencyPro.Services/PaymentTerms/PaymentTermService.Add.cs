// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.PaymentTerms.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.PaymentTerms
{
    public partial class PaymentTermService
    {
        private async Task<bool> AddPaymentTermToOrganization(Guid organizationId, int paymentTermId)
        {
            var paymentTerm = await _organizationPaymentTermRepo.Queryable()
                .Where(x => x.OrganizationId == organizationId && x.PaymentTermId == paymentTermId)
                .FirstOrDefaultAsync();

            int result = 0;
            if (paymentTerm == null)
            {
                paymentTerm = new OrganizationPaymentTerm()
                {
                    PaymentTermId = paymentTermId,
                    OrganizationId = organizationId
                };
                result = await _organizationPaymentTermRepo.InsertAsync(paymentTerm, true);
            }

            return result != 0;
        }

        public async Task<bool> AddPaymentTermToOrganization(IAgencyOwner ao, int paymentTermId)
        {
            return await AddPaymentTermToOrganization(ao.OrganizationId, paymentTermId);
        }
        
        public async Task<bool> AddPaymentTermToOrganization(IOrganizationAccountManager am, int paymentTermId)
        {
            return await AddPaymentTermToOrganization(am.OrganizationId, paymentTermId);
        }
    }
}