// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Contracts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationCustomers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.Events;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Projects.Enums;

namespace AgencyPro.Services.Contracts
{
    public partial class ContractService
    {
        public Task<ContractResult> CreateContract(
            IOrganizationAccountManager am,
            ContractInput model)
        {
            _logger.LogInformation(GetLogMessage("Creating contract as AM"));
            model.ContractorOrganizationId = am.OrganizationId;
            return CreateContract(model);
        }

        public Task<ContractResult> CreateContract(IProviderAgencyOwner ao, ContractInput model)
        {
            _logger.LogInformation(GetLogMessage("Creating contract as PAO"));
            model.ContractorOrganizationId = ao.OrganizationId;
            return CreateContract(model);
        }

        private async Task<ContractResult> CreateContract(ContractInput input)
        {
            _logger.LogInformation(
                GetLogMessage(
                    $@"Creating Contract For Contractor: {input.ContractorId} in Project: {input.ProjectId}"));

            var retVal = new ContractResult
            {
            };
            
            var project = await _projectRepository.Queryable()
                .Include(x => x.CustomerAccount)
                .ThenInclude(x=>x.OrganizationCustomer)
                .Include(x=>x.Customer)
                .ThenInclude(x=>x.OrganizationMarketer)
                .Include(x=>x.OrganizationProjectManager)
                .Where(x => x.Id == input.ProjectId && x.ProjectManagerOrganizationId == input.ContractorOrganizationId)
                .FirstAsync();

            if (project.Status == ProjectStatus.Paused || project.Status == ProjectStatus.Ended)
            {
                retVal.ErrorMessage = "Contracts cannot be added to inactive projects";
                return retVal;
            }

            var orgCustomer = _cuService.GetCustomerForProject<OrganizationCustomerOutput>(input.ProjectId);

            var providerOrganization = _organizationService.Repository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Where(x => x.Id == input.ContractorOrganizationId).FirstAsync();

            var orgAccountManager =
                _amService.GetAccountManagerForProject<OrganizationAccountManagerOutput>(input.ProjectId);
            
            var orgMarketer = project.Customer.OrganizationMarketer;

            var orgContractor = _coService.Repository.Queryable()
                .Include(x=>x.Contractor)
                .ThenInclude(x=>x.OrganizationRecruiter)
                .ThenInclude(x=>x.Organization)
                .ThenInclude(x=>x.RecruitingOrganization)
                .Include(x => x.Contractor)
                .ThenInclude(x => x.OrganizationRecruiter)
                .ThenInclude(x => x.Recruiter)
                .Where(x => x.ContractorId == input.ContractorId && x.OrganizationId == input.ContractorOrganizationId)
                .FirstAsync();

            var recruiterStream = orgContractor.Result.Contractor.OrganizationRecruiter.RecruiterStream;

            await Task.WhenAll(
                orgCustomer,
                providerOrganization,
                orgAccountManager,
                orgContractor);
            
            var nextProviderContractId = await GetNextProviderContractId
                (input.ContractorOrganizationId);

            var nextMarketingContractId = await GetNextMarketingContractId
                (project.CustomerAccount.Customer.MarketerOrganizationId);

            var nextBuyerContractId = await GetNextBuyerContractId
                (orgCustomer.Result.OrganizationId);
            
            var recruitingAgencyStream = orgContractor.Result.Contractor
                .OrganizationRecruiter.Organization.RecruitingOrganization.RecruitingAgencyStream;
            
            var recruitingAgreement = await _recruitingAgreements.Queryable()
                .Where(x => x.RecruitingOrganizationId == orgContractor.Result.Contractor.RecruiterOrganizationId &&
                            x.ProviderOrganizationId == project.ProjectManagerOrganizationId && x.Status == AgreementStatus.Approved)
                .FirstOrDefaultAsync();

            if (recruitingAgreement != null)
            {
                _logger.LogDebug(
                    GetLogMessage("Recruiter agreement found, using agreement values for RE and RAO streams"));

                recruiterStream = recruitingAgreement.RecruiterStream;
                recruitingAgencyStream = recruitingAgreement.RecruitingAgencyStream;
            }
            else
            {
                _logger.LogDebug(GetLogMessage(
                    "Recruiter agreement not found, using default values from recruiting org settings"));
            }

            if (orgContractor.Result.Contractor.RecruiterOrganizationId == providerOrganization.Result.Id)
            {
                _logger.LogDebug(GetLogMessage(
                    "Recruiter organization is same as provider organization, setting RAO stream to 0"));
                recruitingAgencyStream = 0;
            }

            

            var contract = new Contract()
                .InjectFrom(input) as Contract;


            // init()
            contract.ProviderNumber = nextProviderContractId;
            contract.BuyerNumber = nextBuyerContractId;
            contract.RecruitingNumber = await GetNextRecruitingContractId
                (orgContractor.Result.Contractor.RecruiterOrganizationId);
            contract.MarketingNumber = nextMarketingContractId;

            // figure out implicit relationships
            contract.RecruiterOrganizationId = orgContractor.Result.Contractor.RecruiterOrganizationId;
            contract.RecruiterId = orgContractor.Result.Contractor.RecruiterId;
            contract.MarketerId = orgMarketer.MarketerId;
            contract.MarketerOrganizationId = orgMarketer.OrganizationId;
            contract.AccountManagerOrganizationId = orgAccountManager.Result.OrganizationId;
            contract.ProjectManagerOrganizationId = project.ProjectManagerOrganizationId;
            contract.ContractorOrganizationId = orgContractor.Result.OrganizationId;
            contract.CustomerId = project.CustomerId;
            contract.BuyerOrganizationId = project.CustomerOrganizationId;


            var agencyStream = providerOrganization.Result.ProviderOrganization.AgencyStream;
            var accountManagerStream = orgAccountManager.Result.AccountManagerStream;
            var projectManagerStream = project.OrganizationProjectManager.ProjectManagerStream;
            var marketingAgencyStream = project.CustomerAccount.MarketingAgencyStream;
            

            switch (project.Status)
            {
                case ProjectStatus.Pending:
                    contract.Status = ContractStatus.Pending;
                    break;

                case ProjectStatus.Active:
                    contract.Status = ContractStatus.Active;
                    break;
            }

            //if (project.CustomerAccount.IsInternal)
            //{
            //    contract.Status = ContractStatus.Active;
            //    agencyStream = 0;

            //    if (project.CustomerAccount.IsCorporate)
            //    {
            //        recruitingAgencyStream = 0;
            //        accountManagerStream = 0;
            //        projectManagerStream = 0;
            //        marketingAgencyStream = 0;
            //    }
            //}
            
            // determine streams
            contract.AgencyStream = agencyStream;
            contract.RecruitingAgencyStream = recruitingAgencyStream;
            contract.MarketingAgencyStream = marketingAgencyStream;
            contract.RecruiterStream = recruiterStream;
            contract.MarketerStream = project.CustomerAccount.MarketerStream;

            contract.AccountManagerStream = accountManagerStream;
            contract.ProjectManagerStream = projectManagerStream;
            contract.ContractorStream = orgContractor.Result.ContractorStream;
            contract.SystemStream = providerOrganization.Result.ProviderOrganization.SystemStream;

            contract.CreatedById = _userInfo.UserId;
            contract.UpdatedById = _userInfo.UserId;

            contract.AccountManagerId = orgAccountManager.Result.AccountManagerId;
            contract.ProjectManagerId = project.ProjectManagerId;
            contract.ObjectState = ObjectState.Added;

            contract.StatusTransitions.Add(new ContractStatusTransition()
            {
                Status = contract.Status,
                ObjectState = ObjectState.Added
            });

            var result = Repository.Insert(contract, true);

            _logger.LogDebug(GetLogMessage("{0} Contract Records updated in database"), result);

            if (result > 0)
            {
                retVal.ContractId = contract.Id;
                retVal.Succeeded = true;

                await Task.Run(() => RaiseEvent(new ContractCreatedEvent()
                {
                    ContractId = contract.Id
                }));
            }

            return retVal;
        }
    }
}