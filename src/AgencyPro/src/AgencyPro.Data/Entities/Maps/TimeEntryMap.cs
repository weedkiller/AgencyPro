// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class TimeEntryMap : EntityMap<TimeEntry>
    {
        public override void ConfigureInternal(EntityTypeBuilder<TimeEntry> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Story)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.StoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.BillingCategory)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.TimeType);

            builder.HasQueryFilter(z => !z.IsDeleted);

            var totalHoursComputation = @"(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)";
            var totalMinutesComputation = @"DATEDIFF(minute, [StartDate], [EndDate])";

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(x => x.TotalMinutes).HasComputedColumnSql(totalMinutesComputation);
            builder.Property(x => x.TotalHours).HasComputedColumnSql(totalHoursComputation);
            builder
                .HasOne(x => x.Contract)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.ContractId)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.BuyerTimeEntries)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                })
                .IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired();

            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.TimeEntryId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            builder.Property(x => x.InstantAccountManagerStream).HasColumnType("Money");
            builder.Property(x => x.InstantProjectManagerStream).HasColumnType("Money");
            builder.Property(x => x.InstantContractorStream).HasColumnType("Money");
            builder.Property(x => x.InstantMarketerStream).HasColumnType("Money");
            builder.Property(x => x.InstantRecruiterStream).HasColumnType("Money");
            builder.Property(x => x.InstantAgencyStream).HasColumnType("Money");
            builder.Property(x => x.InstantRecruitingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.InstantMarketingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.InstantSystemStream).HasColumnType("Money");

            builder.Property(x => x.TotalRecruitingAgencyStream)
                .HasComputedColumnSql($@"[InstantRecruitingAgencyStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalMarketingAgencyStream)
                .HasComputedColumnSql($@"[InstantMarketingAgencyStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalAccountManagerStream)
                .HasComputedColumnSql($@"[InstantAccountManagerStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalContractorStream)
                .HasComputedColumnSql($@"[InstantContractorStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalRecruiterStream)
                .HasComputedColumnSql($@"[InstantRecruiterStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalMarketerStream)
                .HasComputedColumnSql($@"[InstantMarketerStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalProjectManagerStream)
                .HasComputedColumnSql($@"[InstantProjectManagerStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalAgencyStream)
                .HasComputedColumnSql($@"[InstantAgencyStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalSystemStream)
                .HasComputedColumnSql($@"[InstantSystemStream]*{totalHoursComputation}");
            builder.Property(x => x.TotalCustomerAmount)
                .HasComputedColumnSql($@"([InstantSystemStream]+[InstantAccountManagerStream]+[InstantProjectManagerStream]+[InstantMarketerStream]+[InstantRecruiterStream]+[InstantContractorStream]+[InstantAgencyStream]+[InstantRecruitingAgencyStream]+[InstantMarketingAgencyStream])*{totalHoursComputation}");

            builder.Property(x => x.TotalRecruitingStream)
                .HasComputedColumnSql($@"([InstantRecruitingAgencyStream]*{totalHoursComputation})+([InstantRecruiterStream]*{totalHoursComputation})");
            builder.Property(x => x.TotalMarketingStream)
                .HasComputedColumnSql($@"([InstantMarketingAgencyStream]*{totalHoursComputation})+([InstantMarketerStream]*{totalHoursComputation})");


            builder.HasOne(x => x.InvoiceItem)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.InvoiceItemId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(x => x.Recruiter)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.RecruiterId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationRecruiter)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.RecruitingOrganizationId,
                    x.RecruiterId
                });

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.ContractorId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationContractor)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.ContractorId
                });

            builder.HasOne(x => x.Marketer)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.MarketerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationMarketer)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.MarketingOrganizationId,
                    x.MarketerId
                });

            builder.HasOne(x => x.ProjectManager)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.ProjectManagerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.ProjectManagerId
                });

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => new
                {
                    x.ProviderOrganizationId,
                    x.AccountManagerId
                });

            builder.HasOne(x => x.RecruitingAgencyOwner)
                .WithMany(x => x.RecruitingTimeEntries)
                .HasForeignKey(x => x.RecruitingAgencyOwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.MarketingAgencyOwner)
                .WithMany(x => x.MarketingTimeEntries)
                .HasForeignKey(x => x.MarketingAgencyOwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProviderAgencyOwner)
                .WithMany(x => x.ProviderTimeEntries)
                .HasForeignKey(x => x.ProviderAgencyOwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            AddAuditProperties(builder);

        }
    }
}