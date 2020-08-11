// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.People.ViewModels
{
    public class RemovePersonInput
    {
        public bool RemoveContractor { get; set; }
        public bool RemoveProjectManager { get; set; }
        public bool RemoveAccountManager { get; set; }
        public bool RemoveMarketer { get; set; }
        public bool RemoveRecruiter { get; set; }
    }
}