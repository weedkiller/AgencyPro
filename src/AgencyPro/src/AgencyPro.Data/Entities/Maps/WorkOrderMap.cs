// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Orders.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class WorkOrderMap : EntityMap<WorkOrder>
    {
        public override void ConfigureInternal(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex("AccountManagerOrganizationId", "ProviderNumber")
                .HasName("ProviderNumberIndex").IsUnique();

            builder.HasIndex("CustomerOrganizationId", "BuyerNumber")
                .HasName("BuyerNumberIndex").IsUnique();

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired(true);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired(true);

            builder.HasOne(x => x.CustomerAccount)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired();

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => new
                {
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.AccountManagerOrganizationId);

            builder.HasQueryFilter(x => x.IsDeleted == false);

            AddAuditProperties(builder);


        }
    }
}