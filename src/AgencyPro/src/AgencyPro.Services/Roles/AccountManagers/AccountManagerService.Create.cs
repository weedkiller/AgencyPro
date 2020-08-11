// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.AccountManagers
{
    public partial class AccountManagerService
    {
        public async Task<T> Create<T>(AccountManagerInput model)
            where T : AccountManagerOutput
        {
            _logger.LogTrace(GetLogMessage($@"Person Id: {model.PersonId}"));

            var am = await Repository.Queryable()
                .Where(x => x.Id == model.PersonId)
                .FirstOrDefaultAsync();

            if (am == null)
            {
                am = new AccountManager
                {
                    Id = model.PersonId
                };

                am.InjectFrom<NullableInjection>(model);

                am.Id = model.PersonId;
                am.Created = DateTimeOffset.UtcNow;

                await Repository.InsertAsync(am, true);

                var output = await GetById<T>(model.PersonId);

                await Task.Run(() =>
                {
                    RaiseEvent(new AccountManagerCreatedEvent
                    {
                        AccountManager = output
                    });
                });

                return output;
            }

            return await GetById<T>(model.PersonId);
        }
    }
}