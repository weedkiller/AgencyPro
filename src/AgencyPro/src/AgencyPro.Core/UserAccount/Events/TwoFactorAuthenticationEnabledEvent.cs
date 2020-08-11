// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.UserAccount.Enums;

namespace AgencyPro.Core.UserAccount.Events
{
    public class TwoFactorAuthenticationEnabledEvent : UserAccountEvent
    {
        public TwoFactorAuthMode Mode { get; set; }
    }
}