// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Projects.Extensions;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Retainers.Services;
using AgencyPro.Core.Retainers.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Retainers
{
    public partial class RetainerService
    {
        public async Task<RetainerResult> SetupRetainer(IOrganizationCustomer customer, Guid projectId,
            CreateRetainerOptions options)
        {
            _logger.LogInformation(GetLogMessage("Customer:{0}; Project:{1}"), customer.CustomerId, projectId);

            var retVal = new RetainerResult();

            var project = await _projects.Queryable()
                .ForOrganizationCustomer(customer)
                .Include(x=>x.CustomerAccount)
                .Where(x => x.Id == projectId)
                .FirstAsync();

            var retainer = new ProjectRetainerIntent()
            {
                AccountManagerId = project.CustomerAccount.AccountManagerId,
                ProviderOrganizationId = project.CustomerAccount.AccountManagerOrganizationId,
                CustomerId = project.CustomerAccount.CustomerId,
                CustomerOrganizationId = project.CustomerAccount.CustomerOrganizationId,
                ProjectId = projectId,
                TopOffAmount = options.TopOffAmount,
                CurrentBalance = 0
            };

            var records = await Repository.InsertAsync(retainer, true);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.RetainerId = projectId;
            }

            return retVal;
        }
    }
}