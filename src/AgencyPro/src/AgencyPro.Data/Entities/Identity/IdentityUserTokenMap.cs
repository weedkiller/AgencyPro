// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Data.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Identity
{
    public class IdentityUserTokenMap : EntityMap<IdentityUserToken<Guid>>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
        {
            // Composite primary key consisting of the UserId, LoginProvider and Name
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of the composite key columns due to common DB restrictions
            builder.Property(t => t.LoginProvider).HasMaxLength(DataConstants.NormalMaxStringLength);
            builder.Property(t => t.Name).HasMaxLength(DataConstants.NormalMaxStringLength);

            // Maps to the AspNetUserTokens table
            builder.ToTable("UserToken");
        }
    }
}