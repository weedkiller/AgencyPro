﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Events;

namespace AgencyPro.Core.People.Events
{
    public class PersonEvent : BaseEvent
    {
        public Guid PersonId { get; set; }
    }
}