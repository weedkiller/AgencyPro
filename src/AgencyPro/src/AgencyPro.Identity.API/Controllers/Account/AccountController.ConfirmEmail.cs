// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Identity.API.Controllers.Account
{
    public partial class AccountController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid UserId or Code");
            }
            else
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, $"Unable to load user with ID '{userId}'.");
                }

                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model, [FromQuery] string userId,
            [FromQuery] string code)
        {
            if (model.UserId == null || model.Code == null)
            {
                return Redirect("~/");
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{model.UserId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, model.Code);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }
    }
}