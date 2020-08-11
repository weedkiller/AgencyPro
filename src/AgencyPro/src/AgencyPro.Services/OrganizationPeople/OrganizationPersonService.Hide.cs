// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationPeople
{
    public partial class OrganizationPersonService
    {
        public async Task<OrganizationPersonResult> HideOrganization(IOrganizationPerson person)
        {
            _logger.LogInformation(GetLogMessage("Org: {0}"), person.OrganizationId);

            var retVal = new OrganizationPersonResult()
            {
                OrganizationId = person.OrganizationId,
                PersonId = person.PersonId
            };

            var entity = await Repository.Queryable().Where(x =>
                    x.OrganizationId == person.OrganizationId && x.PersonId == person.PersonId)
                .FirstOrDefaultAsync();

            entity.IsHidden = true;
            
            var result = Repository.Update(entity, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"));

            if (result > 0)
            {
                retVal.Succeeded = true;
            }

            return retVal;
        }
    }
}