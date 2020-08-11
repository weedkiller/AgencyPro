// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Retainers.Services;
using AgencyPro.Core.Retainers.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Retainers
{
    public partial class RetainerService
    {
        public async Task<RetainerResult> TopOffRetainer(IOrganizationCustomer customer, Guid retainerId,
            TopoffRetainerOptions options)
        {
            _logger.LogInformation(GetLogMessage("Customer:{0}; Organization:{1}; Retainer: {2}"),
                customer.CustomerId, customer.OrganizationId, retainerId);

            var retainerResult = new RetainerResult();

            var retainer = await Repository.Queryable().Where(x =>
                    x.CustomerId == customer.CustomerId && x.CustomerOrganizationId == customer.OrganizationId &&
                    x.ProjectId == retainerId)
                .Include(x => x.Project)
                .ThenInclude(x => x.Proposal)
                .ThenInclude(x => x.ProposalAcceptance)
                .Include(x => x.Credits)
                .FirstOrDefaultAsync();
            
            var retainerAmount = retainer.TopOffAmount;
            var currentBalance = retainer.CurrentBalance;

            var chargeAmount = Math.Min(Math.Max(retainerAmount - currentBalance, 0), retainerAmount);

            _logger.LogDebug(GetLogMessage("Charge Amount: {0}; Current Balance: {1}; Retainer Amount: {2}"),
                chargeAmount, currentBalance, chargeAmount);

            // make the charge for top off amount
            
            var records = await Repository.UpdateAsync(retainer, true);
            
            _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

            if (records > 0)
            {
                retainerResult.Succeeded = true;
                retainerResult.RetainerId = retainerId;
                retainerResult.CurrentBalance = retainer.CurrentBalance;
                retainerResult.TopOffAmount = retainer.TopOffAmount;
            }
            

            return retainerResult;
        }
    }
}