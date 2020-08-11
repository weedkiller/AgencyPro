// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class ProjectRetainerMap : EntityMap<ProjectRetainerIntent>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProjectRetainerIntent> builder)
        {
            builder.HasKey(x => x.ProjectId);

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.RetainerIntents)
                .HasForeignKey(x=>x.AccountManagerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerAccount)
                .WithMany(x => x.RetainerIntents)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId,
                    x.ProviderOrganizationId,
                    x.AccountManagerId
                })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.RetainerIntents)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.RetainerIntents)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.AccountManagerId
                })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.RetainerIntents)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                })
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CustomerOrganization)
                .WithMany(x => x.BuyerRetainerIntents)
                .HasForeignKey(x => x.CustomerOrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            
            
            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.ProviderRetainerIntents)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Project)
                .WithOne(x => x.ProjectRetainerIntent)
                .HasForeignKey<ProjectRetainerIntent>(x => x.ProjectId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Credits)
                .WithOne(x => x.RetainerIntent)
                .HasForeignKey(x => x.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}