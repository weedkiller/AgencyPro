// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Roles.Services
{
    public interface IAccountManagerService : IService<AccountManager>,
        IRoleService<AccountManagerInput, AccountManagerUpdateInput, AccountManagerOutput, IAccountManager>
    {
    }
}