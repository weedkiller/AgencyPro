// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class InvoiceTransferMap : EntityMap<InvoiceTransfer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<InvoiceTransfer> builder)
        {
            builder.HasKey(x => x.TransferId);
            builder.HasOne(x => x.Transfer)
                .WithOne(x => x.InvoiceTransfer)
                .HasForeignKey<InvoiceTransfer>(x => x.TransferId)
                .IsRequired();

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoiceTransfers)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            builder.HasMany(x => x.IndividualPayoutIntents)
                .WithOne(x => x.InvoiceTransfer)
                .HasForeignKey(x => x.InvoiceTransferId)
                .IsRequired(false);

            builder.HasMany(x => x.OrganizationPayoutIntents)
                .WithOne(x => x.InvoiceTransfer)
                .HasForeignKey(x => x.InvoiceTransferId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}