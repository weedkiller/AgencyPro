// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class MarketingAgreementMap : EntityMap<MarketingAgreement>
    {
        public override void ConfigureInternal(EntityTypeBuilder<MarketingAgreement> builder)
        {
            builder.HasKey(x => new
            {
                x.ProviderOrganizationId,
                x.MarketingOrganizationId
            });

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.MarketingAgreements)
                .HasForeignKey(x => x.ProviderOrganizationId);

            builder.Property(x => x.MarketerBonus).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.MarketerStream).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyBonus).HasColumnType("Money");


            var marketingStreamComputation = $@"[{nameof(MarketingAgreement.MarketingAgencyStream)}]+[{nameof(MarketingAgreement.MarketerStream)}]";
            var marketingBonusComputation = $@"[{nameof(MarketingAgreement.MarketerBonus)}]+[{nameof(MarketingAgreement.MarketingAgencyBonus)}]";

            builder.Property(x => x.MarketingStream).HasComputedColumnSql(marketingStreamComputation);
            builder.Property(x => x.MarketingBonus).HasComputedColumnSql(marketingBonusComputation);

            AddAuditProperties(builder);

        }
    }
}