// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.PaymentIntents.Models;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeInvoiceMap : EntityMap<StripeInvoice>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeInvoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.Property(x => x.Total).HasColumnType("Money");
            builder.Property(x => x.Subtotal).HasColumnType("Money");
            builder.Property(x => x.AmountRemaining).HasColumnType("Money");
            builder.Property(x => x.AmountDue).HasColumnType("Money");
            builder.Property(x => x.AmountPaid).HasColumnType("Money");

            builder.HasOne(x => x.BuyerAccount)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId);

            builder.HasOne(x => x.PaymentIntent)
                .WithOne(x => x.StripeInvoice)
                .HasForeignKey<StripePaymentIntent>(x => x.InvoiceId);

            builder.HasOne(x => x.ProjectInvoice)
                .WithOne(x => x.Invoice)
                .HasForeignKey<ProjectInvoice>(x => x.InvoiceId);

            builder.HasOne(x => x.SubscriptionInvoice)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.SubscriptionId);

            builder.HasMany(x => x.Lines)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            builder.HasMany(x => x.Charges)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            builder.HasMany(x => x.IndividualPayoutIntents)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            builder.HasMany(x => x.OrganizationPayoutIntents)
                .WithOne(x => x.Invoice)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            AddAuditProperties(builder);
        }
    }
}