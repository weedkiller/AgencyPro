// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.CustomerBalance.Services;
using AgencyPro.Core.CustomerBalance.ViewModels;
using AgencyPro.Core.Data.Repositories;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.Enums;
using AgencyPro.Core.TimeEntries.Models;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.CustomerBalance
{
    public class CustomerBalanceService : Service<OrganizationCustomer>, ICustomerBalanceService
    {
        private readonly IRepositoryAsync<ProjectInvoice> _invoices;
        private readonly IRepositoryAsync<TimeEntry> _timeEntries;

        public CustomerBalanceService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _invoices = UnitOfWork.RepositoryAsync<ProjectInvoice>();
            _timeEntries = UnitOfWork.RepositoryAsync<TimeEntry>();
        }

        public async Task<CustomerBalanceOutput> GetBalance(IOrganizationCustomer cu)
        {
            var retVal = new CustomerBalanceOutput();

            var invoices = await _invoices.Queryable()
                .Include(x=>x.Invoice)
                .Where(x => x.BuyerOrganizationId == cu.OrganizationId)
                .ToListAsync();

            var customerAmount = _timeEntries.Queryable()
                .Where(x => x.Status == TimeStatus.Approved)
                .Where(x => x.CustomerOrganizationId == cu.OrganizationId)
                .Sum(x => x.TotalCustomerAmount);
            
            retVal.AmountPaid = invoices.Where(x => x.Invoice.Status == "paid")
                .Sum(x => x.Invoice.AmountPaid);

            retVal.AmountDue = invoices.Where(x => x.Invoice.Status == "open")
                .Sum(x => x.Invoice.AmountDue);

            retVal.AmountApproved = customerAmount;

            return retVal;
        }
    }
}
