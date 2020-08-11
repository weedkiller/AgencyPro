// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.Organizations.ViewModels;

namespace AgencyPro.Core.Organizations.Events
{
    public abstract class OrganizationEvent<T> : BaseEvent where T : OrganizationOutput
    {
        public T Organization { get; set; }
    }
}