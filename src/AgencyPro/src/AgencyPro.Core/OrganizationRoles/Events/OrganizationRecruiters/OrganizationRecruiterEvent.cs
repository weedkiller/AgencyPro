// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationRecruiters;

namespace AgencyPro.Core.OrganizationRoles.Events.OrganizationRecruiters
{
    public abstract class OrganizationRecruiterEvent<T> : BaseEvent where T : OrganizationRecruiterOutput
    {
        public T OrganizationMarketerOutput { get; set; }
    }
}