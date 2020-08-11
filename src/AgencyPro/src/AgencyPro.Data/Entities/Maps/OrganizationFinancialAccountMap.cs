// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationFinancialAccountMap : EntityMap<OrganizationFinancialAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationFinancialAccount> builder)
        {
            builder.HasOne(x => x.Organization)
                .WithOne(x => x.OrganizationFinancialAccount)
                .HasForeignKey<OrganizationFinancialAccount>(x=>x.Id)
                .IsRequired(true);

            builder.HasOne(x => x.FinancialAccount)
                .WithOne(x => x.OrganizationFinancialAccount)
                .HasForeignKey<OrganizationFinancialAccount>(x => x.FinancialAccountId)
                .IsRequired(true);

            AddAuditProperties(builder);
        }
    }
}