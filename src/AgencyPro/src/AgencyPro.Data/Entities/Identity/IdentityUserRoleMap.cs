// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Data.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Identity
{
    public class IdentityUserRoleMap : EntityMap<IdentityUserRole<Guid>>
    {
        public override void SeedInternal(EntityTypeBuilder<IdentityUserRole<Guid>> entity)
        {
            entity.HasData(new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>()
                {
                    RoleId = EntityConstants.SystemAdminRoleId,
                    UserId = EntityConstants.SystemAdminUserId
                },
                new IdentityUserRole<Guid>()
                {
                    RoleId = EntityConstants.UserRoleId,
                    UserId = EntityConstants.SystemAdminUserId
                }
            });
        }

        public override void ConfigureInternal(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(r => new { r.UserId, r.RoleId });
        }
    }
}