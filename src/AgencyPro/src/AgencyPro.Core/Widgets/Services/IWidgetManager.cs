// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Services;
using AgencyPro.Core.Widgets.Models;

namespace AgencyPro.Core.Widgets.Services
{
    public interface IWidgetManager : IService<CategoryWidget>, IWidgetConfigurationStore, IWidgetOperationalStore
    {

    }
}
