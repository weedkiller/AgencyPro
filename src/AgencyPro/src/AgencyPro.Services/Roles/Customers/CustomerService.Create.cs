// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Customers;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Customers
{
    public partial class CustomerService
    {
        public async Task<T> Create<T>(CustomerInput input)
            where T : CustomerOutput
        {
            _logger.LogTrace(GetLogMessage($@"Person Id: {input.PersonId}"));

            var customer = await Repository.Queryable().Where(x=>x.Id == input.PersonId)
                .FirstOrDefaultAsync();

            var marketer =  _marketerRepository.Queryable()
                .GetById(input.MarketerOrganizationId,
                input.MarketerId).First();
            
            if (customer != null)
                return await GetById<T>(input.PersonId);

            if (marketer == null)
                throw new ApplicationException("Marketer not found");

            customer = new Customer
            {
                Id = input.PersonId,
                MarketerId = input.MarketerId,
                MarketerOrganizationId = input.MarketerOrganizationId
            };

            customer.InjectFrom(input);

            await Repository.InsertAsync(customer, true);

            var output = await GetById<T>(input.PersonId);

            await Task.Run(() =>
            {
                RaiseEvent(new CustomerCreatedEvent<T>
                {
                    Customer = output
                });
            });

            return output;
        }
    }
}