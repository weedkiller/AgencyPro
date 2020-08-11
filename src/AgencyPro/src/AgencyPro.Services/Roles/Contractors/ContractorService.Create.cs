// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.Roles.Events;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.Contractors;
using AgencyPro.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.Roles.Contractors
{
    public partial class ContractorService
    {
        public async Task<T> Create<T>(ContractorInput input
        )
            where T : ContractorOutput
        {
            _logger.LogTrace(GetLogMessage($@"Creating Contractor: {input.PersonId}"));

            var contractor = await Repository.Queryable()
                .Where(x => x.Id == input.PersonId)
                .FirstOrDefaultAsync();

            var recruiter = await _recruiterRepository.Queryable().GetById(input.RecruiterOrganizationId,
                input.RecruiterId).FirstAsync();
            
            if (contractor == null)
            {
                contractor = new Contractor
                {
                    Id = input.PersonId,
                    HoursAvailable = input.HoursAvailable,
                    IsAvailable = input.IsAvailable
                };

                contractor.InjectFrom(input);
                
                await Repository.UpdateAsync(contractor, true);

                var output = await GetById<T>(input.PersonId);

                await Task.Run(() =>
                {
                    RaiseEvent(new ContractorCreatedEvent<T>
                    {
                        Contractor = output
                    });
                });

                return output;
            }

            return await GetById<T>(input.PersonId);
        }
    }
}