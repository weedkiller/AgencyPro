// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationContractorMap : EntityMap<OrganizationContractor>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationContractor> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.ContractorId
                });

            builder.Property(x => x.ContractorStream)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.OrganizationId);

            builder
                .HasMany(x => x.Contracts)
                .WithOne(x => x.OrganizationContractor)
                .HasForeignKey(x => new
                {
                    x.ContractorOrganizationId,
                    x.ContractorId
                });

            builder
                .HasMany(x => x.Stories)
                .WithOne(x => x.OrganizationContractor)
                .HasForeignKey(x => new
                {
                    x.ContractorOrganizationId,
                    x.ContractorId
                });

            builder.HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.Contractor);

            builder
                .HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.Contractor)
                .HasForeignKey<OrganizationContractor>(x => new
                {
                    x.OrganizationId,
                    x.ContractorId
                })
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Position)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.PositionId)
                .IsRequired(false);

            builder.HasOne(x => x.Level)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.LevelId)
                .IsRequired(false);
            
            AddAuditProperties(builder);
        }
        
    }
}