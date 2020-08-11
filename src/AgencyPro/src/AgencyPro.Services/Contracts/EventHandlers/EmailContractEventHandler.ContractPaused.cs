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
        private void ContractPausedSendContractorEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ContractorContractPaused, contract,
                    $@"[{contract.ContractorOrganizationName} : Contracting] Contract paused");

                AddContractNotification(context, "Contract was paused", contract);

            }

        }

        private void ContractPausedSendProjectManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ProjectManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerContractPaused, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Project Management] Contract paused");

                AddContractNotification(context, "Contract was paused", contract);

            }


        }

        private void ContractPausedSendAccountManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AccountManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractPaused, contract,
                    $@"[{contract.AccountManagerOrganizationName} : Account Management] Contract paused");

                AddContractNotification(context, "Contract was paused", contract);

            }



        }


        private void ContractPausedSendAgencyOwnerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AgencyOwnerContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractPaused, contract,
                    $@"[{contract.ContractorOrganizationName}] Contract paused");
            }

            

        }

        public void Handle(ContractPausedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Contract Paused Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => ContractPausedSendContractorEmail(evt.ContractId),
                () => ContractPausedSendAccountManagerEmail(evt.ContractId),
                () => ContractPausedSendAgencyOwnerEmail(evt.ContractId),
                () => ContractPausedSendProjectManagerEmail(evt.ContractId)
            }.ToArray());
        }
    }
}