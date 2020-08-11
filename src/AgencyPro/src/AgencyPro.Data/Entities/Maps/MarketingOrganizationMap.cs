// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class MarketingOrganizationMap : EntityMap<MarketingOrganization>
    {
        public override void ConfigureInternal(EntityTypeBuilder<MarketingOrganization> builder)
        {
            builder
                .HasOne(x => x.Organization)
                .WithOne(x => x.MarketingOrganization)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.MarketerContracts)
                .WithOne(x => x.MarketerOrganization)
                .HasForeignKey(x => x.MarketerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.MarketerStream).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.MarketerBonus).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyBonus).HasColumnType("Money");

            builder.HasOne(x => x.DefaultOrganizationMarketer)
                .WithMany(x => x.OrganizationDefaults)
                .HasForeignKey(x => new
                {
                    x.Id,
                    DefaultOrganizationMarketerId = x.DefaultMarketerId
                }).IsRequired();

            builder.HasMany(x => x.MarketingAgreements)
                .WithOne(x => x.MarketingOrganization)
                .HasForeignKey(x => x.MarketingOrganizationId)
                .IsRequired();

            builder.Property(x => x.CombinedMarketingStream).HasComputedColumnSql(
                $@"[{nameof(MarketingOrganization.MarketerStream)}]+[{nameof(MarketingOrganization.MarketingAgencyStream)}]");

            builder.Property(x => x.CombinedMarketingBonus).HasComputedColumnSql(
                $@"[{nameof(MarketingOrganization.MarketerBonus)}]+[{nameof(MarketingOrganization.MarketingAgencyBonus)}]+[{nameof(MarketingOrganization.ServiceFeePerLead)}]");


            //builder.Ignore(x => x.CombinedMarketingStream);

            //builder.Ignore(x => x.CombinedMarketingBonus);

            AddAuditProperties(builder);

        }
    }
}