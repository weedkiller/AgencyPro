// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.ForgotPassword.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Identity.API.Controllers.Account
{
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                //if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }

                var result = await _forgotPasswordService.ForgotPassword(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
    }
}