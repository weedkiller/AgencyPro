// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.PaymentTerms
{
    public partial class PaymentTermService
    {
        private async Task<bool> RemovePaymentFromOrganization(Guid organizationId, int paymentTermId)
        {
            var result = await _organizationPaymentTermRepo.DeleteAsync(
                x => x.OrganizationId == organizationId && x.PaymentTermId == paymentTermId, true);

            return result;
        }

        public async Task<bool> RemovePaymentFromOrganization(IAgencyOwner ao, int paymentTermId)
        {
            var result = await RemovePaymentFromOrganization(ao.OrganizationId, paymentTermId);
            return result;
        }

        public async Task<bool> RemovePaymentFromOrganization(IOrganizationAccountManager ao, int paymentTermId)
        {
            var result = await RemovePaymentFromOrganization(ao.OrganizationId, paymentTermId);
            return result;
        }
    }
}