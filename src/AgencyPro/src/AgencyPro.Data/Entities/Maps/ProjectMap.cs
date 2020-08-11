// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProjectMap : EntityMap<Project>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex("AccountManagerOrganizationId", "Abbreviation")
                .HasName("ProjectAbbreviationIndex").IsUnique();

            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.ProjectId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            builder
                .HasMany(x => x.Stories)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired();

            builder
                .HasOne(x => x.AccountManager)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired();

            builder
                .HasOne(x => x.ProjectManager)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ProjectManagerId)
                .IsRequired();

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ProjectManagerOrganizationId)
                .IsRequired();

            builder.HasMany(x => x.ProjectBillingCategories)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();


            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder
                .HasOne(x => x.CustomerAccount)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired();

            builder
                .HasMany(x => x.Invoices)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => new
                {
                    x.ProjectManagerOrganizationId,
                    x.ProjectManagerId
                });

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => new
                {
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                });

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.CustomerId);

            AddAuditProperties(builder);
        }
        
    }
}