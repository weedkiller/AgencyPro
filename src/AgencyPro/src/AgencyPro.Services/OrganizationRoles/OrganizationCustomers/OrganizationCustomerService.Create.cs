// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationCustomers;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationCustomers
{
    public partial class OrganizationCustomerService
    {
        public async Task<T> Create<T>(OrganizationCustomerInput model)
            where T : OrganizationCustomerOutput
        {
            var entity = new OrganizationCustomer()
            {
                Updated = DateTimeOffset.UtcNow,
                UpdatedById = _userInfo.UserId,
                Created = DateTimeOffset.UtcNow,
                CreatedById = _userInfo.UserId
            };
            entity.InjectFrom(model);

            await Repository.InsertAsync(entity, true);

            var output = await GetById<T>(model.CustomerId, model.OrganizationId);

            await Task.Run(() =>
            {
                RaiseEvent(new OrganizationCustomerCreatedEvent
                {
                    OrganizationCustomer = output
                });
            });

            return output;
        }

        public Task<T> Create<T>(IAgencyOwner ao, OrganizationCustomerInput input)
            where T : OrganizationCustomerOutput
        {
            return Create<T>(input);
        }
    }
}