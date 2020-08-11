// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Contractors
{
    public partial class ContractorService
    {
        private async Task<T> Update<T>(Contractor contractor, ContractorUpdateInput model)
        {
            contractor.InjectFrom(model);
            contractor.Updated = DateTimeOffset.UtcNow;

            await Repository.UpdateAsync(contractor, true);

            return await Repository.Queryable().Where(x => x.Id == contractor.Id)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }

        public async Task<T> Update<T>(IContractor principal, ContractorUpdateInput model)
            where T : ContractorOutput
        {
            var entity = await Repository.Queryable().Where(x => x.Id == principal.Id)
                .FirstAsync();

            return await Update<T>(entity, model);
        }
    }
}