// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Roles.Services;

namespace AgencyPro.Core.Roles.ViewModels.AccountManagers
{
    public class AccountManagerOutput : AccountManagerInput, IAccountManager
    {
        public virtual Guid Id { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Updated { get; set; }
    }
}