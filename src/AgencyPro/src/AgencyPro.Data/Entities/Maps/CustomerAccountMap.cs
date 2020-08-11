// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AgencyPro.Data.Entities.Maps
{
    public class CustomerAccountMap : EntityMap<CustomerAccount>
    {
        public override void ConfigureInternal(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasIndex("AccountManagerOrganizationId", "Number")
                .HasName("AccountNumberIndex").IsUnique();

            builder.HasIndex("CustomerOrganizationId", "BuyerNumber")
                .HasName("BuyerNumberIndex").IsUnique();

            builder.Property(x => x.IsDeactivated).HasComputedColumnSql(
                @"case when (coalesce([AgencyOwnerDeactivationDate],[AccountManagerDeactivationDate],[CustomerDeactivationDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

            builder.Property(x => x.IsInternal)
                .HasComputedColumnSql(@"case when [AccountManagerOrganizationId]=[CustomerOrganizationId] then cast(1 as bit) else cast(0 as bit) end");

            builder.Property(x => x.IsCorporate)
                .HasComputedColumnSql(@"case when [AccountManagerOrganizationId]=[CustomerOrganizationId] AND [AccountManagerId]=[CustomerId] then cast(1 as bit) else cast(0 as bit) end");
            
            builder
                .HasKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.CustomerAccounts)
                .HasForeignKey(x=>x.AccountManagerId)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.CustomerAccounts)
                .HasForeignKey(x=>x.CustomerId)
                .IsRequired();

            builder.HasMany(x=>x.Contracts)
                .WithOne(x=>x.CustomerAccount)
                .HasForeignKey(x => new
                {
                    x.BuyerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired();

            builder
                .HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => new
                {
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });

            builder
                .HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                });

            builder.Property(x => x.PaymentTermId).HasDefaultValue(1);

            builder.HasOne(x => x.PaymentTerm)
                .WithMany(x => x.CustomerAccounts)
                .HasForeignKey(x => x.PaymentTermId);

            builder.HasMany(x => x.Projects).WithOne(x => x.CustomerAccount).HasForeignKey(x => new
            {
                x.CustomerOrganizationId,
                x.CustomerId,
                x.AccountManagerOrganizationId,
                x.AccountManagerId
            }).IsRequired();

            builder.HasMany(x => x.WorkOrders).WithOne(x => x.CustomerAccount).HasForeignKey(x => new
            {
                x.CustomerOrganizationId,
                x.CustomerId,
                x.AccountManagerOrganizationId,
                x.AccountManagerId
            }).IsRequired();

            builder.HasMany(x => x.Comments).WithOne(x => x.CustomerAccount).HasForeignKey(x => new
            {
                x.CustomerOrganizationId,
                x.CustomerId,
                x.AccountManagerOrganizationId,
                x.AccountManagerId
            }).IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.CustomerAccounts)
                .HasForeignKey(x => x.AccountManagerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BuyerOrganization)
                .WithMany(x => x.BuyerCustomerAccounts)
                .HasForeignKey(x => x.CustomerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });


            builder.Property(x => x.MarketerStream).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyStream).HasColumnType("Money");

            AddAuditProperties(builder);
        }
        
    }
}