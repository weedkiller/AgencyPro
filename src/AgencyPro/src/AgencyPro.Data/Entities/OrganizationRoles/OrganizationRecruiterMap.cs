// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationRecruiterMap : EntityMap<OrganizationRecruiter>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationRecruiter> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.RecruiterId
                });
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.Property(x => x.RecruiterStream).HasColumnType("Money");
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder
                .HasMany(x => x.Contractors)
                .WithOne(x => x.OrganizationRecruiter)
                .HasForeignKey(x => new
                {
                    x.RecruiterOrganizationId,
                    x.RecruiterId
                })
                .IsRequired();

            builder
                .HasMany(x => x.Contracts)
                .WithOne(x => x.OrganizationRecruiter)
                .HasForeignKey(x => new
                {
                    x.RecruiterOrganizationId,
                    x.RecruiterId
                });

            builder
                .HasMany(x => x.Candidates)
                .WithOne(x => x.OrganizationRecruiter)
                .HasForeignKey(x => new
                {
                    x.RecruiterOrganizationId,
                    x.RecruiterId
                });

            builder
                .HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.Recruiter).HasForeignKey<OrganizationRecruiter>(x => new
                {
                    x.OrganizationId,
                    x.RecruiterId
                })
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);

        }

    }
}