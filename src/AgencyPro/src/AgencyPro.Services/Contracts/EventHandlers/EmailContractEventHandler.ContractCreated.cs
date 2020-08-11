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
        private void ContractCreatedSendContractorEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("ContractId: {0}"), contractId);


            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ContractorContractCreated, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Contracting] A new contract has been added");

                AddContractNotification(context, "Contract was created", contract);

            }


        }

        private void ContractCreatedSendProjectManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("ContractId: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ProjectManagerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerContractCreated, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Project Management] A new contract has been added");

                AddContractNotification(context, "Contract was created", contract);

            }

        }

        private void ContractCreatedSendAccountManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("ContractId: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {

                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AccountManagerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractCreated, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Account Management] A new contract has been added");

                AddContractNotification(context, "Contract was created", contract);

            }

        }

        private void ContractCreatedSendAgencyOwnerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("ContractId: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AgencyOwnerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerContractCreated, contract,
                    $@"[{contract.ProjectManagerOrganizationName}] A new contract has been added");

                AddContractNotification(context, "Contract was created", contract);

            }


        }

        public void Handle(ContractCreatedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Contract Created Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => ContractCreatedSendContractorEmail(evt.ContractId),
                () => ContractCreatedSendProjectManagerEmail(evt.ContractId),
                () => ContractCreatedSendAccountManagerEmail(evt.ContractId),
                () => ContractCreatedSendAgencyOwnerEmail(evt.ContractId)
            }.ToArray());
        }
    }
}