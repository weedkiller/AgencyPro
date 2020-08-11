// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.Extensions;

namespace AgencyPro.Core.UserAccount.ViewModels.UserProfile
{
    public class UserProfileInput
    {
        private string _displayName;

        [Required] public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName
        {
            get =>
                !string.IsNullOrEmpty(_displayName)
                    ? _displayName
                    : FirstName.GetDisplayName(LastName);
            set => _displayName = value;
        }

        public string Avatar { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
        public string City { get; set; }
    }
}