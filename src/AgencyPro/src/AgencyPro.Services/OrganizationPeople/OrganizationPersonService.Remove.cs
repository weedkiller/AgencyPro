// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.People.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;

namespace AgencyPro.Services.OrganizationPeople
{

    public partial class OrganizationPersonService
    {
        private async Task<OrganizationPersonResult> Remove(Guid person, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage($@"For Person: {person}, For Organization: {organizationId}"));

            var retVal = new OrganizationPersonResult()
            {
                PersonId = person,
                OrganizationId = organizationId
            };

            var orgPerson = Repository.Queryable()
                .Include(x => x.AccountManager)
                .ThenInclude(x => x.Accounts)
                .Include(x => x.AccountManager)
                .ThenInclude(x => x.Leads)
                .Include(x => x.Recruiter)
                .ThenInclude(x => x.Candidates)
                .Include(x => x.Recruiter)
                .ThenInclude(x => x.Contractors)
                .Include(x => x.Marketer)
                .ThenInclude(x => x.Leads)
                .Include(x => x.Marketer)
                .ThenInclude(x => x.Customers)
                .Include(x => x.ProjectManager)
                .ThenInclude(x => x.Projects)
                .Include(x => x.ProjectManager)
                .ThenInclude(x => x.Candidates)
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Contracts)
                .FirstOrDefault(x => x.PersonId == person && x.OrganizationId == organizationId);

            if (orgPerson == null)
            {
                retVal.Succeeded = false;
                return retVal;

            }

            var organization = await _orgService.Repository.Queryable()
                .Include(x=>x.MarketingOrganization)
                .Include(x=>x.RecruitingOrganization)
                .Include(x=>x.ProviderOrganization)
                .Include(x=>x.AccountManagers)
                .Include(x=>x.ProjectManagers)
                .Include(x=>x.Contractors)
                .Include(x=>x.Recruiters)
                .Include(x=>x.Marketers)
                .Where(x=>x.Id == organizationId)
                .FirstOrDefaultAsync();

            var totalUpdated = 0;
            var keepPerson = false;

            if (orgPerson.Contractor != null)
            {
                if (orgPerson.Contractor.Contracts.Count > 0)
                {
                    var activeContracts = orgPerson.Contractor.Contracts.Where(x => x.IsPaused == false && x.IsEnded == false);

                    if (!activeContracts.Any())
                    {
                        orgPerson.Contractor.IsDeleted = true;
                        orgPerson.Contractor.UpdatedById = _userInfo.UserId;
                        orgPerson.Contractor.Updated = DateTimeOffset.UtcNow;
                        orgPerson.Contractor.ObjectState = ObjectState.Modified;
                    }
                    else
                    {
                        retVal.ErrorMessage = "Cannot remove person that has active contracts";
                        return retVal;
                    }
                }
                // how many contracts do they have?


            }

            if (orgPerson.AccountManager != null)
            {
                if (!orgPerson.AccountManager.DefaultOrganizations.Any())
                {
                    if (orgPerson.AccountManager.Accounts.Count > 0)
                    {
                        foreach (var account in orgPerson.AccountManager.Accounts)
                        {
                            account.AccountManagerId = organization.ProviderOrganization.DefaultAccountManagerId;
                            account.ObjectState = ObjectState.Modified;
                            account.Updated = DateTimeOffset.UtcNow;
                            account.UpdatedById = _userInfo.UserId;
                        }

                    }
                    if (orgPerson.AccountManager.Leads.Count > 0)
                    {
                        foreach (var lead in orgPerson.AccountManager.Leads)
                        {
                            lead.AccountManagerId = organization.ProviderOrganization.DefaultAccountManagerId;
                            lead.Updated = DateTimeOffset.UtcNow;
                            lead.UpdatedById = _userInfo.UserId;
                            lead.ObjectState = ObjectState.Modified;
                        }

                    }

                    orgPerson.AccountManager.IsDeleted = true;
                    orgPerson.AccountManager.Updated = DateTimeOffset.UtcNow;
                    orgPerson.AccountManager.UpdatedById = _userInfo.UserId;
                    orgPerson.AccountManager.ObjectState = ObjectState.Modified;

                }

            }

