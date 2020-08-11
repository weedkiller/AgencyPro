// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Config;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.CustomerAccounts.ViewModels;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.Services;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.Organizations.ViewModels;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AgencyPro.Identity.API.Controllers.Organization
{
    public class OrganizationController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly IOrganizationCustomerService _organizationCustomerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<AppSettings> _settings;

        private readonly IOrganizationService _service;
       private readonly ICustomerService _customerService;


        public OrganizationController(IPersonService personService,
            IHostingEnvironment hostingEnvironment,
            ICustomerAccountService customerAccountService,
            IOrganizationCustomerService organizationCustomerService,
            UserManager<ApplicationUser> userManager,
            IOrganizationService service,
            ICustomerService customerService, IOptions<AppSettings> settings)
        {
            _service = service;
            _customerService = customerService;
            _settings = settings;
            _hostingEnvironment = hostingEnvironment;
            _customerAccountService = customerAccountService;
            _organizationCustomerService = organizationCustomerService;
            _personService = personService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(this.User);
            var result = await _service.GetOrganizations<OrganizationOutput>(Guid.Parse(userId));
            return View(result != null ? result.ToList() : new List<OrganizationOutput>());
        }

        [Authorize]
        public async Task<IActionResult> Details([FromRoute] Guid id)
        {
            var org = await _service.GetOrganization<OrganizationOutput>(id);
            return View(org);
        }

        [Authorize]
        public IActionResult New() => View();

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(OrganizationCreateInput model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(this.User);
                var customer = _customerService.GetPrincipal(Guid.Parse(userId)).Result;
                if (customer != null)
                {
                    var result = await _service.CreateOrganization(customer, model);
                    if (result.Succeeded)
                    {
                        var organizationCustomer = new OrganizationCustomer()
                        {
                            OrganizationId = result.OrganizationId.Value,
                            CustomerId = Guid.Parse(userId)
                        };


                        var linkResult = await _customerAccountService.LinkOrganizationCustomer(organizationCustomer);
                        if (linkResult.Succeeded)
                        {
                            var nonce = Guid.NewGuid()
                                .ToString()
                                .Substring(0, 5);

                            Redirect($"/connect/authorize/callback?client_id=angularClient&redirect_uri={_settings.Value.Urls.FlowRedirect}&response_type=id_token token&scope=openid+email+profile+re_api+ag_api+ao_api+ma_api+am_api+pm_api+co_api+cu_api+pe_api&nonce={nonce}");

                        }
                        else
                        {

                            Redirect("/organization");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to create new organization");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to create new organization");
                }
            }
            return View();
        }


    }

}