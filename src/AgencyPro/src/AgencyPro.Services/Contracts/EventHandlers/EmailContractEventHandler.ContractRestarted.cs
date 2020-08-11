// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Contracts.Emails;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.EmailSending.Services;
using AgencyPro.Core.Templates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Contracts.EventHandlers
{
    public partial class MultiContractEventHandler
    {
        private void ContractRestartedSendContractorEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {

                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.ContractorContractRestarted, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Contracting] Contract has been restarted");

                AddContractNotification(context, "Contract was restarted", contract);

            }

        }

        private void ContractRestartedSendCustomerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

        }

        private void ContractRestartedSendProjectManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ProjectManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerContractRestarted, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Project Management] Contract Restarted");

                AddContractNotification(context, "Contract was restarted", contract);

            }



        }

        private void ContractRestartedSendAccountManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AccountManagerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractRestarted, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Account Management] Contract Restarted");

                AddContractNotification(context, "Contract was restarted", contract);

            }
        }

        private void ContractRestartedSendAgencyOwnerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AgencyOwnerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerContractRestarted, contract,
                    $@"[{contract.ProjectManagerOrganizationName}] Contract Restarted");

                AddContractNotification(context, "Contract was restarted", contract);

            }

        }

        public void Handle(ContractRestartedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Contract Restarted Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => ContractRestartedSendContractorEmail(evt.ContractId),
                () => ContractRestartedSendCustomerEmail(evt.ContractId),
                () => ContractRestartedSendContractorEmail(evt.ContractId),
                () => ContractRestartedSendProjectManagerEmail(evt.ContractId),
                () => ContractRestartedSendAccountManagerEmail(evt.ContractId),
                () => ContractRestartedSendAgencyOwnerEmail(evt.ContractId)
            }.ToArray());

        }
    }
}