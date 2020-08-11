// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.UserAccount.ViewModels.UserProfile
{
    public class UserProfileOutput : UserProfileInput
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastLogin { get; set; }
    }
}