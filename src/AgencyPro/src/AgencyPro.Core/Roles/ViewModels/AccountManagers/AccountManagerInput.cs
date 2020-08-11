// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Roles.ViewModels.AccountManagers
{
    public class AccountManagerInput : AccountManagerUpdateInput
    {
        public Guid PersonId { get; set; }
    }
}