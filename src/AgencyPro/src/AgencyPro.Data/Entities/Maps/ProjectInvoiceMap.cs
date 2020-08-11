// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProjectInvoiceMap : EntityMap<ProjectInvoice>
    {
       
        public override void ConfigureInternal(EntityTypeBuilder<ProjectInvoice> builder)
        {
            builder.HasKey(x => x.InvoiceId);
            builder.Property(x => x.InvoiceId).IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired();

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.ProjectManager)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.ProjectManagerId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.ProviderInvoices)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.BuyerOrganization)
                .WithMany(x => x.BuyerInvoices)
                .HasForeignKey(x => x.BuyerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.ProjectManagerId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.AccountManagerId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => new
                {
                    x.BuyerOrganizationId,
                    x.CustomerId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.ProjectManagerId
                })
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasOne(x => x.CustomerAccount)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => new
                {
                    x.BuyerOrganizationId,
                    x.CustomerId,
                    x.ProviderOrganizationId,
                    x.AccountManagerId
                }).IsRequired();

            builder.HasOne(x => x.Invoice)
                .WithOne(x => x.ProjectInvoice)
                .HasForeignKey<ProjectInvoice>(x => x.InvoiceId);
            
            AddAuditProperties(builder, true);
        }
    }
}