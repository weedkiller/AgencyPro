﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Enums;

namespace AgencyPro.Core.Proposals.Events
{
    public class ProposalCreatedEvent : ProposalEvent
    {
        public ProposalCreatedEvent()
        {
            Action = ModelAction.Create;
        }
    }
}