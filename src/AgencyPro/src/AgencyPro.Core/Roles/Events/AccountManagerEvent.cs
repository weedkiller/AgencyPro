// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Events;
using AgencyPro.Core.Roles.ViewModels.AccountManagers;

namespace AgencyPro.Core.Roles.Events
{
    public abstract class AccountManagerEvent : BaseEvent
    {
        public AccountManagerOutput AccountManager { get; set; }
    }
}