// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Events;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;

namespace AgencyPro.Services.Leads
{
    public partial class LeadService
    {
        public Task<LeadResult> CreateInternalLead(
            IOrganizationMarketer ma,
            LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("MA: {0}"), ma.OrganizationId);

            return CreateLead(ma, ma.OrganizationId, model);
        }

        private async Task<LeadResult> CreateLead(IOrganizationMarketer ma, Guid providerOrganizationId, LeadInput input)
        {
            _logger.LogInformation(GetLogMessage("MA: {0}"), ma.OrganizationId);
            
            var retVal = new LeadResult();

            if (await CountLeadsPerProviderByEmail(providerOrganizationId, input.EmailAddress) > 0)
            {
                retVal.ErrorMessage = "Email has already been used as a lead for this organization";
                retVal.Succeeded = false;

                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Email hasn't been used by this organization"));

            var isExternal = providerOrganizationId != ma.OrganizationId;

            _logger.LogDebug(GetLogMessage("External Lead: {0}"), isExternal);

            var marketer = await _marketerService.Repository.Queryable()
                .Include(x=>x.Marketer)
                .ThenInclude(x=>x.Person)
                .Where(x=>x.MarketerId == ma.MarketerId && x.OrganizationId == ma.OrganizationId)
                .FirstAsync();

            var marketerBonus = marketer.MarketerBonus;
            var marketerAgencyBonus = 0m;
            var marketerAgencyStream = 0m;
            var marketerStream = marketer.MarketerStream;


            _logger.LogTrace(
                GetLogMessage(
                    $@"Marketer Found: {marketer.MarketerId}"));
            
            if (isExternal)
            {
                var marketingAgreement = await _marketingAgreements.Queryable()
                    .Where(x => x.ProviderOrganizationId == providerOrganizationId &&
                                x.MarketingOrganizationId == ma.OrganizationId)
                    .FirstOrDefaultAsync();


                if (marketingAgreement == null)
                {
                    retVal.ErrorMessage =
                        "Marketing agreement doesn't exist between marketing and provider organization";
                    return retVal;
                }
                

                if (marketingAgreement.Status != AgreementStatus.Approved)
                {
                    retVal.ErrorMessage = "Marketing agreement is not approved";
                    return retVal;
                }

                _logger.LogDebug(
                    GetLogMessage(
                        $@"Marketing Agreement found to be valid"));

                marketerBonus = marketingAgreement.MarketerBonus;
                marketerAgencyBonus = marketingAgreement.MarketingAgencyBonus;
                marketerAgencyStream = marketingAgreement.MarketingAgencyStream;
                marketerStream = marketingAgreement.MarketerStream;
                
            }

        
            var lead = new Lead
            {
                Iso2 = input.Iso2,
                ProvinceState = input.ProvinceState,
                MarketerId = marketer.MarketerId,
                MarketerOrganizationId = marketer.OrganizationId,
                MarketerStream = marketerStream,
                MarketerBonus =marketerBonus,
                MarketingAgencyBonus = marketerAgencyBonus,
                MarketingAgencyStream = marketerAgencyStream,
                ProviderOrganizationId = providerOrganizationId,
                UpdatedById = marketer.MarketerId,
                CreatedById = marketer.MarketerId, // cant be _userInfo.UserId
                ObjectState = ObjectState.Added,
                Status = LeadStatus.New
            };

            lead.StatusTransitions.Add(new LeadStatusTransition()
            {
                Status = LeadStatus.New,
                ObjectState = ObjectState.Added
            });

            lead.InjectFrom(input);

            var records = Repository.Insert(lead, true);

            _logger.LogDebug(GetLogMessage("{0} records updated in db"), records);
            
            if (records > 0)
            {
                retVal.LeadId = lead.Id;
                retVal.Succeeded = true;

                await Task.Run(() => RaiseEvent(new LeadCreatedEvent
                {
                    LeadId = lead.Id
                }));
            }

            return retVal;
        }

        public async Task<LeadResult> CreateExternalLead(IOrganizationMarketer ma, Guid providerOrganizationId, LeadInput model)
        {
            _logger.LogInformation(GetLogMessage("MA: {0}"), ma.OrganizationId);
            return await CreateLead(ma, providerOrganizationId, model);
        }

        // this call is unique in that it doesn't accept a principal and still returns a concrete output type
        public async Task<LeadResult> CreateInternalLead(Guid organizationId, LeadInput model)
        {
            _logger.LogInformation(
                GetLogMessage("Org: {0}"), organizationId);

            var organizationMarketer =
               await _marketerService.GetMarketerOrDefault<OrganizationMarketerOutput>(organizationId, null,
                    model.ReferralCode);

            return await CreateInternalLead(organizationMarketer, model);
        }

    }
}