// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers
{
    public class OrganizationAccountManagerInput : IOrganizationAccountManager
    {
        public virtual decimal AccountManagerStream { get; set; }
        public virtual Guid AccountManagerId { get; set; }
        public virtual Guid OrganizationId { get; set; }
    }
}