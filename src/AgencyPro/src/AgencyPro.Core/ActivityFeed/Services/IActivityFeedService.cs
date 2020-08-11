// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.ActivityFeed.ViewModels;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.ActivityFeed.Services
{
    public interface IActivityFeedService
    {
        List<ActivityFeedOutput> GetActivityFeed(IProviderAgencyOwner ao, ActivityFeedFilters filters);
    }
}
