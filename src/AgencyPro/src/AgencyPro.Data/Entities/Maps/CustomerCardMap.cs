// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Cards.Models;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CustomerCardMap : EntityMap<CustomerCard>
    {
        public override void ConfigureInternal(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);
            
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Cards)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.StripeCard)
                .WithOne(x => x.CustomerCard)
                .HasForeignKey<CustomerCard>(x => x.Id)
                .IsRequired();

            AddAuditProperties(builder);

        }
    }
}