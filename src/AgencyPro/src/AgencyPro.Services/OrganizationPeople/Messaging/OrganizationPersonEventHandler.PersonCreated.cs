// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.OrganizationPeople.Emails;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;

namespace AgencyPro.Services.OrganizationPeople.Messaging
{
    public partial class OrganizationPersonEventHandler
    {
        private void PersonCreatedSendAgencyOwnerEmail(Guid organizationId, Guid personId)
        {
            _logger.LogInformation(GetLogMessage("Organization:{0};Person:{0};"), organizationId, personId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.OrganizationPeople
                    .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
                    .ProjectTo<AgencyOwnerOrganizationPersonEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerPersonAdded, email,
                    $@"[{email.OrganizationName}] A person has been added");
            }
        }

        private void PersonCreatedSendPersonEmail(Guid organizationId, Guid personId)
        {
            _logger.LogInformation(GetLogMessage("Organization:{0};Person:{0};"), organizationId, personId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var email = context.OrganizationPeople
                    .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
                    .ProjectTo<OrganizationPersonEmail>(ProjectionMapping)
                    .First();

                email.Initialize(Settings);

                Send(TemplateTypes.OrganizationPersonAdded, email,
                    $@"You have been added to {email.OrganizationName}");
            }
        }

        public void Handle(OrganizationPersonCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Person created and added to organization"));

            Parallel.Invoke(new List<Action>
            {
                () => PersonCreatedSendAgencyOwnerEmail(evt.OrganizationId, evt.PersonId),
               // () => PersonCreatedSendPersonEmail(evt.OrganizationId, evt.PersonId),
            }.ToArray());
        }
    }
}