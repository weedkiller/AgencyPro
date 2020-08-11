// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class RecruitingAgreementMap : EntityMap<RecruitingAgreement>
    {
        public override void ConfigureInternal(EntityTypeBuilder<RecruitingAgreement> builder)
        {
            builder.HasKey(x => new
            {
                x.ProviderOrganizationId,
                x.RecruitingOrganizationId
            });

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.RecruitingAgreements)
                .HasForeignKey(x => x.ProviderOrganizationId);

            builder.Property(x => x.RecruiterBonus).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyBonus).HasColumnType("Money");
            builder.Property(x => x.RecruiterStream).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyStream).HasColumnType("Money");


            var recruitingStreamComputation = $@"[{nameof(RecruitingAgreement.RecruitingAgencyStream)}]+[{nameof(RecruitingAgreement.RecruiterStream)}]";
            var recruitingBonusComputation = $@"[{nameof(RecruitingAgreement.RecruitingAgencyBonus)}]+[{nameof(RecruitingAgreement.RecruiterBonus)}]";

            builder.Property(x => x.RecruitingStream).HasComputedColumnSql(recruitingStreamComputation);
            builder.Property(x => x.RecruitingBonus).HasComputedColumnSql(recruitingBonusComputation);

            AddAuditProperties(builder);
        }
    }
}