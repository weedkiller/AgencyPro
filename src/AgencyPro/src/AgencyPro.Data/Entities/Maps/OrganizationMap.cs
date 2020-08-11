// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationMap : EntityMap<Organization>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.IndividualPayoutIntents)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);

            builder.Property(x => x.PrimaryColor).HasMaxLength(50);
            builder.Property(x => x.SecondaryColor).HasMaxLength(50);
            builder.Property(x => x.TertiaryColor).HasMaxLength(50);

            builder.Property(x => x.ColumnBgColor).HasMaxLength(50);
            builder.Property(x => x.HoverItemColor).HasMaxLength(50);
            builder.Property(x => x.TextColor).HasMaxLength(50);
            builder.Property(x => x.MenuBgHoverColor).HasMaxLength(50);
            builder.Property(x => x.ActiveItemColor).HasMaxLength(50);
            builder.Property(x => x.ActivePresenceColor).HasMaxLength(50);
            builder.Property(x => x.ActiveItemTextColor).HasMaxLength(50);
            builder.Property(x => x.MentionBadgeColor).HasMaxLength(50);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.OwnedAgencies)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ImageUrl).HasMaxLength(2000);

            builder.HasMany(x => x.BuyerProjects)
                .WithOne(x => x.BuyerOrganization)
                .HasForeignKey(x => x.CustomerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasMany(x => x.Customers)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.Recruiters)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);


            builder.HasMany(x => x.Marketers)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.Candidates)
                .WithOne(x => x.RecruiterOrganization)
                .HasForeignKey(x => x.RecruiterOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.BuyerWorkOrders)
                .WithOne(x => x.BuyerOrganization)
                .HasForeignKey(x => x.CustomerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.OrganizationSubscription)
                .WithOne(x => x.Organization)
                .HasForeignKey<OrganizationSubscription>(x => x.Id);

            builder.HasOne(x => x.OrganizationBuyerAccount)
                .WithOne(x => x.Organization)
                .HasForeignKey<OrganizationBuyerAccount>(x => x.Id);

            builder.HasMany(x => x.PayoutIntents)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired();


            AddAuditProperties(builder);
        }
    }
}