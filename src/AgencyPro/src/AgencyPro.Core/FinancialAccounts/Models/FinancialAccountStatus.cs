// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Runtime.Serialization;

namespace AgencyPro.Core.FinancialAccounts.Models
{
    // todo: move this to flags 
    public enum FinancialAccountStatus
    {
        [EnumMember(Value = "Active")]
        Active = 0,

        [EnumMember(Value = "Inactive")]
        Inactive = 1
    }
}