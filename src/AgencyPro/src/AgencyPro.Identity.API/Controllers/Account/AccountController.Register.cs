// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Registration.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Identity.API.Controllers.Account
{
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var vm = await BuildRegisterViewModel();

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _registrationService.Register(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(nameof(model.Email), "Registration Error");
            }

            GetCountries();

            return View(model);
        }

        private Task<RegisterViewModel> BuildRegisterViewModel()
        {
            GetCountries();
            return Task.FromResult(new RegisterViewModel());
        }

        private void GetCountries()
        {
            var list = _geoService.Repository.Queryable()
                .Include(x => x.EnabledCountry)
                .Where(x => x.EnabledCountry != null).ToList();

            list.Insert(0, new Core.Geo.Models.Country { Iso2 = "", Name = "--Select one--" });

            ViewBag.Country = new SelectList(list, "Iso2", "Name", "US");
        }
    }
}