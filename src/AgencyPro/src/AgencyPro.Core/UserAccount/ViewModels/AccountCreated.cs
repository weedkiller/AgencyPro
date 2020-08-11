// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Config;

namespace AgencyPro.Core.UserAccount.ViewModels
{
    public class AccountCreated
    {
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public AppInformation AppInfo { get; set; }
    }
}