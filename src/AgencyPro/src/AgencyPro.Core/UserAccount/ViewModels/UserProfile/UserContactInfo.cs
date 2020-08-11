// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.UserAccount.ViewModels.UserProfile
{
    public class UserContactInfo
    {
        private string _displayName;
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string DisplayName
        {
            get =>
                !string.IsNullOrEmpty(_displayName)
                    ? _displayName
                    : string.Format("{0} {1}", FirstName,
                        string.IsNullOrEmpty(LastName) ? string.Empty : LastName.Substring(1));
            set => _displayName = value;
        }
    }
}