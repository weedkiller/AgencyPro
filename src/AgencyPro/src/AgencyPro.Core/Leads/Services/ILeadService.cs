// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Leads.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Leads.Services
{
    public interface ILeadService : IService<Lead>
    {
        Task<int> MatchingLeadsByEmail(string email);
        Task<int> CountLeadsPerProviderByEmail(Guid providerOrganizationId, string email);

        Task<LeadResult> QualifyLead(IProviderAgencyOwner ao, Guid leadId, LeadQualifyInput input);

        Task<PromoteLeadResult> PromoteLead(IOrganizationAccountManager am, Guid leadId, PromoteLeadOptions options);

        Task<LeadResult> RejectLead(IOrganizationAccountManager am, Guid leadId, LeadRejectInput input);

        Task<PackedList<T>> GetLeads<T>(IOrganizationMarketer ma, CommonFilters filters) where T : MarketerLeadOutput;
        Task<PackedList<T>> GetLeads<T>(IProviderAgencyOwner ao, CommonFilters filters) where T : AgencyOwnerLeadOutput;
        Task<PackedList<T>> GetLeads<T>(IMarketingAgencyOwner ao, CommonFilters filters) where T : AgencyOwnerLeadOutput;
        Task<PackedList<T>> GetLeads<T>(IOrganizationAccountManager am, CommonFilters filters) where T : AccountManagerLeadOutput;

        Task<T> GetAsync<T>(Guid leadId) where T : LeadOutput;
        Task<Lead> GetAsync(Guid leadId);

        Task<T> GetLead<T>(IOrganizationMarketer ma, Guid leadId) where T : MarketerLeadOutput;

        Task<T> GetLead<T>(IOrganizationAccountManager organizationAccountManager, Guid leadId)
            where T : AccountManagerLeadOutput;

        Task<T> GetLead<T>(IProviderAgencyOwner agencyOwner, Guid leadId) where T : AgencyOwnerLeadOutput;
        Task<T> GetLead<T>(IMarketingAgencyOwner agencyOwner, Guid leadId) where T : AgencyOwnerLeadOutput;

        Task<LeadResult> UpdateLead(IProviderAgencyOwner agencyOwner, Guid leadId, LeadInput model);

        Task<LeadResult> UpdateLead(IOrganizationAccountManager am, Guid leadId, LeadInput model);

        Task<LeadResult> UpdateLead(IOrganizationMarketer ma, Guid leadId, LeadInput model);

        Task<LeadResult> DeleteLead(IProviderAgencyOwner agencyOwner, Guid leadId);
        Task<LeadResult> DeleteLead(IOrganizationMarketer ma, Guid leadId);
        Task<LeadResult> DeleteLead(IOrganizationAccountManager am, Guid leadId);

        Task<LeadResult> RejectLead(IProviderAgencyOwner agencyOwner, Guid leadId, LeadRejectInput input);

        Task<LeadResult> CreateInternalLead(IOrganizationMarketer organizationMarketer, LeadInput model);
        Task<LeadResult> CreateExternalLead(IOrganizationMarketer organizationMarketer, Guid providerOrganizationId, LeadInput model);
        Task<LeadResult> CreateInternalLead(Guid organizationId, LeadInput model);
    }
}