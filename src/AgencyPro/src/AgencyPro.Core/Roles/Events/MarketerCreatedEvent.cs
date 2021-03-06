﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.ViewModels.Marketers;

namespace AgencyPro.Core.Roles.Events
{
    public class MarketerCreatedEvent<T> : MarketerEvent<T> where T : MarketerOutput
    {
    }
}