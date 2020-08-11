// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.Roles.ViewModels.Contractors;

namespace AgencyPro.Core.Roles.Events
{
    public abstract class ContractorEvent<T> : BaseEvent where T : ContractorOutput
    {
        // notify recruiter

        public T Contractor { get; set; }
    }
}