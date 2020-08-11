// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Roles
{
    public class CustomerMap : EntityMap<Customer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasOne(x => x.Person)
                .WithOne(x => x.Customer);
            
            builder
                .HasOne(x => x.OrganizationMarketer)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => new
                {
                    x.MarketerOrganizationId,
                    x.MarketerId
                });

            builder.HasMany(x => x.OrganizationCustomers)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.BuyerAccount)
                .WithOne(x => x.Customer)
                .HasForeignKey<IndividualBuyerAccount>(x => x.Id);

            AddAuditProperties(builder);

        }
        
    }
}