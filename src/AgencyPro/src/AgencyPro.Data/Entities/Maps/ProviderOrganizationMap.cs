// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProviderOrganizationMap : EntityMap<ProviderOrganization>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProviderOrganization> builder)
        {
            builder
                .HasOne(x => x.Organization)
                .WithOne(x => x.ProviderOrganization)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.Projects)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.ProjectManagerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(x => x.MarketingAgreements)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.ContractorOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Leads)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .IsRequired();

            builder.HasMany(x => x.Candidates)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .IsRequired();

            builder.HasMany(x => x.CustomerAccounts)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.AccountManagerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Skills)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(x => x.AccountManagerStream).HasColumnType("Money");
            builder.Property(x => x.ProjectManagerStream).HasColumnType("Money");
            builder.Property(x => x.AgencyStream).HasColumnType("Money");
            builder.Property(x => x.ContractorStream).HasColumnType("Money");

            builder.Property(x => x.FutureDaysAllowed).HasDefaultValue(0);
            builder.Property(x => x.PreviousDaysAllowed).HasDefaultValue(14);

            builder.Property(x => x.AccountManagerStream).HasColumnType("Money");
            builder.Property(x => x.ProjectManagerStream).HasColumnType("Money");
           
            builder.Property(x => x.SystemStream).HasColumnType("Money");
            builder.Property(x => x.ContractorStream).HasColumnType("Money");

            builder.HasMany(x => x.WorkOrders)
                .WithOne(x => x.ProviderOrganization)
                .HasForeignKey(x => x.AccountManagerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DefaultContractor)
                .WithMany(x => x.DefaultOrganizations)
                .HasForeignKey(x => new
                {
                    x.Id,
                    x.DefaultContractorId
                }).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DefaultProjectManager)
                .WithMany(x => x.DefaultOrganizations)
                .HasForeignKey(x => new
                {
                    x.Id,
                    x.DefaultProjectManagerId
                }).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DefaultAccountManager)
                .WithMany(x => x.DefaultOrganizations)
                .HasForeignKey(x => new
                {
                    x.Id,
                    x.DefaultAccountManagerId
                }).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            
            //builder.HasMany(x => x.Contractors)
            //    .WithOne(x => x.ProviderOrganization)
            //    .HasForeignKey(x => x.OrganizationId)
            //    .OnDelete(DeleteBehavior.Restrict);
            
            //builder.HasMany(x => x.ProjectManagers)
            //    .WithOne(x => x.ProviderOrganization)
            //    .HasForeignKey(x => x.OrganizationId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(x => x.AccountManagers)
            //    .WithOne(x => x.ProviderOrganization)
            //    .HasForeignKey(x => x.OrganizationId)
            //    .OnDelete(DeleteBehavior.Restrict);

            AddAuditProperties(builder);
        }

    }
}