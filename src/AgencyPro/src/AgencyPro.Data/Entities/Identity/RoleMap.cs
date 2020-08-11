// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Data.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Identity
{
    public class RoleMap : EntityMap<Role>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Role> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("Role");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(DataConstants.NormalMaxStringLength);
            builder.Property(u => u.NormalizedName).HasMaxLength(DataConstants.NormalMaxStringLength);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<IdentityRoleClaim<Guid>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        }

        public override void SeedInternal(EntityTypeBuilder<Role> entity)
        {
            entity.HasData(new List<Role>()
            {
                new Role()
                {
                    Id = EntityConstants.SystemAdminRoleId,
                    Name = "Admin",
                    ConcurrencyStamp = EntityConstants.SystemAdminRoleId.ToString()
                },
                new Role()
                {
                    Id = EntityConstants.UserRoleId,
                    Name = "User",
                    ConcurrencyStamp = EntityConstants.UserRoleId.ToString()
                }
            });
        }
        
    }
}