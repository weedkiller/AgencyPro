// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AgencyPro.Services.MarketingOrganizations
{
    public partial class MarketingOrganizationService
    {
        private static string GetLogMessage(string message, [CallerMemberName] string callerName = null)
        {
            return $"[{nameof(MarketingOrganizationService)}.{callerName}] - {message}";
        }

        public async Task<List<T>> Discover<T>(IProviderAgencyOwner owner) where T : MarketingOrganizationOutput
        {
            _logger.LogInformation(GetLogMessage("For Provider Agency: {0}"), owner.OrganizationId);

            var exclude = await _marketingAgreement.Queryable().IgnoreQueryFilters()
                .Where(x => x.ProviderOrganizationId == owner.OrganizationId)
                .Select(x => x.MarketingOrganizationId).ToListAsync();
            exclude.Add(owner.OrganizationId);

            _logger.LogDebug(GetLogMessage("excluding {@ids}"), exclude);

            return await Repository.Queryable()
                .Include(x => x.Organization)
                .ThenInclude(x => x.OrganizationFinancialAccount)
                .Where(x => !exclude.Contains(x.Id) && x.Organization.OrganizationFinancialAccount != null && x.Discoverable)
                .ProjectTo<T>(ProjectionMapping).ToListAsync();
        }
    }
}