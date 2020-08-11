// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class StripeCardMap : EntityMap<StripeCard>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeCard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.Property(x => x.Id).IsRequired();

            builder.HasOne(x => x.AccountCard)
                .WithOne(x => x.StripeCard)
                .HasForeignKey<AccountCard>(x => x.Id);

            builder.HasOne(x => x.CustomerCard)
                .WithOne(x => x.StripeCard)
                .HasForeignKey<CustomerCard>(x => x.Id);

            AddAuditProperties(builder);
        }
    }
}