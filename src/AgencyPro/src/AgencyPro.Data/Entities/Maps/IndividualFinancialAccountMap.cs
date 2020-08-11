// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class IndividualFinancialAccountMap : EntityMap<IndividualFinancialAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IndividualFinancialAccount> builder)
        {
            builder.HasOne(x => x.Person)
                .WithOne(x => x.IndividualFinancialAccount)
                .HasForeignKey<IndividualFinancialAccount>(x=>x.Id)
                .IsRequired();

            builder.HasOne(x => x.FinancialAccount)
                .WithOne(x => x.IndividualFinancialAccount)
                .HasForeignKey<IndividualFinancialAccount>(x => x.FinancialAccountId)
                .IsRequired();

            AddAuditProperties(builder);
        }
    }
}