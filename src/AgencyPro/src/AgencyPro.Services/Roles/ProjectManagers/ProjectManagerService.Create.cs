// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.ProjectManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.ProjectManagers
{
    public partial class ProjectManagerService
    {
        public async Task<T> Create<T>(ProjectManagerInput model)
            where T : ProjectManagerOutput
        {
            _logger.LogTrace(GetLogMessage($@"Person Id: {model.PersonId}"));

            var pm = await Repository.Queryable()
                .Where(x => x.Id == model.PersonId)
                .FirstOrDefaultAsync();

            if (pm == null)
            {
                pm = new ProjectManager
                {
                    Id = model.PersonId
                };

                pm.InjectFrom<NullableInjection>(model);

                pm.Id = model.PersonId;
                pm.Created = DateTimeOffset.UtcNow;

                await Repository.InsertAsync(pm, true);

                var output = await GetById<T>(model.PersonId);

                await Task.Run(() =>
                {
                    RaiseEvent(new ProjectManagerCreatedEvent
                    {
                        ProjectManager = output
                    });
                });

                return output;
            }

            return await GetById<T>(model.PersonId);
        }
    }
}