// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeSessionCheckoutMap : EntityMap<StripeCheckoutSession>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeCheckoutSession> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.CheckoutSessions)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            AddAuditProperties(builder);
        }
    }
}