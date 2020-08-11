// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BuyerAccounts.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class BuyerAccountMap : EntityMap<BuyerAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<BuyerAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Balance).HasColumnType("Money");

            builder.HasOne(x => x.IndividualBuyerAccount)
                .WithOne(x => x.BuyerAccount)
                .HasForeignKey<IndividualBuyerAccount>(x => x.BuyerAccountId);

            builder.HasOne(x => x.OrganizationBuyerAccount)
                .WithOne(x => x.BuyerAccount)
                .HasForeignKey<OrganizationBuyerAccount>(x => x.BuyerAccountId);

            builder.HasMany(x => x.Invoices)
                .WithOne(x => x.BuyerAccount)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.PaymentSources)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.InvoiceItems)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.CheckoutSessions)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Charges)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Cards)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            AddAuditProperties(builder);
        }
    }
}