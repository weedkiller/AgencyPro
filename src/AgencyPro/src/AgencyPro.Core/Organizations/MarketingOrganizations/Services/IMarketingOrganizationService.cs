// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.Services
{
    public interface IMarketingOrganizationService
    {
        Task<List<T>> Discover<T>(IProviderAgencyOwner owner) where T : MarketingOrganizationOutput;
    }
}