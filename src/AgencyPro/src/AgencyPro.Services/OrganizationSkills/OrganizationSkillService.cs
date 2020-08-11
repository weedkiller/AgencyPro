// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Skills.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.OrganizationSkills
{
    public partial class OrganizationSkillService : Service<OrganizationSkill>, IOrganizationSkillService
    {
        private readonly ILogger<OrganizationSkillService> _logger;

        public OrganizationSkillService(IServiceProvider serviceProvider,
            ILogger<OrganizationSkillService> logger) : base(serviceProvider)
        {
            _logger = logger;
        }

        //public async Task<Organization> AddAgencySkill(OrganizationCustomer agencyOwner, AgencySkillInput input)
        //{
        //    var s = new OrganizationSkill { SkillId = input.SkillId, OrganizationId = agencyOwner.OrganizationId };

        //    agencyOwner.Organization.Skills.Add(s);

        //    await _primaryContext.SaveChangesAsync();

        //    return await GetOrganization(agencyOwner.OrganizationId);
        //}

        //public async Task<Organization> RemoveAgencySkill(OrganizationCustomer agencyOwner, AgencySkillInput input)
        //{
        //    var skill = agencyOwner
        //        .Organization
        //        .Skills
        //        .First(x => x.SkillId == input.SkillId && x.OrganizationId == agencyOwner.OrganizationId);

        //    agencyOwner.Organization.Skills.Remove(skill);

        //    await _primaryContext.SaveChangesAsync();

        //    return await GetOrganization(agencyOwner.OrganizationId);
        //}

        public Task<List<OrganizationSkillOutput>> GetSkills(Guid organizationId)
        {
            return Repository.Queryable().Where(x => x.OrganizationId == organizationId)
                .ProjectTo<OrganizationSkillOutput>(ProjectionMapping)
                .ToListAsync();
        }

        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[OrganizationSkillService.{callerName}] - {message}";
        }
    }
}