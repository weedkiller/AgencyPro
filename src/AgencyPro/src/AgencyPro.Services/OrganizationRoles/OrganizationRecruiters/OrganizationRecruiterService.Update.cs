// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Events.OrganizationRecruiters;
using AgencyPro.Core.OrganizationRoles.Extensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationRecruiters
{
    public partial class OrganizationRecruiterService
    {
        public Task<T> Update<T>(IAgencyOwner ao, OrganizationRecruiterInput input)
            where T : OrganizationRecruiterOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Update<T>(input);
        }

        public Task<T> Update<T>(IOrganizationAccountManager am, OrganizationRecruiterInput input) where T : OrganizationRecruiterOutput
        {
            input.OrganizationId = am.OrganizationId;
            return Update<T>(input);
        }

        private async Task<T> Update<T>(OrganizationRecruiterInput model)
            where T : OrganizationRecruiterOutput
        {
            _logger.LogInformation(GetLogMessage("Recruiter: {0}"), model.RecruiterId);

            var entity = await Repository.Queryable()
                .FindById(model.RecruiterId, model.OrganizationId)
                .FirstAsync();

            entity.Updated = DateTimeOffset.UtcNow;
            entity.RecruiterStream = model.RecruiterStream;
            entity.RecruiterBonus = model.RecruiterBonus;

            entity.InjectFrom(model);

            var records = await Repository.UpdateAsync(entity, true);
            _logger.LogDebug(GetLogMessage("{0} records updated"), records);

            var result = await GetById<T>(model.RecruiterId, model.OrganizationId);

            if (records > 0)
            {
                await Task.Run(() =>
                {
                    RaiseEvent(new OrganizationRecruiterUpdatedEvent<T>
                    {
                        OrganizationMarketerOutput = result
                    });
                });
            }
            
            return result;
        }
    }
}