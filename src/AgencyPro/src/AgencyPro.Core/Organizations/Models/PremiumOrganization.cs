// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Organizations.Models
{
    public class PremiumOrganization : AuditableEntity
    {
        [ForeignKey("Id")] public Organization Organization { get; set; }

    }
}