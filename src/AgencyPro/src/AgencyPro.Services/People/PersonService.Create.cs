// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.People.Events;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Roles.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.People
{
    public partial class PersonService
    {
        public async Task<PersonResult> CreatePerson(
            PersonInput input, Guid? recruiterId, Guid? marketerId, Guid? affiliateOrganizationId, string password = "AgencyPro!"
        )
        {
            _logger.LogInformation(GetLogMessage("Recruiter:{0};Marketer:{1};AffiliateOrg:{2}"), recruiterId, marketerId, affiliateOrganizationId);

            var user = await _userAccountManager
               .FindByEmailAsync(input.EmailAddress);

            var retVal = new PersonResult();

            if (user == null)
            {
                OrganizationRecruiter re = null;
                OrganizationMarketer ma = null;

                if (affiliateOrganizationId.HasValue)
                {
                    if (marketerId.HasValue)
                    {
                        ma = await _orgMarketerRepository.Queryable()
                            .Where(x => x.OrganizationId == affiliateOrganizationId.Value && x.MarketerId == marketerId.Value)
                            .FirstOrDefaultAsync();
                    }
                    else
                    {
                        ma = await _orgMarketerRepository.Queryable()
                            .Include(x => x.OrganizationDefaults)
                            .Where(x => x.OrganizationId == affiliateOrganizationId.Value
                                        && x.OrganizationDefaults.Any())
                            .FirstOrDefaultAsync();
                    }

                    if (recruiterId.HasValue)
                    {
                        re = await _orgRecruiterRepository.Queryable()
                            .Where(x => x.OrganizationId == affiliateOrganizationId.Value && x.RecruiterId == recruiterId.Value)
                            .FirstOrDefaultAsync();
                    }
                    else
                    {
                        re = await _orgRecruiterRepository.Queryable()
                            .Include(x => x.RecruitingOrganizationDefaults)
                            .Where(x => x.OrganizationId == affiliateOrganizationId.Value && x.RecruitingOrganizationDefaults.Any())
                            .FirstOrDefaultAsync();
                    }
                }

                if (ma == null)
                    ma = await _orgMarketerRepository.Queryable().Where(x => x.IsSystemDefault).FirstAsync();

                if (re == null)
                    re = await _orgRecruiterRepository.Queryable().Where(x => x.IsSystemDefault).FirstAsync();

                user = new ApplicationUser
                {
                    UserName = input.EmailAddress,
                    Email = input.EmailAddress,
                    EmailConfirmed = false,
                    Created = DateTimeOffset.UtcNow,
                    PhoneNumber = input.PhoneNumber
                };

                var result = await _userAccountManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    retVal.ErrorMessage = result.ToString();
                    return retVal;
                }

                var person = new Person
                {
                    ImageUrl = "https://www.dropbox.com/s/icxbbieztc2rrwd/default-avatar.png?raw=1",
                    Id = user.Id,
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Iso2 = input.Iso2,
                    ProvinceState = input.ProvinceState,
                    ReferralCode = ma.ReferralCode,
                    AccountManager = new AccountManager()
                    {
                        Id = user.Id,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,
                        ObjectState = ObjectState.Added
                    },
                    ProjectManager = new ProjectManager()
                    {
                        Id = user.Id,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,
                        ObjectState = ObjectState.Added
                    },
                    Marketer = new Marketer()
                    {
                        Id = user.Id,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,
                        ObjectState = ObjectState.Added
                    },
                    Recruiter = new Recruiter()
                    {
                        Id = user.Id,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,
                        ObjectState = ObjectState.Added
                    },
                    Contractor = new Contractor()
                    {
                        RecruiterOrganizationId = re.OrganizationId,
                        RecruiterId = re.RecruiterId,
                        IsAvailable = false,
                        ObjectState = ObjectState.Added,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,

                    },
                    Customer = new Customer()
                    {
                        MarketerId = ma.MarketerId,
                        MarketerOrganizationId = ma.OrganizationId,
                        ObjectState = ObjectState.Added,
                        Created = DateTimeOffset.UtcNow,
                        Updated = DateTimeOffset.UtcNow,
                    },
                    ObjectState = ObjectState.Added,
                    Created = DateTimeOffset.UtcNow,
                    Updated = DateTimeOffset.UtcNow
                };

                var theResult = Repository.InsertOrUpdateGraph(person, true);

                _logger.LogDebug(GetLogMessage("{0} results updated"), theResult);

                if (theResult > 0)
                {
                    retVal.Succeeded = true;
                    retVal.PersonId = person.Id;

                    await Task.Run(() =>
                    {
                        RaiseEvent(new PersonCreatedEvent
                        {
                            PersonId = person.Id
                        });
                    });
                }
            }
            else
            {
                _logger.LogInformation(GetLogMessage("Email address:{0};"), input.EmailAddress);
                retVal.ErrorMessage = "Email address already exists";
            }

            return retVal;
        }
        
    }
}