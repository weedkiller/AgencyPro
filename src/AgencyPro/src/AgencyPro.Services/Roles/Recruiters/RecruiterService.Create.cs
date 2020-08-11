// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Recruiters;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Recruiters
{
    public partial class RecruiterService
    {
        public Task<T> Create<T>(RecruiterInput model
        )
            where T : RecruiterOutput
        {
            return CreateInternal<T>(model);
        }

        private async Task<T> CreateInternal<T>(RecruiterInput model)
            where T : RecruiterOutput

        {
            var recruiter = await Repository.Queryable().Where(x => x.Id == model.PersonId)
                .FirstOrDefaultAsync();

            if (recruiter == null)
            {
                var entity = new Recruiter().InjectFrom(model) as Recruiter;

                await Repository.InsertAsync(entity, true);

                var output = await GetById<T>(model.PersonId);
                await Task.Run(() =>
                {
                    RaiseEvent(new RecruiterCreatedEvent<T>
                    {
                        Recruiter = output
                    });
                });

                return output;
            }

           return await GetById<T>(model.PersonId);
        }
    }
}