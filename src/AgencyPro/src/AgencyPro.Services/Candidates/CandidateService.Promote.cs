// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Candidates.Enums;
using AgencyPro.Core.Candidates.Events;
using AgencyPro.Core.Candidates.Extensions;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Candidates.ViewModels;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.Candidates
{
    public partial class CandidateService
    {
        public async Task<CandidatePromotionResult> Promote(
            IOrganizationProjectManager pm,
            Guid candidateId,
            CandidateAcceptInput model)
        {
            _logger.LogInformation(GetLogMessage("PM: {0}; Promoting Candidate: {1}"), pm.OrganizationId, candidateId);

            var candidate = await Repository.Queryable().ForOrganizationProjectManager(pm)
                .Where(x => x.Id == candidateId).FirstAsync();

            var isNotValid = CheckValidation(candidateId, candidate, out var retVal);

            if (isNotValid) return retVal;

            if (candidate.Status != CandidateStatus.Qualified && candidate.Status != CandidateStatus.Promoted)
            {
                retVal.ErrorMessage = "Candidate can only be promoted in the qualified state";
                return retVal;
            }

            _logger.LogDebug(GetLogMessage("Candidate is able to be promoted"));

            var orgPerson = await _organizationPersonService.Create(new CreateOrganizationPersonInput()
            {
                Iso2 = candidate.Iso2,
                ProvinceState = candidate.ProvinceState,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                EmailAddress = candidate.EmailAddress,
                PhoneNumber = candidate.PhoneNumber,
                SendEmail = false,
                ContractorStream = model.Rate,
                RecruiterId = candidate.RecruiterId,
                AffiliateOrganizationId = candidate.RecruiterOrganizationId,
                IsContractor = true

            }, pm.OrganizationId, candidate.RecruiterOrganizationId, false);

            if (orgPerson.Succeeded)
            {

                _logger.LogDebug(GetLogMessage("New person added {0}"), orgPerson.PersonId);
                candidate.Status = CandidateStatus.Promoted;

                candidate.StatusTransitions.Add(new CandidateStatusTransition()
                {
                    ObjectState = ObjectState.Added,
                    Status = CandidateStatus.Promoted
                });

                candidate.UpdatedById = _userInfo.UserId;
                candidate.ObjectState = ObjectState.Modified;

                candidate.Updated = DateTimeOffset.UtcNow;

                var result = Repository.Update(candidate, true);

                _logger.LogDebug(GetLogMessage("{0} results added"), result);


                if (result > 0)
                {
                    retVal.Succeeded = true;
                    retVal.CandidateId = candidateId;
                    retVal.PersonId = orgPerson.PersonId;

                    await Task.Run(() =>
                    {
                        RaiseEvent(new CandidatePromotedEvent()
                        {
                            CandidateId = candidateId
                        });
                    });
                }
            }
            else
            {
                _logger.LogWarning(GetLogMessage("Unable to create org person"));
            }


            return retVal;

        }

        private bool CheckValidation(Guid candidateId, Candidate model, out CandidatePromotionResult retVal)
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

            retVal = new CandidatePromotionResult
            {
                CandidateId = candidateId
            };

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
    }
}