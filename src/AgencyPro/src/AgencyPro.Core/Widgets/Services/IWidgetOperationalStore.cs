// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.TimeEntries.ViewModels;
using AgencyPro.Core.Widgets.ViewModels;

namespace AgencyPro.Core.Widgets.Services
{
    public interface IWidgetOperationalStore
    {
        Task<AgencyStreamWidget> AgencyStreamsWidget(IAgencyOwner ao, TimeMatrixFilters filters = null);
        Task<AgencyTimeMatrixWidget> TimeMatrixWidget(IAgencyOwner ao, TimeMatrixFilters filters = null);
        Task<AgencySummaryWidget> AgencySummaryWidget(IAgencyOwner ao);
    }
}