// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;

namespace AgencyPro.Services.OrganizationRoles.OrganizationRecruiters
{
    public partial class OrganizationRecruiterService
    {
        public async Task<T> Create<T>(
            OrganizationRecruiterInput model
        )
            where T : OrganizationRecruiterOutput
        {
            var entity = await Repository.Queryable().IgnoreQueryFilters()
                .Where(x => x.OrganizationId == model.OrganizationId && x.RecruiterId == model.RecruiterId)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                if (entity.IsDeleted)
                {
                    entity.IsDeleted = false;
                    entity.UpdatedById = _userInfo.UserId;
                    entity.Updated = DateTimeOffset.UtcNow;
                    entity.Created = entity.Updated;
                    entity.CreatedById = _userInfo.UserId;

                    entity.InjectFrom(model);

                    var records = await Repository.UpdateAsync(entity, true);
                    _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

                    if (records > 0)
                    {

                    }

                }
            }
            else
            {
                entity = new OrganizationRecruiter()
                {
                    Updated = DateTimeOffset.UtcNow,
                    Created = DateTimeOffset.UtcNow,
                    CreatedById = _userInfo.UserId,
                    UpdatedById = _userInfo.UserId,
                    RecruiterStream = model.RecruiterStream,
                    RecruiterBonus = model.RecruiterBonus
                }.InjectFrom(model) as OrganizationRecruiter;

                var records = await Repository.InsertAsync(entity, true);

                _logger.LogDebug(GetLogMessage("{0} Records Updated"), records);

                if (records > 0)
                {

                }
            }
            
            var output = await GetById<T>(model.RecruiterId, model.OrganizationId);

            return output;
        }


        public Task<T> Create<T>(IAgencyOwner ao, OrganizationRecruiterInput input)
            where T : OrganizationRecruiterOutput
        {
            input.OrganizationId = ao.OrganizationId;
            return Create<T>(input);
        }
    }
}