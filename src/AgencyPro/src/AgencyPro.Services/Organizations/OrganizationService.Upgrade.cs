// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.ViewModels;
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
        private const decimal SystemStream = 5m;

        public async Task<OrganizationResult> UpgradeOrganization(IOrganizationCustomer organizationCustomer,
            OrganizationUpgradeInput input)
        {
            _logger.LogInformation(GetLogMessage("Upgrade Organization Id: {input}"), organizationCustomer.OrganizationId);

            var retVal = new OrganizationResult()
            {
                OrganizationId = organizationCustomer.OrganizationId
            };

            var organization = await Repository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Include(x => x.MarketingOrganization)
                .Include(x => x.RecruitingOrganization)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Where(x => x.Id == organizationCustomer.OrganizationId)
                .FirstAsync();

            _logger.LogDebug(GetLogMessage("Organization Found: {Organization}"), organization.OrganizationType.ToString());

            var upgradeProviderOrg = (input.ProviderOrganizationInput != null);
            var upgradeMarketingOrg = (input.MarketingOrganizationInput != null);
            var upgradeRecruitingOrg = (input.RecruitingOrganizationInput != null);

            _logger.LogDebug(GetLogMessage("Upgrade Provider Org: {Provider}; Upgrade Marketing Org: {Marketing}; Upgrade Recruiting Org {Recruiting}"),
                upgradeProviderOrg,
                upgradeMarketingOrg,
                upgradeRecruitingOrg);

            var orgPerson = organization
                .OrganizationPeople
                .First(x => x.PersonId == organizationCustomer.CustomerId);

            _logger.LogDebug(GetLogMessage("Person: {PersonId}"), orgPerson.PersonId);


            orgPerson.ObjectState = ObjectState.Modified;

            organization.CategoryId = input.CategoryId;
            organization.UpdatedById = _userInfo.Value.UserId;
            organization.ObjectState = ObjectState.Modified;
            organization.Updated = DateTimeOffset.UtcNow;

            if (upgradeProviderOrg)
            {
                if (organization.ProviderOrganization != null)
                {
                    retVal.ErrorMessage = "Provider organization is already upgraded";
                    return retVal;
                }

                _logger.LogInformation(GetLogMessage("Upgrading to Provider Organization..."));
                
                var upgradeResult = UpgradeToProviderOrganization(input.ProviderOrganizationInput, organization, orgPerson, true);

                if (!upgradeMarketingOrg && organization.MarketingOrganization == null)
                {
                    // add so we have a default marketer in the org
                    orgPerson.Marketer = new OrganizationMarketer()
                    {
                        Updated = DateTimeOffset.UtcNow,
                        Created = DateTimeOffset.UtcNow,
                        MarketerId = orgPerson.PersonId,
                        OrganizationId = orgPerson.OrganizationId,
                        MarketerStream = 0,
                        MarketerBonus = 0,
                        ObjectState = ObjectState.Added
                    };
                }

                if (!upgradeRecruitingOrg && organization.RecruitingOrganization == null)
                {
                    // add so we have a default recruiter
                    orgPerson.Recruiter = new OrganizationRecruiter()
                    {
                        Updated = DateTimeOffset.UtcNow,
                        Created = DateTimeOffset.UtcNow,
                        RecruiterId = orgPerson.PersonId,
                        OrganizationId = orgPerson.OrganizationId,
                        RecruiterStream = 0,
                        ObjectState = ObjectState.Added,
                    };
                }
            }


            if (upgradeMarketingOrg)
            {
                if (organization.MarketingOrganization != null)
                    throw new ApplicationException("Marketing organization is already upgraded");

                _logger.LogDebug(GetLogMessage("Upgrading to Marketing Organization..."));

                var newMarketingOrgInput = new MarketingOrganizationInput()
                {
                    DefaultMarketerId = orgPerson.PersonId
                }.InjectFrom(input.MarketingOrganizationInput) as MarketingOrganizationInput;


                retVal = UpgradeToMarketingOrganization(newMarketingOrgInput, organization, orgPerson, true);
            }

            if (upgradeRecruitingOrg)
            {
                if (organization.RecruitingOrganization != null)
                {
                    retVal.ErrorMessage = "Recruiting organization is already upgraded";
                    return retVal;
                }

                _logger.LogDebug(GetLogMessage("Upgrading to Recruiting Organization..."));

                retVal = UpgradeToRecruitingOrganization(input.RecruitingOrganizationInput, organization, orgPerson, true);

            }

            return retVal;
        }

        private OrganizationResult UpgradeToRecruitingOrganization(RecruitingOrganizationUpgradeInput input,
            Organization organization, OrganizationPerson person, bool saveChanges = true)
        {
            _logger.LogInformation(GetLogMessage("{0}"), organization.Name);
            
            if (person.Recruiter == null)
            {
                _logger.LogDebug(GetLogMessage("Person is not a recruiter"));

                person.Recruiter = new OrganizationRecruiter()
                {
                    RecruiterId = person.PersonId,
                    OrganizationId = person.OrganizationId,
                    RecruiterStream = input.RecruiterStream,
                    ObjectState = ObjectState.Added,
                };
                person.ObjectState = ObjectState.Modified;

                var recordsUpdated = _organizationPersonRepo.InsertOrUpdateGraph(person, true);

                _logger.LogDebug(GetLogMessage("OrganizationPeople Records Updated: {0}"), recordsUpdated);

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Person is already a recruiter"));
            }


            organization.RecruitingOrganization = new RecruitingOrganization()
            {
                RecruiterStream = input.RecruiterStream,
                RecruitingAgencyStream = input.RecruitingAgencyStream,
                DefaultRecruiterId = person.PersonId,
                Updated = DateTimeOffset.UtcNow,
                Created = DateTimeOffset.UtcNow,
                ObjectState = ObjectState.Added,
            }.InjectFrom(input) as RecruitingOrganization;
            organization.ObjectState = ObjectState.Modified;
            organization.OrganizationType = organization.OrganizationType.Add(OrganizationType.Recruiting);

            var result = Repository.InsertOrUpdateGraph(organization, saveChanges);

            _logger.LogDebug(GetLogMessage("Pushing subscription to stripe..."));

            var result2 = _subscriptionService.PushSubscription(organization.Id, saveChanges).Result;

            _logger.LogDebug(GetLogMessage("{OrganizationRecordsChanged}, {StripeRecordsChanged}"), result, result2);


            return new OrganizationResult()
            {
                Succeeded = result > 0,
                OrganizationId = organization.Id
            };
        }

        private OrganizationResult UpgradeToProviderOrganization(ProviderOrganizationUpgradeInput input,
            Organization organization, OrganizationPerson person, bool saveChanges = true)
        {
            _logger.LogInformation(GetLogMessage("{Organization}, Saving Changes: {saveChanges}"), organization.Name, saveChanges);

            var retVal = new OrganizationResult()
            {
                OrganizationId = organization.Id
            };

            person.AccountManager = new OrganizationAccountManager()
            {
                AccountManagerId = person.PersonId,
                OrganizationId = person.OrganizationId,
                AccountManagerStream = input.AccountManagerStream,
                ObjectState = ObjectState.Added
            };

            person.ProjectManager = new OrganizationProjectManager()
            {
                OrganizationId = person.OrganizationId,
                ProjectManagerId = person.PersonId,
                ProjectManagerStream = input.ProjectManagerStream,
                ObjectState = ObjectState.Added
            };

            person.Contractor = new OrganizationContractor()
            {
                ContractorId = person.PersonId,
                OrganizationId = person.OrganizationId,
                ContractorStream = input.ContractorStream,
                ObjectState = ObjectState.Added
            };
            person.ObjectState = ObjectState.Modified;

            var records = _organizationPersonRepo.InsertOrUpdateGraph(person, true);

            _logger.LogDebug(GetLogMessage("{0} Records updated"), records);

            if (records == 0)
            {
                retVal.Succeeded = false;
                retVal.ErrorMessage = "Unable to add user to provider roles";
                return retVal;
            }

            organization.ProviderOrganization = new ProviderOrganization()
            {
                ContractorStream = input.ContractorStream,
                ProjectManagerStream = input.ProjectManagerStream,
                AccountManagerStream = input.AccountManagerStream,
                AgencyStream = input.AgencyStream,
                Discoverable = input.Discoverable,
                ObjectState = ObjectState.Added,
                SystemStream = SystemStream,
                DefaultAccountManagerId = person.PersonId,
                DefaultContractorId = person.PersonId,
                DefaultProjectManagerId = person.PersonId
            }.InjectFrom(input) as ProviderOrganization;
            organization.ObjectState = ObjectState.Modified;
            organization.OrganizationType = organization.OrganizationType.Add(OrganizationType.Provider);

            _logger.LogDebug(GetLogMessage("new org type {0}"), organization.OrganizationType);

            var orgRecords = Repository.InsertOrUpdateGraph(organization, saveChanges);

            _logger.LogDebug(GetLogMessage("{0} Organization Records updated"), orgRecords);

            if (orgRecords == 0)
            {
                retVal.ErrorMessage = "Unable to update organization";
                return retVal;
            }

            var stripeResult = _subscriptionService.PushSubscription(organization.Id, saveChanges).Result;
            
            _logger.LogDebug(GetLogMessage("{StripeChanges} stripe records updated"), stripeResult);

            if (stripeResult > 0)
            {
                retVal.Succeeded = true;

            }

            return retVal;
        }

        private OrganizationResult UpgradeToMarketingOrganization(MarketingOrganizationUpgradeInput input,
            Organization organization, OrganizationPerson person, bool saveChanges = true)
        {
            _logger.LogInformation(GetLogMessage("{0}, {1}"), organization.Name, organization.OrganizationType.GetDescription());

            if (person.Marketer == null)
            {
                _logger.LogDebug(GetLogMessage("No Marketer Found"));

                person.Marketer = new OrganizationMarketer()
                {
                    MarketerId = person.PersonId,
                    OrganizationId = person.OrganizationId,
                    MarketerStream = input.MarketerStream,
                    MarketerBonus = input.MarketerBonus,
                    ObjectState = ObjectState.Added,
                };
                person.ObjectState = ObjectState.Modified;

                var recordsUpdated = _organizationPersonRepo.InsertOrUpdateGraph(person, true);

                _logger.LogDebug(GetLogMessage("OrganizationPeople Records Updated: {0}"), recordsUpdated);

            }
            else
            {
                _logger.LogDebug(GetLogMessage("Person is already a marketer"));
            }


            organization.MarketingOrganization = new MarketingOrganization()
            {
                MarketerStream = input.MarketerStream,
                MarketingAgencyBonus = input.MarketingAgencyBonus,
                MarketerBonus = input.MarketerBonus,
                MarketingAgencyStream = input.MarketingAgencyStream,
             
                DefaultMarketerId = person.PersonId,
                ObjectState = ObjectState.Added,
                ServiceFeePerLead = 1
            }.InjectFrom(input) as MarketingOrganization;
            organization.ObjectState = ObjectState.Modified;
            organization.OrganizationType = organization.OrganizationType.Add(OrganizationType.Marketing);

            _logger.LogDebug(GetLogMessage("new org type {0}"), organization.OrganizationType);

            var organizationResult = Repository.InsertOrUpdateGraph(organization, saveChanges);

            _logger.LogDebug(GetLogMessage("Organization Records Updated: {0}"), organizationResult);

            var subscriptionResult = _subscriptionService.PushSubscription(organization.Id, true).Result;

            _logger.LogDebug(GetLogMessage("Subscription Records Updated: {0}"), subscriptionResult);


            return new OrganizationResult()
            {
                Succeeded = organizationResult > 0,
                OrganizationId = organization.Id
            };
        }

        public async Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner, MarketingOrganizationUpgradeInput input)
        {

            _logger.LogInformation(GetLogMessage("{input}"), input);

            var retVal = new OrganizationResult()
            {
                OrganizationId = agencyOwner.OrganizationId
            };

            var organization = await Repository.Queryable()
                .Include(x => x.MarketingOrganization)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Where(x => x.Id == agencyOwner.OrganizationId && x.MarketingOrganization == null)
                .FirstOrDefaultAsync();

            if (organization == null)
            {
                retVal.ErrorMessage = "unable to upgrade organization, possibly because it's already upgraded";
                return retVal;
            }

            var orgPerson = organization
                .OrganizationPeople
                .First(x => x.PersonId == agencyOwner.CustomerId);

            retVal = UpgradeToMarketingOrganization(input, organization, orgPerson, true);

            return retVal;
        }

        public async Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner, ProviderOrganizationUpgradeInput input)
        {
            _logger.LogInformation(GetLogMessage("{@input}"), input);

            var retVal = new OrganizationResult()
            {
                OrganizationId = agencyOwner.OrganizationId
            };

            var organization = await Repository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Where(x => x.Id == agencyOwner.OrganizationId && x.ProviderOrganization == null)
                .FirstOrDefaultAsync();

            if (organization == null)
            {
                retVal.ErrorMessage = "unable to upgrade organization, possibly because it's already upgraded";
                return retVal;
            }

            var orgPerson = organization
                .OrganizationPeople
                .First(x => x.PersonId == agencyOwner.CustomerId);

            _logger.LogDebug(GetLogMessage("Person Found {0}"), orgPerson.PersonId);

            retVal = UpgradeToProviderOrganization(input, organization, orgPerson, true);

            return retVal;
        }

        public async Task<OrganizationResult> UpgradeOrganization(IAgencyOwner agencyOwner, RecruitingOrganizationUpgradeInput input)
        {
            _logger.LogInformation(GetLogMessage("{@input}"), input);

            var retVal = new OrganizationResult()
            {
                OrganizationId = agencyOwner.OrganizationId
            };

            var organization = await Repository.Queryable()
                .Include(x => x.ProviderOrganization)
                .Include(x => x.MarketingOrganization)
                .Include(x => x.RecruitingOrganization)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Marketer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Customer)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Contractor)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.ProjectManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.AccountManager)
                .Include(x => x.OrganizationPeople)
                .ThenInclude(x => x.Recruiter)
                .Where(x => x.Id == agencyOwner.OrganizationId && x.RecruitingOrganization==null)
                .FirstOrDefaultAsync();


            if (organization == null)
            {
                retVal.ErrorMessage = "unable to upgrade organization, possibly because it's already upgraded";
                return retVal;
            }

            var orgPerson = organization
                .OrganizationPeople
                .First(x => x.PersonId == agencyOwner.CustomerId);

            retVal = UpgradeToRecruitingOrganization(input, organization, orgPerson, true);

            return retVal;
        }
    }
}