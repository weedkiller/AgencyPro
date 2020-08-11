// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.ViewModels;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationPeople
{
    public partial class OrganizationPersonService
    {
        public async Task<OrganizationPersonResult> Update(OrganizationPersonInput input, Guid organizationId)
        {
            _logger.LogInformation(GetLogMessage($@"For Person: {input.PersonId}, For Organization: {organizationId}"));
            
            var retVal = new OrganizationPersonResult()
            {
                PersonId = input.PersonId,
                OrganizationId = organizationId
            };

            return await Task.FromResult(retVal);
        }
        
    }
}