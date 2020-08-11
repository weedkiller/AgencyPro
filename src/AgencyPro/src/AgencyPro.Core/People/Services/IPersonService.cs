// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.People.ViewModels;
using AgencyPro.Core.Services;
using Microsoft.AspNetCore.Http;

namespace AgencyPro.Core.People.Services
{
    public interface IPersonService : IService<Person>
    {
        Task<T> GetPerson<T>(Guid personId) where T : PersonOutput;
        Task<PersonOutput> Get(Guid personId);
        Task<T> GetPerson<T>(string email) where T : PersonOutput;
        Task<bool> SwitchOrgs(SwitchOrganizationInput input);

        Task<List<T>> GetPeople<T>(Guid[] ids) where T : PersonOutput;

        Task<PersonResult> CreatePerson(PersonInput input, Guid? recruiterId, 
            Guid? marketerId, Guid? affiliateOrganizationId, string password = "AgencyPro!");

        Task<PersonResult> UpdateProfilePic(Guid personId, IFormFile image);
        Task<string> UploadProfilePic(Guid personId, IFormFile image);

        Task<PersonResult> UpdatePersonDetails(Guid personId, PersonDetailsInput input);
        Task<PersonOutput> CreateOrUpdate(PersonOutput person);
    }
}