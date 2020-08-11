// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.Services
{
    public interface IRecruitingOrganizationService
    {
        Task<List<T>> Discover<T>(IProviderAgencyOwner owner) where T : RecruitingOrganizationOutput;
    }
}