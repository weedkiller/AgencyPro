// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

namespace AgencyPro.Core.EventHandling
{
    public class AggregateEventBus : List<IEventBus>, IEventBus
    {
        public void RaiseEvent(IEvent evt)
        {
            foreach (var eb in this) eb.RaiseEvent(evt);
        }
    }
}