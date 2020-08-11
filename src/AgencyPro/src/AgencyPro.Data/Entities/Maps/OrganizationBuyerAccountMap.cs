// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationBuyerAccountMap : EntityMap<OrganizationBuyerAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationBuyerAccount> builder)
        {
          
            builder.HasOne(x => x.BuyerAccount)
                .WithOne(x => x.OrganizationBuyerAccount)
                .HasForeignKey<OrganizationBuyerAccount>(x=>x.BuyerAccountId)
                .IsRequired(true);

            builder.HasOne(x => x.Organization)
                .WithOne(x => x.OrganizationBuyerAccount)
                .HasForeignKey<OrganizationBuyerAccount>(x => x.Id)
                .IsRequired(true);
        }
    }
}