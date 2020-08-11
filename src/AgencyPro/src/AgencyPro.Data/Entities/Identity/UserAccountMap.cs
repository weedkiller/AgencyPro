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
    public class UserAccountMap : EntityMap<ApplicationUser>
    {
    //    public override void SeedInternal(EntityTypeBuilder<ApplicationUser> entity)
    //    {
    //        var username = "admin";

    //        entity.HasData(new List<ApplicationUser>
    //        {
    //            new ApplicationUser
    //            {
    //                Id = EntityConstants.SystemAdminUserId,
    //                UserName = username,
    //                NormalizedEmail = "rod@agencypro.com",
    //                PasswordHash = "AQAAAAEAACcQAAAAEKAX8kRGawi1dqKgUENfN6A5gfblwBd48OhsoHr+Xjpk/kBqu+b5OK6qJm616Nq2Bg==",
    //                Email = "rod@agencypro.com",
    //                EmailConfirmed = true,
    //                LockoutEnabled = false,
    //                NormalizedUserName = username,
    //                PhoneNumber = "123-321-4321",
    //                Created = EntityConstants.DefaultDateTime,
    //                ConcurrencyStamp = EntityConstants.SystemAdminUserId.ToString(),
    //                SecurityStamp = EntityConstants.SystemAdminUserId.ToString(),
    //                IsAdmin = true
    //            },
    //            new ApplicationUser
    //            {
    //                Id = EntityConstants.MarketerOnlyId,
    //                UserName = "marketer",
    //                PasswordHash = "AQAAAAEAACcQAAAAEKxU4j9XOqEdFdvh5TR9DtkhiQ1+nK/NY3WnMKZxsRa+/wpvbwbTyWyQENIVlTb6pQ==",
    //                Email = "marketer@agencypro.com",
    //                EmailConfirmed = true,
    //                LockoutEnabled = false,
    //                NormalizedUserName = "marketer",
    //                NormalizedEmail = "marketer@agencypro.com",
    //                Created = EntityConstants.DefaultDateTime,
    //                PhoneNumber = "123-123-1234",
    //                PhoneNumberConfirmed = true,
    //                ConcurrencyStamp = EntityConstants.MarketerOnlyId.ToString(),
    //                SecurityStamp = EntityConstants.MarketerOnlyId.ToString()

    //            }
    //        });
    //    }

        public override void ConfigureInternal(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.ToTable("UserAccount");

            builder
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();


            builder
                .Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(DataConstants.NormalMaxStringLength);

            builder
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(DataConstants.EmailMaxStringLength);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();
            
            builder
                .Property(p => p.PhoneNumber)
                .HasMaxLength(DataConstants.PhoneNumberMaxStringLength);

            builder
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
            
            builder
                .HasMany(x => x.ExceptionsRaised)
                .WithOne(u => u.ApplicationUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.AuditLogs)
                .WithOne(u => u.ApplicationUser)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .Property(s => s.Created)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder
                .Property(s => s.LastUpdated)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            //builder.HasMany(p => p.UserRoles).WithOne()
            //    .HasForeignKey(p => p.UserId)
            //    .IsRequired();

            builder.Ignore(x => x.ObjectState);

            //builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            builder.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
            builder.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            builder.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();


        }
    }
}