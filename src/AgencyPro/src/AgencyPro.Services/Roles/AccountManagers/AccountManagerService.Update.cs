// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.AccountManagers
{
    public partial class AccountManagerService
    {
        public async Task<T> Update<T>(IAccountManager accountManager, AccountManagerUpdateInput model)
            where T : AccountManagerOutput
        {
            var entity = await Repository.Queryable().FirstAsync(x => x.Id == accountManager.Id);

            return await Update<T>(entity, model);
        }

        private async Task<T> Update<T>(AccountManager accountManager, AccountManagerUpdateInput model)
        {
            accountManager.InjectFrom(model);
            accountManager.Updated = DateTimeOffset.UtcNow;

            await Repository.UpdateAsync(accountManager, true);

            return await Repository.Queryable().Where(x => x.Id == accountManager.Id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

    }
}