// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers;

namespace AgencyPro.Core.OrganizationRoles.Events.OrganizationMarketers
{
    public class OrganizationMarketerUpdatedEvent<T> : OrganizationMarketerEvent<T> where T : OrganizationMarketerOutput
    {
    }
}