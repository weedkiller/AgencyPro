// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Proposals.Enums;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class FixedPriceProposalMap : EntityMap<FixedPriceProposal>
    {
        public override void ConfigureInternal(EntityTypeBuilder<FixedPriceProposal> builder)
        {
            builder.ToTable("Proposal");
            builder.Property(x => x.CustomerRateBasis);
            builder.Property(x => x.StoryPointBasis);
            builder.Property(x => x.EstimationBasis);
            builder.Property(x => x.ExtraDayBasis);
            builder.Property(x => x.OtherPercentBasis)
                .HasDefaultValue(0)
                .HasColumnType("decimal(3,2)");
            

            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.ProposalId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            var storyPointBasis = $@"[{nameof(FixedPriceProposal.StoryPointBasis)}]";
            var estimationBasis = $@"[{nameof(FixedPriceProposal.EstimationBasis)}]";
            var otherPercentBasis = $@"[{nameof(FixedPriceProposal.OtherPercentBasis)}]";
            var customerRateBasis = $@"[{nameof(FixedPriceProposal.CustomerRateBasis)}]";
            var extraDayBasis = $@"[{nameof(FixedPriceProposal.ExtraDayBasis)}]";

            var storyHourComputation = $@"({storyPointBasis}*{estimationBasis})";
            var totalHoursComputation = $@"({storyHourComputation} * (1 + {otherPercentBasis}))";
            var totalPriceComputation = $@"({totalHoursComputation} * {customerRateBasis})";

            var velocityBasis = $@"[{nameof(FixedPriceProposal.VelocityBasis)}]";
            var maxHourBasis = $@"[{nameof(FixedPriceProposal.WeeklyMaxHourBasis)}]";

            var weeklyCapacityComputation = $@"({maxHourBasis} * {velocityBasis})";
            var dailyCapacityComputation = $@"({weeklyCapacityComputation} / 7)";
            var totalDayComputation = $@"(({totalHoursComputation}/{dailyCapacityComputation})+{extraDayBasis})";

            builder.Property(x => x.TotalDays)
                .HasComputedColumnSql(totalDayComputation);

            builder.Property(x => x.StoryHours)
                .HasComputedColumnSql(storyHourComputation);

            builder.Property(x => x.TotalHours)
                .HasComputedColumnSql(totalHoursComputation);

            builder.Property(x => x.TotalPriceQuoted)
                .HasComputedColumnSql(totalPriceComputation);

            builder.Property(x => x.WeeklyCapacity)
                .HasComputedColumnSql(weeklyCapacityComputation);

            builder.Property(x => x.DailyCapacity)
                .HasComputedColumnSql(dailyCapacityComputation);

            builder.Property(x => x.VelocityBasis)
                .HasDefaultValue(1)
                .HasColumnType("decimal(3,2)");

            builder.Property(x => x.BudgetBasis)
                .HasColumnType("Money")
                .IsRequired(false);
            

            //builder.Property(x => x.AverageCustomerRate)
            //    .HasColumnType("Money");


            builder.Property(u => u.ConcurrencyStamp)
                .IsConcurrencyToken();

            builder.Property(x => x.ProposalType)
                .HasDefaultValue(ProposalType.Fixed);
            
            builder.Property(x => x.WeeklyMaxHourBasis);

            builder
                .HasOne(x => x.Project)
                .WithOne(x => x.Proposal)
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);


        }
    }
}