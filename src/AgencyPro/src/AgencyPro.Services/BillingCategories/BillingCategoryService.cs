// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.BillingCategories.ViewModels;
using AgencyPro.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.BillingCategories
{
    public class BillingCategoryService : Service<BillingCategory>, IBillingCategoryService
    {
        private readonly IRepositoryAsync<CategoryBillingCategory> _catBillingCatRepo;
        private readonly IRepositoryAsync<OrganizationBillingCategory> _orgBillingCatRepo;
        private readonly IRepositoryAsync<ProjectBillingCategory> _projBillingCatRepo;

        public BillingCategoryService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _catBillingCatRepo = UnitOfWork.RepositoryAsync<CategoryBillingCategory>();
            _projBillingCatRepo = UnitOfWork.RepositoryAsync<ProjectBillingCategory>();
            _orgBillingCatRepo = UnitOfWork.RepositoryAsync<OrganizationBillingCategory>();
        }

        public Task<List<BillingCategoryOutput>> GetBillingCategoriesByCategory(int categoryId)
        {
            return _catBillingCatRepo.Queryable()
                .Where(x => x.CategoryId == categoryId)
                .Include(x => x.BillingCategory)
                .Select(x => x.BillingCategory)
                .ProjectTo<BillingCategoryOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<BillingCategoryOutput>> GetBillingCategoriesByOrganization(Guid organizationId)
        {
            return _orgBillingCatRepo.Queryable()
                .Where(x => x.OrganizationId == organizationId)
                .Include(x => x.BillingCategory)
                .Select(x => x.BillingCategory)
                .ProjectTo<BillingCategoryOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<BillingCategoryOutput>> GetBillingCategoriesByProject(Guid organizationId, Guid projectId)
        {
            return _projBillingCatRepo.Queryable()
                .Where(x => x.ProjectId == projectId && x.Project.AccountManagerOrganizationId == organizationId)
                .Include(x => x.BillingCategory)
                .Select(x => x.BillingCategory)
                .ProjectTo<BillingCategoryOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public async Task<bool> AddBillingCategory(IProviderAgencyOwner agencyOwner, int billingCategoryId)
        {
            var entity = await _orgBillingCatRepo.Queryable()
                .Where(x => x.OrganizationId == agencyOwner.OrganizationId && x.BillingCategoryId == billingCategoryId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new OrganizationBillingCategory()
                {
                    OrganizationId = agencyOwner.OrganizationId, 
                    BillingCategoryId = billingCategoryId,
                    ObjectState = ObjectState.Added
                };
                var result = await _orgBillingCatRepo.InsertAsync(entity, true);
                return result != 0;
            }

            return false;
        }

        public async Task<bool> AddBillingCategoryToProject(IProviderAgencyOwner agencyOwner, Guid projectId, int billingCategoryId)
        {
            var entity = await _projBillingCatRepo.Queryable()
                .Where(x => x.Project.AccountManagerOrganizationId == agencyOwner.OrganizationId &&
                            x.ProjectId == projectId && x.BillingCategoryId == billingCategoryId)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                entity = new ProjectBillingCategory()
                {
                    BillingCategoryId = billingCategoryId,
                    ProjectId = projectId,
                };

                var result = await _projBillingCatRepo.InsertAsync(entity, true);
                return result != 0;
            }

            return false;
        }


        public async Task<bool> RemoveBillingCategory(IProviderAgencyOwner agencyOwner, int billingCategoryId)
        {
            return await _orgBillingCatRepo.DeleteAsync(
                x => x.OrganizationId == agencyOwner.OrganizationId && x.BillingCategoryId == billingCategoryId, true);
        }

        public Task<bool> RemoveBillingCategoryFromProject(IProviderAgencyOwner agencyOwner, Guid projectId, int billingCategoryId)
        {
            return _projBillingCatRepo.DeleteAsync(x =>
                x.ProjectId == projectId && x.BillingCategoryId == billingCategoryId &&
                x.Project.AccountManagerOrganizationId == agencyOwner.OrganizationId, true);
        }
    }
}
