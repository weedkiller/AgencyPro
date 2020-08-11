// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.People.Enums;
using Newtonsoft.Json;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers
{
    public class CustomerOrganizationAccountManagerOutput 
        : OrganizationAccountManagerOutput
    {
        [JsonIgnore]
        public override decimal AccountManagerStream { get; set; }

        [JsonIgnore]
        public override PersonStatus Status { get; set; }
    }
}