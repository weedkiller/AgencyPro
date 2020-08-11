// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.UserAccount.Enums;

namespace AgencyPro.Core.UserAccount.Models
{
    public class UserMobileDevice : AuditableEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Person Owner { get; set; }
        public string OS { get; set; }
        public string Token { get; set; }
        public string InstallationId { get; set; }
        public string ObjectId { get; set; }
        public string Device { get; set; }
        public string Version { get; set; }
        public bool Active { get; set; }
        public MobileState State { get; set; }
        public DateTimeOffset? StateUpdated { get; set; }
        public MobileOS MobileOs => GetMobileOs(OS);

        public static MobileOS GetMobileOs(string os)
        {
            switch (os.ToUpper())
            {
                case "IPHONE":
                case "IOS": return MobileOS.iOS;
                case "ANDROID": return MobileOS.Android;
                case "BLACKBERRY": return MobileOS.BlackBerry;
                case "WINDOWS": return MobileOS.WindowsPhone;
                default:
                    return MobileOS.Other;
            }
        }
    }
}