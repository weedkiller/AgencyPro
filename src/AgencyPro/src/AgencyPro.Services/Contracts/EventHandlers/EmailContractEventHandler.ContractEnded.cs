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
        private void ContractEndedSendContractorEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);

            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ContractorContractEnded, contract,
                    $@"[{contract.ContractorOrganizationName} : Contracting] Contract Ended");

                AddContractNotification(context, "Contract was ended", contract);

            }

        }

        private void ContractEndedSendProjectManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ProjectManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerContractEnded, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Project Management] Contract Ended");
            }
           

        }

        private void ContractEndedSendAccountManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AccountManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractEnded, contract,
                    $@"[{contract.AccountManagerOrganizationName} : Account Management] Contract Ended");
            }
           

        }

        private void ContractEndedSendAgencyOwnerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AgencyOwnerContractEmail>(ProjectionMapping)
                    .First();
                
                contract.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerContractEnded, contract,
                    $@"[{contract.ContractorOrganizationName}] Contract Ended");
            }
           

        }

        public void Handle(ContractEndedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Contract Ended Event Triggered"));
            
            Parallel.Invoke(new List<Action>
            {
                () => ContractEndedSendContractorEmail(evt.ContractId),
                () => ContractEndedSendAccountManagerEmail(evt.ContractId),
                () => ContractEndedSendProjectManagerEmail(evt.ContractId),
                () => ContractEndedSendAgencyOwnerEmail(evt.ContractId)
            }.ToArray());
        }
    }
}