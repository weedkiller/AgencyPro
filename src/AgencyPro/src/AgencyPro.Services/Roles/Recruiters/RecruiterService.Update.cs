// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Recruiters
{
    public partial class RecruiterService
    {
        public async Task<T> Update<T>(IRecruiter principal, RecruiterUpdateInput model)
            where T : RecruiterOutput
        {
            var recruiter = await Repository.FirstOrDefaultAsync(x => x.Id == principal.Id);

            return await UpdateInternal<T>(recruiter, model);
        }

        private async Task<T> UpdateInternal<T>(Recruiter recruiter,  RecruiterUpdateInput input)
            where T : RecruiterOutput
        {
            recruiter.InjectFrom(input);
            recruiter.Updated = DateTimeOffset.UtcNow;
            recruiter.ObjectState = ObjectState.Modified;

            await Repository.UpdateAsync(recruiter, true);

            var output = await GetById<T>(recruiter.Id);
           
            return output;
        }
    }
}