// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.People.Enums;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationProjectManagers
{
    public class OrganizationProjectManagerOutput : OrganizationProjectManagerInput
    {
        
        public virtual string DisplayName { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsDefault { get; set; }
        public virtual PersonStatus Status { get; set; }
    }
}