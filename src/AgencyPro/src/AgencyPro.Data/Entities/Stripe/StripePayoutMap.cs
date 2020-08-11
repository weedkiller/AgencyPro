﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripePayoutMap : EntityMap<StripePayout>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripePayout> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            AddAuditProperties(builder);


            builder.HasMany(x => x.BalanceTransactions)
                .WithOne(x => x.Payout)
                .HasForeignKey(x => x.PayoutId)
                .IsRequired(false);
        }
    }
}