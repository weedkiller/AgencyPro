// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors;

namespace AgencyPro.Core.OrganizationRoles.Events.OrganizationContractors
{
    public abstract class OrganizationContractorEvent : BaseEvent
    {
        public OrganizationContractorOutput OrganizationContractor { get; set; }
    }
}