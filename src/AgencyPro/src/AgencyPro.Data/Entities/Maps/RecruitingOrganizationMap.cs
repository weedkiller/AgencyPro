// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class RecruitingOrganizationMap : EntityMap<RecruitingOrganization>
    {
        public override void ConfigureInternal(EntityTypeBuilder<RecruitingOrganization> builder)
        {
            builder
                .HasOne(x => x.Organization)
                .WithOne(x => x.RecruitingOrganization)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RecruiterContracts)
                .WithOne(x => x.RecruiterOrganization)
                .HasForeignKey(x => x.RecruiterOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.RecruiterStream).HasColumnType("Money");
           

            builder.Property(x => x.RecruitingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.RecruiterBonus).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyBonus).HasColumnType("Money");


            builder.HasOne(x => x.DefaultOrganizationRecruiter)
                .WithMany(x => x.RecruitingOrganizationDefaults)
                .HasForeignKey(x => new
                {
                    x.Id,
                    DefaultOrganizationRecruiterId = x.DefaultRecruiterId
                }).IsRequired();

            builder.HasMany(x => x.RecruitingAgreements)
                .WithOne(x => x.RecruitingOrganization)
                .HasForeignKey(x => x.RecruitingOrganizationId);

            builder.Property(x => x.CombinedRecruitingStream).HasComputedColumnSql(
                $@"[{nameof(RecruitingOrganization.RecruiterStream)}]+[{nameof(RecruitingOrganization.RecruitingAgencyStream)}]");

            builder.Property(x => x.CombinedRecruitingBonus).HasComputedColumnSql(
                $@"[{nameof(RecruitingOrganization.RecruiterBonus)}]+[{nameof(RecruitingOrganization.RecruitingAgencyBonus)}]+[{nameof(RecruitingOrganization.ServiceFeePerLead)}]");

            AddAuditProperties(builder);
        }
    }
}