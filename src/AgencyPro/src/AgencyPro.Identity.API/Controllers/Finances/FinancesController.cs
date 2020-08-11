// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Identity.API.Controllers.Finances
{
    public class FinancesController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public FinancesController(IPersonService personService,
            IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _personService = personService;
            _userManager = userManager;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(this.User);
            if (userId != null)
            {
                var person = await  _personService.GetPerson<PersonOutput>(Guid.Parse(userId));
                if(person != null)
                {
                    return View(person);
                }
            }
            return View(new PersonOutput() { Id = Guid.Parse(userId) });
        }
    }
}