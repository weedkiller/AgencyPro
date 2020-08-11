// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class FinancialAccountMap : EntityMap<FinancialAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<FinancialAccount> builder)
        {
            builder.HasKey(x => x.AccountId);

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.Property(x => x.AccountId).IsRequired();
            
            builder.HasOne(x => x.IndividualFinancialAccount)
                .WithOne(x => x.FinancialAccount)
                .HasForeignKey<IndividualFinancialAccount>(x => x.FinancialAccountId);

            builder.HasOne(x => x.OrganizationFinancialAccount)
                .WithOne(x => x.FinancialAccount)
                .HasForeignKey<OrganizationFinancialAccount>(x => x.FinancialAccountId);

            builder.HasMany(x => x.DestinationCharges)
                .WithOne(x => x.Destination)
                .HasForeignKey(x => x.DestinationId);

            builder.HasMany(x => x.Transfers)
                .WithOne(x => x.DestinationAccount)
                .HasForeignKey(x => x.DestinationId);

            builder.HasMany(x => x.Cards)
                .WithOne(x => x.FinancialAccount)
                .HasForeignKey(x=>x.AccountId);
            


            AddAuditProperties(builder);
        }
    }
}