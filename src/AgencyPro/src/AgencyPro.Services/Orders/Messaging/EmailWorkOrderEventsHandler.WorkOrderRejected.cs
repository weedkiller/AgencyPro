// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Orders.Emails;
using AgencyPro.Core.Orders.Events;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Orders.Messaging
{
    public partial class MultiWorkOrderEventsHandler
    {
        private void WorkOrderRejectedSendAccountManagerEmail(Guid id)
        {
            _logger.LogInformation(GetLogMessage("Work Order: {0}"), id);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.WorkOrders.Where(x => x.Id == id)
                    .ProjectTo<AccountManagerWorkOrderEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);


                Send(TemplateTypes.AccountManagerWorkOrderRejected, email,
                    $@"[{email.AccountManagerOrganizationName} : Account Management] A work order has been rejected");
            }


        }

        private void WorkOrderRejectedSendAgencyOwnerEmail(Guid id)
        {
            _logger.LogInformation(GetLogMessage("Work Order: {0}"), id);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.WorkOrders.Where(x => x.Id == id)
                    .ProjectTo<AgencyOwnerWorkOrderEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);


                Send(TemplateTypes.AgencyOwnerWorkOrderRejected, email,
                    $@"[{email.AccountManagerOrganizationName}] A work order has been rejected");
            }


        }

        private void WorkOrderRejectedSendCustomerEmail(Guid id)
        {
            _logger.LogInformation(GetLogMessage("Work Order: {0}"), id);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.WorkOrders.Where(x => x.Id == id)
                    .ProjectTo<CustomerWorkOrderEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);


                Send(TemplateTypes.CustomerWorkOrderRejected, email,
                    $@"A work order has been accepted");
            }


        }

        public void Handle(WorkOrderRejectedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Work order accepted event triggered"));

            Parallel.Invoke(new List<Action>
            {
                () => WorkOrderRejectedSendAccountManagerEmail(evt.WorkOrderId),
                () => WorkOrderRejectedSendAgencyOwnerEmail(evt.WorkOrderId),
                () => WorkOrderRejectedSendCustomerEmail(evt.WorkOrderId),
            }.ToArray());
        }
    }
}