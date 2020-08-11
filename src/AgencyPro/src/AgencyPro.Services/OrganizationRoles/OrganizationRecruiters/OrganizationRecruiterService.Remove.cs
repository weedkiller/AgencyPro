// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.OrganizationRoles.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyPro.Services.OrganizationRoles.OrganizationRecruiters
{
    public partial class OrganizationRecruiterService
    {
        public async Task<bool> Remove(IAgencyOwner ao, Guid personId, bool commit = true)
        {
            _logger.LogTrace(
                GetLogMessage($@"Recruiter: {personId}, Organization: {ao.OrganizationId}"));

            var entity = _personRepository.Queryable()
                .Include(x => x.Recruiter)
                .ThenInclude(x => x.Candidates)
                .FirstOrDefault(x => x.PersonId == personId && x.OrganizationId == ao.OrganizationId);

            var organization = await _orgService
                .Repository.Queryable()
                .Include(x => x.Recruiters)
                .Where(x => x.Id == ao.OrganizationId)
                .FirstAsync();

            if (entity.Recruiter.Candidates.Count > 0)
            {
                foreach (var e in entity.Recruiter.Candidates)
                {
                    e.RecruiterId = organization.RecruitingOrganization.DefaultRecruiterId;
                    e.UpdatedById = _userInfo.UserId;
                    e.Updated = DateTimeOffset.UtcNow;
                    e.ObjectState = ObjectState.Modified;

                }


            }

            entity.Recruiter.IsDeleted = true;
            entity.Recruiter.UpdatedById = _userInfo.UserId;
            entity.Recruiter.Updated = DateTimeOffset.UtcNow;
            entity.Recruiter.ObjectState = ObjectState.Modified;

            entity.UpdatedById = _userInfo.UserId;
            entity.Updated = DateTimeOffset.UtcNow;
            entity.ObjectState = ObjectState.Modified;

            int result = _personRepository.InsertOrUpdateGraph(entity, true);
            return result > 0;
        }

    }
}