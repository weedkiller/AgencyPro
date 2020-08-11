// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.UserAccount.Enums;

namespace AgencyPro.Core.UserAccount.Models
{
    public class MobileInfo
    {
        public string InstallationId { get; set; }
        public string ObjectId { get; set; }
        public MobileOS Os { get; set; }
        public string Version { get; set; }
        public string Device { get; set; }
        public MobileState State { get; set; }
        public string Token { get; set; }
    }
}