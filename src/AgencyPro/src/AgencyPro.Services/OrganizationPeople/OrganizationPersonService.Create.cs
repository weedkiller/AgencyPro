// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationPeople.Events;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationPeople
{
    public partial class OrganizationPersonService
    {
        public async Task<OrganizationPersonResult> Create(CreateOrganizationPersonInput input, Guid organizationId, Guid? affiliateOrganizationId, bool checkValidation = true)
        {
            _logger.LogInformation(GetLogMessage("Organization Id: {0}"), organizationId);

            var isNotValid = CheckValidation(input, out OrganizationPersonResult retVal);

            if (isNotValid) return retVal;

            var result = await _personService
                .CreatePerson(input, input.RecruiterId, input.MarketerId, affiliateOrganizationId);
            
            _logger.LogDebug(GetLogMessage("Person: {@result}"), result);

            if (result.Succeeded)
            {
                if (result.PersonId != null)
                {
                    var orgPersonInput = new OrganizationPersonInput()
                    {
                        RecruiterBonus = input.RecruiterBonus,
                        MarketerBonus = input.MarketerBonus,
                        PersonId = result.PersonId.Value,
                    }.InjectFrom(input) as OrganizationPersonInput;

                    retVal = await Create(orgPersonInput, organizationId);
                }
                else
                {
                    _logger.LogWarning(GetLogMessage("Person result is in an invalid state"));
                }
            }
            else
            {
                retVal.ErrorMessage = result.ErrorMessage;
                _logger.LogWarning(GetLogMessage(retVal.ErrorMessage));
                _logger.LogWarning(GetLogMessage("Unable to promote candidate"));
            }
            
            return retVal;
        }

        private bool CheckValidation(CreateOrganizationPersonInput model, out OrganizationPersonResult retVal)
        {
            bool isPhoneExists = false;
            bool isEmailExists = false;

            if (!string.IsNullOrEmpty(model.PhoneNumber?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Phone Number validation : {0}"), model.PhoneNumber);
                isPhoneExists = _applicationUsers.Queryable().Any(a => a.PhoneNumber == model.PhoneNumber);
            }

            if (!string.IsNullOrEmpty(model.EmailAddress?.Trim()))
            {
                _logger.LogInformation(GetLogMessage("Email validation : {0}"), model.EmailAddress);
                isEmailExists = _applicationUsers.Queryable().Any(a => a.Email == model.EmailAddress);
            }

            retVal = new OrganizationPersonResult();

            if (!isPhoneExists && !isEmailExists) return false;

            retVal.Succeeded = false;
            retVal.ErrorMessage = GetErrorMessage(isPhoneExists, isEmailExists);
            _logger.LogInformation(GetLogMessage(retVal.ErrorMessage));
            return true;
        }

        private string GetErrorMessage(bool phoneValidation, bool isEmailExists)
        {
            List<string> validationList = new List<string>();
            if (phoneValidation)
                validationList.Add("Phone number");

            if (isEmailExists)
                validationList.Add("Email");

            return string.Join(", ", validationList) + " already exists.";
        }

        public async Task<OrganizationPersonResult> AddExistingPerson(IAgencyOwner agencyOwner, AddExistingPersonInput input)
        {
            _logger.LogInformation(GetLogMessage("Organization: {0}; Email: {1}"), agencyOwner.OrganizationId, input.EmailAddress);

            var retVal = new OrganizationPersonResult()
            {
                OrganizationId = agencyOwner.OrganizationId
            };
            var foundPerson = await _accountManager.FindByEmailAsync(input.EmailAddress);

            if (foundPerson != null)
            {
                _logger.LogDebug(GetLogMessage("Person Found: {0}"), foundPerson.Id);

                var orgPersonInput = new OrganizationPersonInput()
                {
                    PersonId = foundPerson.Id
                }.InjectFrom(input) as OrganizationPersonInput;

                retVal = await Create(orgPersonInput, agencyOwner.OrganizationId);

                _logger.LogDebug(GetLogMessage("Person Result: {@result}"), retVal);
            }
            else
            {
                _logger.LogWarning(GetLogMessage("User does not exist"));
            }

            return retVal;
        }


        public async Task<OrganizationPersonResult> Create(OrganizationPersonInput input, Guid organizationId)
        {
            _logger.LogTrace(GetLogMessage($@"For Person: {input.PersonId}, For Organization: {organizationId}"));

            var retVal = new OrganizationPersonResult()
            {
                OrganizationId = organizationId,
                PersonId = input.PersonId
            };

            var person = _personService.Get(input.PersonId);

            var organization = _organizationRepository.Queryable()
                .FirstOrDefaultAsync(x => x.Id == organizationId);

            await Task.WhenAll(person, organization);

            if (person.Result == null || organization.Result == null)
            {
                retVal.ErrorMessage = "Person or Organization is invalid";
                return retVal;
            }

            var organizationPerson = await Repository.Queryable()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.PersonId == input.PersonId && x.OrganizationId == organizationId);

            var records = 0;
            if (organizationPerson != null)
            {
                if (organizationPerson.IsDeleted)
                {
                    organizationPerson.IsDeleted = false;
                    organizationPerson.UpdatedById = _userInfo.UserId;
                    organizationPerson.CreatedById = _userInfo.UserId;
                    Repository.Update(organizationPerson);
                    records = Repository.Commit();
                }
            }
            else
            {
                organizationPerson = Repository.CreateOrgPerson(input, organizationId);
                records = Repository.Commit();
            }

            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            if (records > 0)
            {
                retVal.Succeeded = true;
                retVal.PersonId = organizationPerson.PersonId;

                await Task.Run(() =>
                {
                    RaiseEvent(new OrganizationPersonCreatedEvent
                    {
                        OrganizationId = organizationPerson.OrganizationId,
                        PersonId = organizationPerson.PersonId
                    });
                });

            }
            else
            {
                _logger.LogWarning(GetLogMessage("Unable to create organization person"));
            }

            return retVal;
        }


        public Task<OrganizationPersonResult> Create(IOrganizationAccountManager am, CreateOrganizationPersonInput input)
        {
            _logger.LogInformation(GetLogMessage("Organization: {0};"), am.OrganizationId);

            return Create(input, am.OrganizationId, input.AffiliateOrganizationId);
        }

        public Task<OrganizationPersonResult> Create(IAgencyOwner ao, CreateOrganizationPersonInput input)
        {
            _logger.LogInformation(GetLogMessage("Organization: {0};"), ao.OrganizationId);

            return Create(input, ao.OrganizationId, input.AffiliateOrganizationId);
        }
    }
}