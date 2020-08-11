// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople;
using AgencyPro.Core.OrganizationPeople.Filters;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Middleware.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.AccountManager.API.Controllers.v2
{
    public class PeopleController : OrganizationUserController
    {
        private readonly IOrganizationAccountManager _accountManager;
        private readonly IOrganizationAccountManagerService _accountManagerService;
        private readonly IOrganizationContractorService _contractorService;
        private readonly IOrganizationCustomerService _customerService;
        private readonly IOrganizationMarketerService _marketerService;
        private readonly IOrganizationPersonService _personService;
        private readonly IOrganizationProjectManagerService _projectManagerService;
        private readonly IOrganizationRecruiterService _recruiterService;

        public PeopleController(
            IOrganizationMarketerService marketerService,
            IOrganizationRecruiterService recruiterService,
            IOrganizationProjectManagerService projectManagerService,
            IOrganizationAccountManagerService accountManagerService,
            IOrganizationCustomerService customerService,
            IOrganizationContractorService contractorService,
            IOrganizationPersonService personService,
            IOrganizationAccountManager accountManager,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _marketerService = marketerService;
            _recruiterService = recruiterService;
            _projectManagerService = projectManagerService;
            _accountManagerService = accountManagerService;
            _customerService = customerService;
            _contractorService = contractorService;
            _accountManager = accountManager;
            _personService = personService;
        }

        

        /// <summary>
        ///     removes a person from an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="personId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public ActionResult RemovePerson([FromRoute] Guid organizationId, [FromRoute] Guid personId,
            [FromQuery] RemovePersonInput model
        )
        {
            return Ok();
        }

        /// <summary>
        ///     Gets a person within an organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(AccountManagerOrganizationPersonDetailsOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPerson([FromRoute] Guid organizationId,
            [FromRoute] Guid personId
        )
        {
            var result = await _personService
                .GetOrganizationPerson<AccountManagerOrganizationPersonDetailsOutput>(personId, organizationId);
            return Ok(result);
        }

        /// <summary>
        /// Gets all people within an organization
        /// </summary>
        /// <param name="organizationId">the organization id</param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationPersonOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPeopleInOrganization([FromRoute] Guid organizationId,
            [FromQuery] OrganizationPeopleFilters filters
        )
        {
            var result = await _personService.GetPeople<AccountManagerOrganizationPersonOutput>(_accountManager, filters);
            AddPagination(filters, result.Total);
            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all marketers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("marketer")]
        [Obsolete]
        [ProducesResponseType(typeof(List<AgencyOwnerOrganizationMarketerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMarketers([FromRoute] Guid organizationId, [FromQuery]MarketerFilters filters
        )
        {
            var result = await _marketerService
                .GetForOrganization<AgencyOwnerOrganizationMarketerOutput>(_accountManager.OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// Gets all recruiters within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("recruiter")]
        [Obsolete]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationRecruiterOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecruiters(
            [FromRoute] Guid organizationId, 
            [FromQuery] RecruiterFilters filters
        )
        {
            var result = await _recruiterService
                .GetForOrganization<AccountManagerOrganizationRecruiterOutput>(_accountManager.OrganizationId, filters);

            return Ok(result);
        }

        /// <summary>
        /// Gets all account managers within an organization
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet("account-manager")]
        [Obsolete]
        [ProducesResponseType(typeof(List<AccountManagerOrganizationAccountManagerOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountManagers([FromRoute] Guid organizationId,
            [FromQuery]AccountManagerFilters filters
        )
        {
            var result = await _accountManagerService
                .GetForOrganization<AccountManagerOrganizationAccountManagerOutput>(_accountManager.OrganizationId, filters);

            return Ok(result);
        }

       
    }
}