// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel;

namespace AgencyPro.Core.Enums
{
    public enum ModelType
    {
        [Description("Application Settings")] AppSettings = 0,
        [Description("Application Audience")] Audience = 1,
        [Description("User Profile")] UserProfile = 2,
        [Description("User Avatar")] UserAvatar = 3,
        [Description("Enabled Country")] EnabledCountry,
        [Description("Country Language")] CountryLanguage,
        [Description("Country")] Country,
        [Description("Notebook")] Notebook,
        [Description("Notification")] Notification,
        [Description("Exception")] ExceptionLog = 90,
        [Description("General")] Unknown = 100
    }
}