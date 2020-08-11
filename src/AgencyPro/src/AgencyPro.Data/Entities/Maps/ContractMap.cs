// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ContractMap : EntityMap<Contract>
    {
       
        public override void ConfigureInternal(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(nameof(Contract.ContractorOrganizationId), nameof(Contract.ProviderNumber)).HasName($@"Contract{nameof(Contract.ProviderNumber)}Index").IsUnique();
            builder.HasIndex(nameof(Contract.MarketerOrganizationId), nameof(Contract.MarketingNumber)).HasName($@"Contract{nameof(Contract.MarketingNumber)}Index").IsUnique();
            builder.HasIndex(nameof(Contract.RecruiterOrganizationId), nameof(Contract.RecruitingNumber)).HasName($@"Contract{nameof(Contract.RecruitingNumber)}Index").IsUnique();

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");
            builder.HasQueryFilter(z => !z.IsDeleted);
            builder.Property(x => x.ContractorStream).HasColumnType("Money");
            builder.Property(x => x.ProjectManagerStream).HasColumnType("Money");
            builder.Property(x => x.AccountManagerStream).HasColumnType("Money");
            builder.Property(x => x.RecruiterStream).HasColumnType("Money");
            builder.Property(x => x.MarketerStream).HasColumnType("Money");
            builder.Property(x => x.AgencyStream).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.SystemStream).HasColumnType("Money");

            var customerRateComputation =
                @"[ContractorStream]+[RecruiterStream]+[ProjectManagerStream]+[AccountManagerStream]+[MarketerStream]+[AgencyStream]+[MarketingAgencyStream]+[RecruitingAgencyStream]+[SystemStream]";
            builder.Property(x => x.CustomerRate).HasComputedColumnSql(customerRateComputation);

            builder.Property(x => x.MaxCustomerWeekly).HasComputedColumnSql(
                $@"({customerRateComputation})*[MaxWeeklyHours]");

            builder.Property(x => x.MaxContractorWeekly).HasComputedColumnSql(
                @"([ContractorStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxRecruiterWeekly).HasComputedColumnSql(
                @"([RecruiterStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxMarketerWeekly).HasComputedColumnSql(
                @"([MarketerStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxProjectManagerWeekly).HasComputedColumnSql(
                @"([ProjectManagerStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxAccountManagerWeekly).HasComputedColumnSql(
                @"([AccountManagerStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxAgencyWeekly).HasComputedColumnSql(
                @"([AgencyStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxSystemWeekly).HasComputedColumnSql(
                @"([SystemStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxMarketingAgencyWeekly).HasComputedColumnSql(
                @"([MarketingAgencyStream]*[MaxWeeklyHours])");

            builder.Property(x => x.MaxRecruitingAgencyWeekly).HasComputedColumnSql(
                @"([RecruitingAgencyStream]*[MaxWeeklyHours])");

            builder.Property(x => x.IsPaused).HasComputedColumnSql(
                @"case when (coalesce([AgencyOwnerPauseDate],[AccountManagerPauseDate],[ContractorPauseDate],[CustomerPauseDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

            builder.Property(x => x.IsPaused).HasComputedColumnSql(
                @"case when (coalesce([AgencyOwnerPauseDate],[AccountManagerPauseDate],[ContractorPauseDate],[CustomerPauseDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

            builder.Property(x => x.IsEnded).HasComputedColumnSql(
                @"case when (coalesce([AgencyOwnerEndDate],[AccountManagerEndDate],[ContractorEndDate],[CustomerEndDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.ContractorId);

            builder.HasOne(x => x.Recruiter)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.RecruiterId);

            builder.HasOne(x => x.Marketer)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.MarketerId);

            builder.HasOne(x => x.ProjectManager)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.ProjectManagerId)
                .IsRequired(true);

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired(true);


            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired();

            builder
                .HasOne(x => x.OrganizationContractor)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => new
                {
                    x.ContractorOrganizationId,
                    x.ContractorId
                })
                .IsRequired();

            builder.HasOne(x => x.OrganizationRecruiter)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => new
                {
                    x.RecruiterOrganizationId,
                    x.RecruiterId
                })
                .IsRequired();

            builder.HasMany(x => x.InvoiceItems)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .IsRequired(false);

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => new
                {
                    x.AccountManagerOrganizationId,
                    x.AccountManagerId
                });

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => new
                {
                    x.ProjectManagerOrganizationId,
                    x.ProjectManagerId
                });

            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.ContractId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.BuyerOrganization)
                .WithMany(x => x.BuyerContracts)
                .HasForeignKey(x => x.BuyerOrganizationId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => new
                {
                    x.BuyerOrganizationId,
                    x.CustomerId
                })
                .IsRequired();
            
            AddAuditProperties(builder, true);
        }
        
    }
}