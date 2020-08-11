﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Contracts.Enums
{
    public enum ContractStatus : byte
    {
        Pending = 0,
        Active = 1,
        Paused = 2,
        Ended = 4,
        Inactive = Paused | Ended

    }
}