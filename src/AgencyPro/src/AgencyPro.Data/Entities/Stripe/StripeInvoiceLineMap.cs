// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeInvoiceLineMap : EntityMap<StripeInvoiceLine>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeInvoiceLine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired();

            builder.HasOne(x => x.InvoiceItem)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.InvoiceItemId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}