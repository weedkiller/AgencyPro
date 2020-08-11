// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class BonusTransferMap : EntityMap<BonusTransfer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<BonusTransfer> builder)
        {
            builder.HasKey(x => x.TransferId);
            builder.HasOne(x => x.Transfer)
                .WithOne(x => x.BonusTransfer)
                .HasForeignKey<BonusTransfer>(x => x.TransferId)
                .IsRequired();

            builder.HasMany(x => x.IndividualBonusIntents)
                .WithOne(x => x.BonusTransfer)
                .HasForeignKey(x => x.TransferId)
                .IsRequired(false);

            builder.HasMany(x => x.OrganizationBonusIntents)
                .WithOne(x => x.BonusTransfer)
                .HasForeignKey(x => x.TransferId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}