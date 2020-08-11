// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;
using AgencyPro.Core.EventHandling;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.UserAccount.Events
{
    public abstract class UserAccountEvent : IEvent
    {
        public ApplicationUser Account { get; set; }
        public AppInformation AppInfo { get; set; }
        public AppBaseUrls Urls { get; set; }
    }
}