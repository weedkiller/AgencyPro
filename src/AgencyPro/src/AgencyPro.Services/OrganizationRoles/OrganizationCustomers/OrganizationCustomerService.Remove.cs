// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Services.OrganizationRoles.OrganizationCustomers
{
    public partial class OrganizationCustomerService
    {
        public Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            throw new NotImplementedException();
        }


        private Task Remove(IOrganizationCustomer customer
        )
        {
            //_logger.LogTrace(GetLogMessage($@"Marketer: {marketer.MarketerId}, Organization: {marketer.OrganizationId}"));

            //var entity = await Repository.FirstOrDefaultAsync(x =>
            //    x.OrganizationId == marketer.OrganizationId && x.MarketerId == marketer.MarketerId);
            //entity.Status = PersonActivityStatus.Inactive;

            //await Repository.UpdateAsync(entity, true);

            //var output = await GetById(marketer.MarketerId, marketer.OrganizationId);

            //await Task.Run(() =>
            //{
            //    RaiseEvent(new OrganizationMarketerRemovedEvent
            //    {
            //        OrganizationMarketerOutput = output
            //    });
            //});

            return Task.FromResult(0);
        }
    }
}