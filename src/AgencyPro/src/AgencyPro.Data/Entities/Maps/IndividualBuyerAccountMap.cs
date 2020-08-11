// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class IndividualBuyerAccountMap : EntityMap<IndividualBuyerAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IndividualBuyerAccount> builder)
        {
            builder.HasOne(x => x.Customer)
                .WithOne(x => x.BuyerAccount)
                .HasForeignKey<IndividualBuyerAccount>(x => x.Id)
                .IsRequired();

            builder.HasOne(x => x.BuyerAccount)
                .WithOne(x => x.IndividualBuyerAccount)
                .HasForeignKey<IndividualBuyerAccount>(x => x.BuyerAccountId)
                .IsRequired();

            AddAuditProperties(builder);
        }
    }
}