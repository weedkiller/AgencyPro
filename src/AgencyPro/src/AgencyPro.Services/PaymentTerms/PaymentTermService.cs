// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.PaymentTerms.Services;
using AgencyPro.Core.PaymentTerms.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.PaymentTerms
{
    public partial class PaymentTermService : Service<PaymentTerm>, IPaymentTermService
    {
        private readonly IRepositoryAsync<CategoryPaymentTerm> _categoryPaymentTermRepo;
        private readonly IRepositoryAsync<OrganizationPaymentTerm> _organizationPaymentTermRepo;

        public PaymentTermService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _categoryPaymentTermRepo = UnitOfWork.RepositoryAsync<CategoryPaymentTerm>();
            _organizationPaymentTermRepo = UnitOfWork.RepositoryAsync<OrganizationPaymentTerm>();
        }

        public Task<List<PaymentTermOutput>> GetPaymentTermsByCategory(int categoryId)
        {
            return _categoryPaymentTermRepo.Queryable()
                .Where(x => x.CategoryId == categoryId)
                .ProjectTo<PaymentTermOutput>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<List<PaymentTermOutput>> GetPaymentTermsByOrganization(Guid organizationId)
        {
            return _organizationPaymentTermRepo.Queryable()
                .Where(x => x.OrganizationId == organizationId)
                .ProjectTo<PaymentTermOutput>(ProjectionMapping)
                .ToListAsync();
        }
    }
}
