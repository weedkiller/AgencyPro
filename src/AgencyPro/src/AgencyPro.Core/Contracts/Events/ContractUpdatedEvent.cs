// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Enums;

namespace AgencyPro.Core.Contracts.Events
{
    public class ContractUpdatedEvent : ContractEvent
    {
        // contractor
        // account manager
        // project manager
        // customer

        public ContractUpdatedEvent()
        {
            Action = ModelAction.Update;
        }
    }
}