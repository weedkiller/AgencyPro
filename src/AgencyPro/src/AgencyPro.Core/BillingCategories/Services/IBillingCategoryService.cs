// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.BillingCategories.Services
{
    public interface IBillingCategoryService
    {
        Task<List<BillingCategoryOutput>> GetBillingCategoriesByCategory(int categoryId);
        Task<List<BillingCategoryOutput>> GetBillingCategoriesByOrganization(Guid organizationId);
        Task<List<BillingCategoryOutput>> GetBillingCategoriesByProject(Guid organizationId, Guid projectId);


        Task<bool> AddBillingCategory(IProviderAgencyOwner agencyOwner, int billingCategoryId);
        Task<bool> AddBillingCategoryToProject(IProviderAgencyOwner agencyOwner, Guid projectId, int billingCategoryId);
        Task<bool> RemoveBillingCategory(IProviderAgencyOwner agencyOwner, int billingCategoryId);
        Task<bool> RemoveBillingCategoryFromProject(IProviderAgencyOwner agencyOwner, Guid projectId, int billingCategoryId);
    }
}
