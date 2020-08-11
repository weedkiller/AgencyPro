// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.IO;
using System.Threading.Tasks;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.UserAccount.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Identity.API.Controllers.Profile
{
    [Obsolete]
    public class ProfileController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(IPersonService personService,
            IHostingEnvironment hostingEnvironment,
            UserManager<ApplicationUser> userManager
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _personService = personService;
            _userManager = userManager;

        }
        [Authorize]
        [Obsolete]
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

        [Authorize]
        [Obsolete]
        public IActionResult Password()
        {
            var userId = _userManager.GetUserId(this.User);
            return View("ChangePassword");
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

                    }
                }
            }
            return View("ChangePassword", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<IActionResult> Email(ChangeEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                user.Email = model.Email;
                user.UserName = model.Email;
                user.NormalizedUserName = model.Email.ToLower();
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("ChangeEmail", model);
        }

        [Obsolete]
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(PersonInput model, IFormFile ProfileImage)
        //{
        //    var userId = _userManager.GetUserId(this.User);

        //    if (model.Id == null || model.Id == Guid.Empty)
        //    {
        //        model.Id = Guid.Parse(userId);
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        if (ProfileImage != null)
        //        {
        //            // model.ImageUrl = SaveLocal(ProfileImage);
        //            model.ImageUrl = await _personService.UploadProfilePic(model.Id, ProfileImage);
        //        }
        //        var personOutput = await _personService.CreatePerson<PersonOutput>(model);
        //        return View(personOutput);
        //    }
        //    return View(model);
        //}
        private string SaveLocal(IFormFile file)
        {
            if (file != null)
            {
                var fileName = GetUniqueFileName(file.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, fileName);
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                return $"/uploads/{fileName}";
            }
            return null;
        }
    }
}