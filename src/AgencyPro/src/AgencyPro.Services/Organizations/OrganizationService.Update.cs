// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.Organizations
{
    public partial class OrganizationService
    {
        private Task<OrganizationResult> UpdateProviderOrganization(ProviderOrganization providerOrganization, ProviderOrganizationInput input, Organization organization)
        {
            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), providerOrganization.Id, input);

            providerOrganization.DefaultAccountManagerId = input.DefaultAccountManagerId;
            providerOrganization.DefaultProjectManagerId = input.DefaultProjectManagerId;
            providerOrganization.DefaultContractorId = input.DefaultContractorId;
            providerOrganization.Discoverable = input.Discoverable;
            providerOrganization.InjectFrom(input);
            providerOrganization.Updated = DateTimeOffset.UtcNow;
            providerOrganization.ObjectState = ObjectState.Modified;

            organization.ProviderOrganization = providerOrganization;
            var result = Repository.InsertOrUpdateGraph(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), result);

            return Task.FromResult(new OrganizationResult()
            {
                OrganizationId = organization?.Id,
                Succeeded = result > 0
            });
        }

        public async Task<OrganizationResult> UpdateOrganization(IAgencyOwner agencyOwner, OrganizationUpdateInput input)
        {
            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), agencyOwner.OrganizationId, input);


            var organization = await Repository.Queryable()

                .Include(x => x.ProviderOrganization)
                .Include(x => x.Contractors)
                .Include(x => x.ProjectManagers)
                .Include(x => x.AccountManagers)

                .Include(x => x.MarketingOrganization)
                .Include(x => x.Marketers)

                .Include(x => x.RecruitingOrganization)
                .Include(x => x.Recruiters)
                .Where(x => x.Id == agencyOwner.OrganizationId)
                .FirstAsync();

            return await Update(organization, input);
        }

        public async Task<OrganizationResult> UpdateOrganization(IMarketingAgencyOwner agencyOwner, MarketingOrganizationInput input)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), agencyOwner.OrganizationId, input);

            var marketingOrganization = await Repository.Queryable()

                .Include(x => x.MarketingOrganization)
                .Include(x => x.Marketers)

                .Where(x => x.Id == agencyOwner.OrganizationId)
                .Select(x => x.MarketingOrganization)
                .FirstAsync();

            var organization = await Repository.Queryable()
             .Where(x => x.Id == agencyOwner.OrganizationId)
             .FirstOrDefaultAsync();

            return await UpdateMarketingOrganization(marketingOrganization, input, organization);
        }

        public async Task<OrganizationResult> UpdateOrganization(IProviderAgencyOwner agencyOwner, ProviderOrganizationInput input)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), agencyOwner.OrganizationId, input);

            var providerOrganization = await Repository.Queryable()

                .Include(x => x.ProviderOrganization)
                .Include(x => x.Contractors)
                .Include(x => x.ProjectManagers)
                .Include(x => x.AccountManagers)

                .Where(x => x.Id == agencyOwner.OrganizationId)
                .Select(x => x.ProviderOrganization)
                .FirstAsync();

            var organization = await Repository.Queryable()
              .Where(x => x.Id == agencyOwner.OrganizationId)
              .FirstOrDefaultAsync();

            return await UpdateProviderOrganization(providerOrganization, input, organization);
        }

        public async Task<OrganizationResult> UpdateOrganization(IRecruitingAgencyOwner agencyOwner, RecruitingOrganizationInput input)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), agencyOwner.OrganizationId, input);

            var recruitingOrganization = await Repository.Queryable()

                .Include(x => x.RecruitingOrganization)
                .Include(x => x.Recruiters)

                .Where(x => x.Id == agencyOwner.OrganizationId)
                .Select(x => x.RecruitingOrganization)
                .FirstAsync();

            var organization = await Repository.Queryable()
               .Where(x => x.Id == agencyOwner.OrganizationId)
               .FirstOrDefaultAsync();

            return await UpdateRecruitingOrganization(recruitingOrganization, input, organization);
        }

        public async Task<OrganizationResult> UpdateBuyerOrganization(IOrganizationCustomer cu, OrganizationUpdateInput input)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), cu.OrganizationId, input);

            var org = Repository.Select(x => x.Id == cu.OrganizationId).First();

            org.InjectFrom(input);

            return await Update(org, input);
        }

        public Task<OrganizationResult> UpdateOrganizationPic(IAgencyOwner agencyOwner, IFormFile image)
        {
            _logger.LogInformation(GetLogMessage("Updating as agency owner: {0}"), agencyOwner.OrganizationId);

            return UpdateOrganizationPic(agencyOwner.OrganizationId, image);
        }

        public Task<OrganizationResult> UpdateOrganizationPic(IOrganizationCustomer cu, IFormFile image)
        {
            _logger.LogInformation(GetLogMessage("Updating as customer: {0}"), cu.OrganizationId);

            return UpdateOrganizationPic(cu.OrganizationId, image);
        }

        private async Task<OrganizationResult> UpdateOrganizationPic(Guid organizationId, IFormFile image)
        {
            var organization = await Repository.Queryable()
                .FirstOrDefaultAsync(x => x.Id == organizationId);
            
            _logger.LogInformation(GetLogMessage("Organization: {0}"), organization.Id);

            organization.ImageUrl =
                await _storageService.StorePngImageAtPath(image, "organizations", organizationId.ToString());
            organization.Updated = DateTimeOffset.UtcNow;
            organization.ObjectState = ObjectState.Modified;
            organization.UpdatedById = _userInfo.Value.UserId;

            var result = await Repository.UpdateAsync(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), result);

            return await Task.FromResult(new OrganizationResult
            {
                OrganizationId = organization.Id,
                Succeeded = result > 0
            });
        }

        private Task<OrganizationResult> Update(Organization organization, OrganizationUpdateInput input)
        {
            _logger.LogInformation(GetLogMessage("Organization: {0}"), organization.Id);

            organization.Iso2 = input.Iso2;
            organization.ProvinceState = input.ProvinceState;
            organization.Updated = DateTimeOffset.UtcNow;
            organization.ObjectState = ObjectState.Modified;
            organization.UpdatedById = _userInfo.Value.UserId;

            organization.InjectFrom(input);

            var result = Repository.InsertOrUpdateGraph(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), result);

            return Task.FromResult(new OrganizationResult()
            {
                OrganizationId = organization.Id,
                Succeeded = result > 0
            });
        }

        public Task<OrganizationResult> UpdateRecruitingOrganization(RecruitingOrganization recruitingOrganization, RecruitingOrganizationInput input, Organization organization)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), recruitingOrganization.Id, input);

            recruitingOrganization.InjectFrom(input);

            recruitingOrganization.Updated = DateTimeOffset.UtcNow;

            recruitingOrganization.ObjectState = ObjectState.Modified;

            recruitingOrganization.Discoverable = input.Discoverable;
            recruitingOrganization.RecruiterStream = input.RecruiterStream;
            recruitingOrganization.RecruiterBonus = input.RecruiterBonus;
            recruitingOrganization.RecruitingAgencyStream = input.RecruitingAgencyStream;
            recruitingOrganization.RecruitingAgencyBonus = input.RecruitingAgencyBonus;

            organization.RecruitingOrganization = recruitingOrganization;

            var result = Repository.InsertOrUpdateGraph(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), result);

            return Task.FromResult(new OrganizationResult()
            {
                OrganizationId = organization.Id,
                Succeeded = result > 0
            });
        }

        public Task<OrganizationResult> UpdateMarketingOrganization(MarketingOrganization marketingOrganization, MarketingOrganizationInput input, Organization organization)
        {

            _logger.LogInformation(GetLogMessage("Organization:{0}; input:{@input}"), marketingOrganization.Id, input);

            marketingOrganization.InjectFrom(input);

            marketingOrganization.Updated = DateTimeOffset.UtcNow;
            marketingOrganization.ObjectState = ObjectState.Modified;
            marketingOrganization.Discoverable = input.Discoverable;

            marketingOrganization.MarketerStream = input.MarketerStream;
            marketingOrganization.MarketerBonus = input.MarketerBonus;
            marketingOrganization.MarketingAgencyStream = input.MarketingAgencyStream;
            marketingOrganization.MarketingAgencyBonus = input.MarketingAgencyBonus;

            organization.MarketingOrganization = marketingOrganization;

            var result = Repository.InsertOrUpdateGraph(organization, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), result);

            return Task.FromResult(new OrganizationResult()
            {
                OrganizationId = organization.Id,
                Succeeded = result > 0
            });
        }
    }
}