            if (orgPerson.ProjectManager != null)
            {
                // how many projects do they have?
                if (orgPerson.ProjectManager.Projects.Count > 0)
                {
                    foreach (var project in orgPerson.ProjectManager.Projects)
                    {
                        project.ProjectManagerId = organization.ProviderOrganization.DefaultProjectManagerId;
                        project.ObjectState = ObjectState.Modified;
                        project.Updated = DateTimeOffset.UtcNow;
                        project.UpdatedById = _userInfo.UserId;
                    }
                }

                if (orgPerson.ProjectManager.Candidates.Count > 0)
                {
                    foreach (var candidate in orgPerson.ProjectManager.Candidates)
                    {
                        candidate.ProjectManagerId = organization.ProviderOrganization.DefaultProjectManagerId;
                        candidate.ObjectState = ObjectState.Modified;
                        candidate.Updated = DateTimeOffset.UtcNow;
                        candidate.UpdatedById = _userInfo.UserId;
                    }
                }

                orgPerson.ProjectManager.IsDeleted = true;
                orgPerson.ProjectManager.Updated = DateTimeOffset.UtcNow;
                orgPerson.ProjectManager.UpdatedById = _userInfo.UserId;
                orgPerson.ProjectManager.ObjectState = ObjectState.Modified;
            }

            if (orgPerson.Customer != null)
            {
                // you cannot delete the customer record without deactivating the account
            }

            if (orgPerson.Recruiter != null)
            {
                if (orgPerson.Recruiter.Candidates.Count > 0)
                {
                    foreach (var candidate in orgPerson.Recruiter.Candidates)
                    {
                        candidate.RecruiterId = organization.RecruitingOrganization.DefaultRecruiterId;
                        candidate.UpdatedById = _userInfo.UserId;
                        candidate.Updated = DateTimeOffset.UtcNow;
                        candidate.ObjectState = ObjectState.Modified;
                    }
                }

                // any active candidates?
                orgPerson.Recruiter.IsDeleted = true;
                orgPerson.Recruiter.ObjectState = ObjectState.Modified;
                orgPerson.UpdatedById = _userInfo.UserId;
                orgPerson.Updated = DateTimeOffset.UtcNow;
            }

            if (orgPerson.Marketer != null)
            {
                // any active leads?
                orgPerson.Marketer.IsDeleted = true;
                orgPerson.Marketer.ObjectState = ObjectState.Modified;
                orgPerson.UpdatedById = _userInfo.UserId;
                orgPerson.Updated = DateTimeOffset.UtcNow;

                if (orgPerson.Marketer.Leads.Any())
                {
                    foreach (var lead in orgPerson.Marketer.Leads)
                    {
                        lead.MarketerId = organization.MarketingOrganization.DefaultMarketerId;
                        lead.ObjectState = ObjectState.Modified;
                        lead.UpdatedById = _userInfo.UserId;
                        lead.Updated = DateTimeOffset.UtcNow;
                    }
                }

                if (orgPerson.Marketer.Customers.Any())
                {
                    foreach (var customer in orgPerson.Marketer.Customers)
                    {
                        // deal with these customers
                        customer.MarketerId = organization.RecruitingOrganization.DefaultRecruiterId;
                        customer.ObjectState = ObjectState.Modified;
                        customer.Updated = DateTimeOffset.UtcNow;
                    }
                }
            }

            orgPerson.IsDeleted = !keepPerson;
            orgPerson.Status = PersonStatus.Inactive;
            orgPerson.UpdatedById = _userInfo.UserId;
            orgPerson.ObjectState = ObjectState.Modified;

            totalUpdated = Repository.InsertOrUpdateGraph(orgPerson, true);

            if (totalUpdated > 0)
            {
                retVal.Succeeded = true;
               
            }

            return retVal;
        }


        public Task<OrganizationPersonResult> Remove(IAgencyOwner ao, Guid personId)
        {
            return Remove(personId, ao.OrganizationId);
        }
    }
}