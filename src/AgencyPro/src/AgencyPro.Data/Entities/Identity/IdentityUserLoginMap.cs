// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Data.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Identity
{
    public class IdentityUserLoginMap : EntityMap<IdentityUserLogin<Guid>>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
        {
            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of the composite key columns due to common DB restrictions
            builder.Property(l => l.LoginProvider).HasMaxLength(128);
            builder.Property(l => l.ProviderKey).HasMaxLength(128);

            // Maps to the AspNetUserLogins table
            builder.ToTable("UserLogin");
        }
    }
}