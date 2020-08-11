// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Widgets.ViewModels;

namespace AgencyPro.Core.Widgets.Services
{
    public interface IWidgetConfigurationStore
    {
        Task<List<CategoryWidgetOutput>> GetWidgets(IAgencyOwner ao, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationCustomer cu, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationRecruiter re, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationMarketer ma, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationPerson pe, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationContractor co, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationProjectManager pm, WidgetFilters filters = null);
        Task<List<CategoryWidgetOutput>> GetWidgets(IOrganizationAccountManager am, WidgetFilters filters = null);
    }
}