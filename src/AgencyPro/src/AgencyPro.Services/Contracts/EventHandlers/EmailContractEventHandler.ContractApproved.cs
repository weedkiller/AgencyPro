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

        private void ContractApprovedSendContractorEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract ID: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ContractorContractEmail>(ProjectionMapping)
                    .First();

                contract.Initialize(Settings);

                Send(TemplateTypes.ContractorContractApproved, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Contracting] A new contract has been approved");

                AddContractNotification(context, "Contract was approved", contract);

            }

        }

        private void ContractApprovedSendAgencyOwnerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract ID: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AgencyOwnerContractEmail>(ProjectionMapping)
                    .First();
                
                contract.Initialize(Settings);

                Send(TemplateTypes.AgencyOwnerContractApproved, contract,
                    $@"[{contract.ProjectManagerOrganizationName}] A new contract has been approved");

                AddContractNotification(context, "Contract was approved", contract);

            }

        }

        private void ContractApprovedSendAccountManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract ID: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<AccountManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.AccountManagerContractApproved, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Account Management] A new contract has been approved");

                AddContractNotification(context, "Contract was approved", contract);
            }

        }

        private void ContractApprovedSendProjectManagerEmail(Guid contractId)
        {
            _logger.LogInformation(GetLogMessage("Contract ID: {0}"), contractId);
            using (var context = new AppDbContext(DbContextOptions))
            {
                var contract = context.Contracts
                    .Where(x => x.Id == contractId)
                    .ProjectTo<ProjectManagerContractEmail>(ProjectionMapping)
                    .First();


                contract.Initialize(Settings);

                Send(TemplateTypes.ProjectManagerContractApproved, contract,
                    $@"[{contract.ProjectManagerOrganizationName} : Project Management] A new contract has been approved");

                AddContractNotification(context, "Contract was approved", contract);


            }

        }

        public void Handle(ContractApprovedEvent evt)
        {
            _logger.LogInformation(GetLogMessage("Contract Id: {0}"), evt.ContractId);
            
            Parallel.Invoke(new List<Action>
            {
                () => ContractApprovedSendContractorEmail(evt.ContractId),
                () => ContractApprovedSendAgencyOwnerEmail(evt.ContractId),
                () => ContractApprovedSendProjectManagerEmail(evt.ContractId),
                () => ContractApprovedSendAccountManagerEmail(evt.ContractId)
            }.ToArray());
        }
    }
}