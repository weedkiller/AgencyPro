// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationMarketerMap : EntityMap<OrganizationMarketer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationMarketer> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.MarketerId
                });

            builder.Property(x => x.MarketerStream).HasColumnType("Money");
            builder.Property(x => x.MarketerBonus).HasColumnType("Money");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.HasQueryFilter(x => x.IsDeleted == false);



            builder
                .HasMany(x => x.Contracts)
                .WithOne(x => x.OrganizationMarketer)
                .HasForeignKey(x => new
                {
                    x.MarketerOrganizationId,
                    x.MarketerId
                });

            builder
                .HasMany(x => x.Leads)
                .WithOne(x => x.OrganizationMarketer)
                .HasForeignKey(x => new
                {
                    x.MarketerOrganizationId,
                    x.MarketerId
                });

            builder
                .HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.Marketer)
                .HasForeignKey<OrganizationMarketer>(x => new
                {
                    x.OrganizationId,
                    x.MarketerId
                })
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);

        }


    }
}