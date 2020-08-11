// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.People.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.People
{
    public partial class PersonService
    {
        public async Task<bool> SwitchOrgs(SwitchOrganizationInput input)
        {
            var entities = await _orgPersonRepository.Queryable()
                .Where(x => x.PersonId == input.PersonId
                            & (x.IsDefault | x.OrganizationId == input.OrganizationId))
                .ToListAsync();

            foreach (var entity in entities)
            {
                entity.IsDefault = input.OrganizationId == entity.OrganizationId;
                entity.ObjectState = ObjectState.Modified;
            }
            var result = _orgPersonRepository.Commit();

            return result > 0;
        }
    }
}