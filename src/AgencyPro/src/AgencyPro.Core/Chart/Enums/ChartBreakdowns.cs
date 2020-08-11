// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Chart.Enums
{
    [Flags]
    public enum ChartBreakdowns : byte 
    {
        None=0,
        PROJ = 1,
        MA = 2,
        RE = 4,
        AM = 8,
        PM = 16,
        CO = 32, //Contractor
        CONT = 64,//Contract
    }
}
