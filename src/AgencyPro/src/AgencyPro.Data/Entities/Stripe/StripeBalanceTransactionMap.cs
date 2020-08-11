// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Transactions.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeBalanceTransactionMap : EntityMap<StripeBalanceTransaction>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeBalanceTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            AddAuditProperties(builder);

            builder.HasOne(x => x.Payout)
                .WithMany(x => x.BalanceTransactions)
                .HasForeignKey(x => x.PayoutId)
                .IsRequired(false);
        }
    }
}