// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeInvoiceItemMap : EntityMap<StripeInvoiceItem>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeInvoiceItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.InvoiceItems)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.InvoiceId);

            builder.HasMany(x => x.TimeEntries)
                .WithOne(x => x.InvoiceItem)
                .HasForeignKey(x => x.InvoiceItemId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Contract)
                .WithMany(x => x.InvoiceItems)
                .HasForeignKey(x => x.ContractId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(x => x.IndividualPayoutIntents)
                .WithOne(x => x.InvoiceItem)
                .HasForeignKey(x => x.InvoiceItemId)
                .IsRequired();

            builder.HasMany(x => x.OrganizationPayoutIntents)
                .WithOne(x => x.InvoiceItem)
                .HasForeignKey(x => x.InvoiceItemId)
                .IsRequired();

            builder.HasMany(x => x.InvoiceLines)
                .WithOne(x => x.InvoiceItem)
                .HasForeignKey(x => x.InvoiceItemId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            AddAuditProperties(builder);
        }
    }
}