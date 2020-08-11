// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationCustomerMap : EntityMap<OrganizationCustomer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationCustomer> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.CustomerId
                });

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.HasQueryFilter(x => x.IsDeleted == false);


            builder.HasOne(x => x.Customer)
                .WithMany(x => x.OrganizationCustomers)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder
                .HasMany(x => x.Accounts)
                .WithOne(x => x.OrganizationCustomer)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                }).IsRequired();
            

            builder
                .HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.Customer).HasForeignKey<OrganizationCustomer>(x => new
                {
                    x.OrganizationId,
                    x.CustomerId
                })
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);

        }

    }
}