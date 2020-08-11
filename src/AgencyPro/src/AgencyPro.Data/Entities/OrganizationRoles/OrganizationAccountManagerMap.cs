// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationAccountManagerMap : EntityMap<OrganizationAccountManager>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationAccountManager> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.AccountManagerId
                });

            builder.Property(x => x.AccountManagerStream).HasColumnType("Money");
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.HasQueryFilter(x => x.IsDeleted == false);


            builder
                .HasMany(x => x.Accounts)
                .WithOne(x => x.OrganizationAccountManager)
                .HasForeignKey(x => new
                {
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });

            builder
                .HasMany(x => x.Leads)
                .WithOne(x => x.OrganizationAccountManager)
                .HasForeignKey(x => new
                {
                    OrganizationId = x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired(false);

            builder
                .HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.AccountManager)
                .HasForeignKey<OrganizationAccountManager>(x => new
                {
                    x.OrganizationId,
                    x.AccountManagerId
                })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.AccountManagers)
                .HasForeignKey(x => x.OrganizationId);

            AddAuditProperties(builder);
        }

        
    }
}