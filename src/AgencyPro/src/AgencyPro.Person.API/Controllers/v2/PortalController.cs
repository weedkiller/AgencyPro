// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AgencyPro.Person.API.Controllers.v2
{
    [Authorize]
    [Route("portal")]
    public class PortalController : ControllerBase
    {
        protected Guid UserId => new Guid(_userManager.GetUserId(User));
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<AppSettings> _settings;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IPersonService _personService;
        private readonly IOrganizationService _organizationService;
        private readonly ICustomerService _customerService;

        public PortalController(IPersonService personService, 
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> settings,
            ICustomerAccountService customerAccountService,
            IOrganizationService organizationService, 
            ICustomerService customerService)
        {
            _personService = personService;
            _userManager = userManager;
            _settings = settings;
            _customerAccountService = customerAccountService;
            _organizationService = organizationService;
            _customerService = customerService;
        }

        [HttpGet("profile/profile")]
        public async Task<IActionResult> GetProfile()
        {
            var personOutput = await _personService.GetPerson<PersonOutput>(UserId);
            return Ok(personOutput);
        }

        // this is an output, don't EVER use it as an input, the architect was NOT an idiot
        [HttpPut("profile/profile")]
        public async Task<IActionResult> UpdateProfile([FromBody]PersonOutput model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Select(x => x.Value.Errors).First(y => y.Count > 0);
                return BadRequest(error.First().ErrorMessage);
            }

            var personOutput = await _personService.CreateOrUpdate(model);
            return Ok(personOutput);
        }

        [HttpPost("profile/image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var imageUrl = await _personService.UploadProfilePic(UserId, file);
            return Ok($"\"{imageUrl}\"");
        }


        [HttpGet("profile/email")]
        public async Task<IActionResult> GetEmail()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok($"\"{user.Email}\"");
        }

        [HttpPut("profile/password")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Select(x => x.Value.Errors).First(y => y.Count > 0);
                return BadRequest(error.First().ErrorMessage);
            }
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("profile/email")]
        public async Task<IActionResult> UpdateEmail([FromBody]EmailModel model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Select(x => x.Value.Errors).First(y => y.Count > 0);
                return BadRequest(error.First().ErrorMessage);
            }
            var user = await _userManager.GetUserAsync(User);
            user.Email = model.Email;
            var result = await _userManager.UpdateAsync(user);
            return Ok(result);
        }

        [HttpGet("profile/organizations")]
        public async Task<IActionResult> GetOrganizations()
        {
            var result = await _organizationService.GetOrganizations<OrganizationOutput>(UserId);
            return Ok(result);
        }


        [HttpGet("profile/organizations/{id:Guid}")]
        public async Task<IActionResult> GetOrganization(Guid id)
        {
            var result = await _organizationService.GetOrganization<OrganizationOutput>(id);
            return Ok(result);
        }

        [HttpPost("profile/organizations")]
        public async Task<IActionResult> CreateOrganization([FromBody] OrganizationCreateInput model)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Select(x => x.Value.Errors).First(y => y.Count > 0);
                return BadRequest(error.First().ErrorMessage);
            }
            var customer = await _customerService.GetPrincipal(UserId);
            var result = await _organizationService.CreateOrganization(customer, model);
            if (result.Succeeded)
            {
                var organizationCustomer = new OrganizationCustomer()
                {
                    OrganizationId = result.OrganizationId.Value,
                    CustomerId =UserId
                };


                var linkResult = await _customerAccountService.LinkOrganizationCustomer(organizationCustomer);
                if (linkResult.Succeeded)
                {
                    return await GetOrganization(result.OrganizationId.Value);

                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create new organization");
            }

            return BadRequest();
        }
        
    }
}