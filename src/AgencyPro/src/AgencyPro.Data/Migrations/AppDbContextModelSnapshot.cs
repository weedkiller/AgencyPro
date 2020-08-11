// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgencyPro.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgencyPro.Core.Agreements.Models.MarketingAgreement", b =>
                {
                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<Guid>("MarketingOrganizationId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("InitiatedByProvider");

                    b.Property<decimal>("MarketerBonus")
                        .HasColumnType("Money");

                    b.Property<string>("MarketerInformation");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingBonus")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[MarketerBonus]+[MarketingAgencyBonus]");

                    b.Property<decimal>("MarketingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[MarketingAgencyStream]+[MarketerStream]");

                    b.Property<bool>("RequireUniqueEmail");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("ProviderOrganizationId", "MarketingOrganizationId");

                    b.HasIndex("MarketingOrganizationId");

                    b.ToTable("MarketingAgreement");
                });

            modelBuilder.Entity("AgencyPro.Core.Agreements.Models.RecruitingAgreement", b =>
                {
                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<Guid>("RecruitingOrganizationId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("InitiatedByProvider");

                    b.Property<decimal>("RecruiterBonus")
                        .HasColumnType("Money");

                    b.Property<string>("RecruiterInformation");

                    b.Property<decimal>("RecruiterStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingBonus")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[RecruitingAgencyBonus]+[RecruiterBonus]");

                    b.Property<decimal>("RecruitingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[RecruitingAgencyStream]+[RecruiterStream]");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("ProviderOrganizationId", "RecruitingOrganizationId");

                    b.HasIndex("RecruitingOrganizationId");

                    b.ToTable("RecruitingAgreement");
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.BillingCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnabled");

                    b.Property<bool>("IsPrivate");

                    b.Property<bool>("IsStoryBucket");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OrganizationId");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.ToTable("BillingCategory");
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.CategoryBillingCategory", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("BillingCategoryId");

                    b.HasKey("CategoryId", "BillingCategoryId");

                    b.HasIndex("BillingCategoryId");

                    b.ToTable("CategoryBillingCategory");
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.OrganizationBillingCategory", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("BillingCategoryId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("OrganizationId", "BillingCategoryId");

                    b.HasIndex("BillingCategoryId");

                    b.ToTable("OrganizationBillingCategory");
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.ProjectBillingCategory", b =>
                {
                    b.Property<Guid>("ProjectId");

                    b.Property<int>("BillingCategoryId");

                    b.HasKey("ProjectId", "BillingCategoryId");

                    b.HasIndex("BillingCategoryId");

                    b.ToTable("ProjectBillingCategory");
                });

            modelBuilder.Entity("AgencyPro.Core.BonusIntents.Models.IndividualBonusIntent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Amount");

                    b.Property<int>("BonusType");

                    b.Property<Guid?>("CandidateId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<Guid?>("LeadId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("PersonId");

                    b.Property<string>("TransferId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId")
                        .IsUnique()
                        .HasFilter("[CandidateId] IS NOT NULL");

                    b.HasIndex("LeadId")
                        .IsUnique()
                        .HasFilter("[LeadId] IS NOT NULL");

                    b.HasIndex("PersonId");

                    b.HasIndex("TransferId");

                    b.HasIndex("OrganizationId", "PersonId");

                    b.ToTable("IndividualBonusIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.BonusIntents.Models.OrganizationBonusIntent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Amount");

                    b.Property<int>("BonusType");

                    b.Property<Guid?>("CandidateId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<Guid?>("LeadId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("TransferId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId")
                        .IsUnique()
                        .HasFilter("[CandidateId] IS NOT NULL");

                    b.HasIndex("LeadId")
                        .IsUnique()
                        .HasFilter("[LeadId] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("TransferId");

                    b.ToTable("OrganizationBonusIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("Delinquent");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("BuyerAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.Candidates.Models.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(254);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsContacted");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Iso2")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<Guid?>("ProjectManagerId");

                    b.Property<Guid?>("ProjectManagerOrganizationId");

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<string>("ProvinceState")
                        .IsRequired()
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<decimal>("RecruiterBonus")
                        .HasColumnType("Money");

                    b.Property<Guid>("RecruiterId");

                    b.Property<Guid>("RecruiterOrganizationId");

                    b.Property<decimal>("RecruiterStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<string>("RejectionDescription")
                        .HasMaxLength(1000);

                    b.Property<int>("RejectionReason");

                    b.Property<byte>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ProviderOrganizationId");

                    b.HasIndex("RecruiterId");

                    b.HasIndex("ProjectManagerOrganizationId", "ProjectManagerId");

                    b.HasIndex("RecruiterOrganizationId", "RecruiterId");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("AgencyPro.Core.Cards.Models.AccountCard", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("AccountId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountCard");
                });

            modelBuilder.Entity("AgencyPro.Core.Cards.Models.CustomerCard", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId")
                        .IsRequired();

                    b.Property<Guid?>("CustomerId1");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.ToTable("CustomerCard");
                });

            modelBuilder.Entity("AgencyPro.Core.Cards.Models.StripeCard", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressCity");

                    b.Property<string>("AddressCountry");

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("Brand");

                    b.Property<string>("Country");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CvcCheck");

                    b.Property<string>("DynamicLast4");

                    b.Property<int>("ExpMonth");

                    b.Property<int>("ExpYear");

                    b.Property<string>("Fingerprint");

                    b.Property<string>("Funding");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Last4");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("StripeCard");
                });

            modelBuilder.Entity("AgencyPro.Core.Categories.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountManagerTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Account Manager");

                    b.Property<string>("AccountManagerTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Account Managers");

                    b.Property<string>("ContractorTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Contractor");

                    b.Property<string>("ContractorTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Contractors");

                    b.Property<string>("CustomerTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Customer");

                    b.Property<string>("CustomerTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Customers");

                    b.Property<decimal>("DefaultAccountManagerStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(5m);

                    b.Property<decimal>("DefaultAgencyStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(15m);

                    b.Property<decimal>("DefaultContractorStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(25m);

                    b.Property<decimal>("DefaultMarketerBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(10m);

                    b.Property<decimal>("DefaultMarketerStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(2.5m);

                    b.Property<decimal>("DefaultMarketingAgencyBonus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(10m);

                    b.Property<decimal>("DefaultMarketingAgencyStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(1m);

                    b.Property<decimal>("DefaultProjectManagerStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(7.5m);

                    b.Property<decimal>("DefaultRecruiterStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(2.5m);

                    b.Property<decimal>("DefaultRecruitingAgencyStream")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Money")
                        .HasDefaultValue(2m);

                    b.Property<string>("MarketerTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Marketer");

                    b.Property<string>("MarketerTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Marketers");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ProjectManagerTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Project Manager");

                    b.Property<string>("ProjectManagerTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Project Managers");

                    b.Property<string>("RecruiterTitle")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Recruiter");

                    b.Property<string>("RecruiterTitlePlural")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasDefaultValue("Recruiters");

                    b.Property<bool>("Searchable")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("StoryTitle");

                    b.Property<string>("StoryTitlePlural");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("AgencyPro.Core.Categories.Models.CategorySkill", b =>
                {
                    b.Property<Guid>("SkillId");

                    b.Property<int>("CategoryId");

                    b.HasKey("SkillId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategorySkill");
                });

            modelBuilder.Entity("AgencyPro.Core.Charges.Models.StripeCharge", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("BalanceTransactionId");

                    b.Property<bool?>("Captured");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("DestinationId");

                    b.Property<bool>("Disputed");

                    b.Property<string>("InvoiceId")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("OnBehalfOfId");

                    b.Property<string>("OrderId");

                    b.Property<string>("OutcomeNetworkStatus");

                    b.Property<string>("OutcomeReason");

                    b.Property<string>("OutcomeRiskLevel");

                    b.Property<long>("OutcomeRiskScore");

                    b.Property<string>("OutcomeSellerMessage");

                    b.Property<string>("OutcomeType");

                    b.Property<bool>("Paid");

                    b.Property<string>("PaymentIntentId");

                    b.Property<Guid?>("ProjectId");

                    b.Property<string>("ReceiptEmail");

                    b.Property<string>("ReceiptNumber");

                    b.Property<string>("ReceiptUrl");

                    b.Property<bool>("Refunded");

                    b.Property<string>("StatementDescriptor");

                    b.Property<string>("StatementDescriptorSuffix");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PaymentIntentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("StripeCharge");
                });

            modelBuilder.Entity("AgencyPro.Core.Comments.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountManagerId");

                    b.Property<Guid?>("AccountManagerOrganizationId");

                    b.Property<string>("Body");

                    b.Property<Guid?>("CandidateId");

                    b.Property<Guid?>("ContractId");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<Guid>("CreatedById");

                    b.Property<Guid?>("CustomerId");

                    b.Property<Guid?>("CustomerOrganizationId");

                    b.Property<bool>("Internal");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid?>("LeadId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid?>("ProjectId");

                    b.Property<Guid?>("StoryId");

                    b.Property<DateTimeOffset>("Updated");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("CandidateId");

                    b.HasIndex("ContractId");

                    b.HasIndex("LeadId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StoryId");

                    b.HasIndex("OrganizationId", "CreatedById");

                    b.HasIndex("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("AgencyPro.Core.Common.Models.Language", b =>
                {
                    b.Property<string>("Code")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Code");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Language");
                });

            modelBuilder.Entity("AgencyPro.Core.Common.Models.LanguageCountry", b =>
                {
                    b.Property<string>("LanguageCode")
                        .HasMaxLength(20);

                    b.Property<string>("Iso2")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<bool>("Default");

                    b.HasKey("LanguageCode", "Iso2");

                    b.HasIndex("Iso2");

                    b.ToTable("LanguageCountry");
                });

            modelBuilder.Entity("AgencyPro.Core.Contracts.Models.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset?>("AccountManagerEndDate");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<Guid>("AccountManagerOrganizationId");

                    b.Property<DateTimeOffset?>("AccountManagerPauseDate");

                    b.Property<decimal>("AccountManagerStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset?>("AgencyOwnerEndDate");

                    b.Property<DateTimeOffset?>("AgencyOwnerPauseDate");

                    b.Property<decimal>("AgencyStream")
                        .HasColumnType("Money");

                    b.Property<int>("BuyerNumber");

                    b.Property<Guid>("BuyerOrganizationId");

                    b.Property<DateTimeOffset?>("ContractorEndDate");

                    b.Property<Guid>("ContractorId");

                    b.Property<Guid>("ContractorOrganizationId");

                    b.Property<DateTimeOffset?>("ContractorPauseDate");

                    b.Property<decimal>("ContractorStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTimeOffset?>("CustomerEndDate");

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTimeOffset?>("CustomerPauseDate");

                    b.Property<decimal>("CustomerRate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[ContractorStream]+[RecruiterStream]+[ProjectManagerStream]+[AccountManagerStream]+[MarketerStream]+[AgencyStream]+[MarketingAgencyStream]+[RecruitingAgencyStream]+[SystemStream]");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsEnded")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when (coalesce([AgencyOwnerEndDate],[AccountManagerEndDate],[ContractorEndDate],[CustomerEndDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

                    b.Property<bool>("IsPaused")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when (coalesce([AgencyOwnerPauseDate],[AccountManagerPauseDate],[ContractorPauseDate],[CustomerPauseDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

                    b.Property<Guid>("MarketerId");

                    b.Property<Guid>("MarketerOrganizationId");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<int>("MarketingNumber");

                    b.Property<decimal>("MaxAccountManagerWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([AccountManagerStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxAgencyWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([AgencyStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxContractorWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([ContractorStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxCustomerWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([ContractorStream]+[RecruiterStream]+[ProjectManagerStream]+[AccountManagerStream]+[MarketerStream]+[AgencyStream]+[MarketingAgencyStream]+[RecruitingAgencyStream]+[SystemStream])*[MaxWeeklyHours]");

                    b.Property<decimal>("MaxMarketerWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([MarketerStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxMarketingAgencyWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([MarketingAgencyStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxProjectManagerWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([ProjectManagerStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxRecruiterWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([RecruiterStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxRecruitingAgencyWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([RecruitingAgencyStream]*[MaxWeeklyHours])");

                    b.Property<decimal>("MaxSystemWeekly")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([SystemStream]*[MaxWeeklyHours])");

                    b.Property<int>("MaxWeeklyHours");

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("ProjectManagerId");

                    b.Property<Guid>("ProjectManagerOrganizationId");

                    b.Property<decimal>("ProjectManagerStream")
                        .HasColumnType("Money");

                    b.Property<int>("ProviderNumber");

                    b.Property<Guid>("RecruiterId");

                    b.Property<Guid>("RecruiterOrganizationId");

                    b.Property<decimal>("RecruiterStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<int>("RecruitingNumber");

                    b.Property<byte>("Status");

                    b.Property<decimal>("SystemStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("TotalHoursLogged");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MarketerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("RecruiterId");

                    b.HasIndex("AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("ContractorOrganizationId", "ContractorId");

                    b.HasIndex("ContractorOrganizationId", "ProviderNumber")
                        .IsUnique()
                        .HasName("ContractProviderNumberIndex");

                    b.HasIndex("MarketerOrganizationId", "MarketerId");

                    b.HasIndex("MarketerOrganizationId", "MarketingNumber")
                        .IsUnique()
                        .HasName("ContractMarketingNumberIndex");

                    b.HasIndex("ProjectManagerOrganizationId", "ProjectManagerId");

                    b.HasIndex("RecruiterOrganizationId", "RecruiterId");

                    b.HasIndex("RecruiterOrganizationId", "RecruitingNumber")
                        .IsUnique()
                        .HasName("ContractRecruitingNumberIndex");

                    b.HasIndex("BuyerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", b =>
                {
                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("AccountManagerOrganizationId");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<DateTimeOffset?>("AccountManagerDeactivationDate");

                    b.Property<int>("AccountStatus");

                    b.Property<DateTimeOffset?>("AgencyOwnerDeactivationDate");

                    b.Property<bool>("AutoApproveTimeEntries");

                    b.Property<int>("BuyerNumber");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<DateTimeOffset?>("CustomerDeactivationDate");

                    b.Property<bool>("IsCorporate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when [AccountManagerOrganizationId]=[CustomerOrganizationId] AND [AccountManagerId]=[CustomerId] then cast(1 as bit) else cast(0 as bit) end");

                    b.Property<bool>("IsDeactivated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when (coalesce([AgencyOwnerDeactivationDate],[AccountManagerDeactivationDate],[CustomerDeactivationDate]) is null) then cast(0 as bit) else cast(1 as bit) end");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsInternal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when [AccountManagerOrganizationId]=[CustomerOrganizationId] then cast(1 as bit) else cast(0 as bit) end");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<int>("Number");

                    b.Property<int>("PaymentTermId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<string>("StripeCustomerId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentTermId");

                    b.HasIndex("AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("AccountManagerOrganizationId", "Number")
                        .IsUnique()
                        .HasName("AccountNumberIndex");

                    b.HasIndex("CustomerOrganizationId", "BuyerNumber")
                        .IsUnique()
                        .HasName("BuyerNumberIndex");

                    b.ToTable("CustomerAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.ExceptionLog.ExceptionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("HResult");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(800);

                    b.Property<string>("Method")
                        .HasMaxLength(20);

                    b.Property<string>("RequestUri")
                        .HasMaxLength(200);

                    b.Property<string>("Source")
                        .HasMaxLength(400);

                    b.Property<string>("StackTrace");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExceptionLog");
                });

            modelBuilder.Entity("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", b =>
                {
                    b.Property<string>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken");

                    b.Property<string>("AccountType");

                    b.Property<string>("CardIssuingCapabilityStatus");

                    b.Property<string>("CardPaymentsCapabilityStatus");

                    b.Property<bool>("ChargesEnabled");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("DefaultCurrency");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("MerchantCategoryCode");

                    b.Property<bool>("PayoutsEnabled");

                    b.Property<string>("RefreshToken");

                    b.Property<int>("Status");

                    b.Property<string>("StripePublishableKey");

                    b.Property<string>("SupportEmail");

                    b.Property<string>("SupportPhone");

                    b.Property<string>("TransfersCapabilityStatus");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("AccountId");

                    b.ToTable("FinancialAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.FinancialAccounts.Models.IndividualFinancialAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("FinancialAccountId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("FinancialAccountId")
                        .IsUnique();

                    b.ToTable("IndividualFinancialAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.FinancialAccounts.Models.OrganizationFinancialAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("FinancialAccountId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("FinancialAccountId")
                        .IsUnique();

                    b.ToTable("OrganizationFinancialAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.Geo.Models.Country", b =>
                {
                    b.Property<string>("Iso2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Capital")
                        .HasMaxLength(200);

                    b.Property<string>("Currency")
                        .HasColumnType("char(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Iso3")
                        .IsRequired()
                        .HasColumnType("char(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Latitude")
                        .HasMaxLength(20);

                    b.Property<string>("Longitude")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("NumericCode");

                    b.Property<string>("OfficialName")
                        .HasMaxLength(200);

                    b.Property<string>("PhoneCode")
                        .HasMaxLength(20);

                    b.Property<string>("PostalCodeFormat")
                        .HasMaxLength(200);

                    b.Property<string>("PostalCodeRegex")
                        .HasMaxLength(200);

                    b.HasKey("Iso2");

                    b.HasIndex("Iso2")
                        .IsUnique();

                    b.ToTable("Country");
                });

            modelBuilder.Entity("AgencyPro.Core.Geo.Models.EnabledCountry", b =>
                {
                    b.Property<string>("Iso2")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<DateTimeOffset>("Created");

                    b.Property<bool>("Enabled");

                    b.Property<DateTimeOffset?>("Updated");

                    b.HasKey("Iso2");

                    b.ToTable("EnabledCountry");
                });

            modelBuilder.Entity("AgencyPro.Core.Geo.Models.ProvinceState", b =>
                {
                    b.Property<string>("Iso2")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<string>("Code")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Iso2", "Code");

                    b.ToTable("ProvinceState");
                });

            modelBuilder.Entity("AgencyPro.Core.Invoices.Models.ProjectInvoice", b =>
                {
                    b.Property<string>("InvoiceId");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<Guid>("BuyerOrganizationId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("ProjectManagerId");

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<string>("RefNo");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("InvoiceId");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ProviderOrganizationId", "AccountManagerId");

                    b.HasIndex("ProviderOrganizationId", "ProjectManagerId");

                    b.HasIndex("BuyerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId");

                    b.ToTable("ProjectInvoice");
                });

            modelBuilder.Entity("AgencyPro.Core.Leads.Models.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("AccountManagerId");

                    b.Property<Guid?>("AccountManagerOrganizationId");

                    b.Property<DateTime?>("CallbackDate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<string>("Description");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsContacted");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsInternal")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("case when [MarketerOrganizationId]=[ProviderOrganizationId] then cast(1 as bit) else cast(0 as bit) end");

                    b.Property<string>("Iso2")
                        .HasColumnType("char(2)")
                        .HasMaxLength(2);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("MarketerBonus")
                        .HasColumnType("Money");

                    b.Property<Guid>("MarketerId");

                    b.Property<Guid>("MarketerOrganizationId");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<string>("MeetingNotes")
                        .HasMaxLength(5000);

                    b.Property<string>("OrganizationName")
                        .HasMaxLength(50);

                    b.Property<Guid?>("PersonId");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<string>("ProvinceState")
                        .IsRequired()
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("ReferralCode");

                    b.Property<string>("RejectionReason");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("MarketerId");

                    b.HasIndex("PersonId")
                        .IsUnique()
                        .HasFilter("[PersonId] IS NOT NULL");

                    b.HasIndex("ProviderOrganizationId");

                    b.HasIndex("AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("MarketerOrganizationId", "MarketerId");

                    b.ToTable("Lead");
                });

            modelBuilder.Entity("AgencyPro.Core.Levels.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("PositionId");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("Level");
                });

            modelBuilder.Entity("AgencyPro.Core.Models.AuditLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DataTime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("EntityId")
                        .HasMaxLength(32);

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Event")
                        .HasMaxLength(200);

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AuditLog");
                });

            modelBuilder.Entity("AgencyPro.Core.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created");

                    b.Property<string>("Description")
                        .HasMaxLength(400);

                    b.Property<string>("Meta")
                        .HasMaxLength(200);

                    b.Property<int>("SortOrder");

                    b.Property<bool>("Starred");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset?>("Updated");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("Acknowledged");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Message")
                        .HasMaxLength(200);

                    b.Property<Guid>("OrganizationId");

                    b.Property<bool>("RequiresAcknowledgement");

                    b.Property<byte>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Url");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Notification");

                    b.HasDiscriminator<byte>("Type");
                });

            modelBuilder.Entity("AgencyPro.Core.Orders.Model.ProposalWorkOrder", b =>
                {
                    b.Property<Guid>("WorkOrderId");

                    b.Property<Guid>("ProposalId");

                    b.HasKey("WorkOrderId", "ProposalId");

                    b.HasIndex("ProposalId");

                    b.ToTable("ProposalWorkOrder");
                });

            modelBuilder.Entity("AgencyPro.Core.Orders.Model.WorkOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountManagerId");

                    b.Property<Guid>("AccountManagerOrganizationId");

                    b.Property<int>("BuyerNumber");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ProviderNumber");

                    b.Property<DateTimeOffset?>("ProviderResponseTime");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("AccountManagerOrganizationId", "ProviderNumber")
                        .IsUnique()
                        .HasName("ProviderNumberIndex");

                    b.HasIndex("CustomerOrganizationId", "BuyerNumber")
                        .IsUnique()
                        .HasName("BuyerNumberIndex");

                    b.HasIndex("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                    b.ToTable("WorkOrder");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("PersonId");

                    b.Property<string>("AffiliateCode");

                    b.Property<long>("AgencyFlags");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsOrganizationOwner");

                    b.Property<long>("PersonFlags");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("OrganizationPerson");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.IndividualBuyerAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("BuyerAccountId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("BuyerAccountId")
                        .IsUnique();

                    b.ToTable("IndividualBuyerAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<decimal>("AccountManagerStream")
                        .HasColumnType("Money");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "AccountManagerId");

                    b.HasIndex("AccountManagerId");

                    b.ToTable("OrganizationAccountManager");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationBuyerAccount", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("BuyerAccountId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Created");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.HasIndex("BuyerAccountId")
                        .IsUnique();

                    b.ToTable("OrganizationBuyerAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("ContractorId");

                    b.Property<bool>("AutoApproveTimeEntries");

                    b.Property<string>("Biography");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<decimal>("ContractorStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFeatured");

                    b.Property<int?>("LevelId");

                    b.Property<string>("PortfolioMediaUrl");

                    b.Property<int?>("PositionId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "ContractorId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("LevelId");

                    b.HasIndex("PositionId");

                    b.ToTable("OrganizationContractor");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("OrganizationCustomer");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("MarketerId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSystemDefault");

                    b.Property<decimal>("MarketerBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<string>("ReferralCode");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "MarketerId");

                    b.HasIndex("MarketerId");

                    b.ToTable("OrganizationMarketer");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("ProjectManagerId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("ProjectManagerStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "ProjectManagerId");

                    b.HasIndex("ProjectManagerId");

                    b.ToTable("OrganizationProjectManager");
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("RecruiterId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSystemDefault");

                    b.Property<decimal>("RecruiterBonus");

                    b.Property<decimal>("RecruiterStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("OrganizationId", "RecruiterId");

                    b.HasIndex("RecruiterId");

                    b.ToTable("OrganizationRecruiter");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.MarketingOrganizations.Models.MarketingOrganization", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<decimal>("CombinedMarketingBonus")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[MarketerBonus]+[MarketingAgencyBonus]+[ServiceFeePerLead]");

                    b.Property<decimal>("CombinedMarketingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[MarketerStream]+[MarketingAgencyStream]");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("DefaultMarketerId");

                    b.Property<bool>("Discoverable");

                    b.Property<decimal>("MarketerBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("MarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("ServiceFeePerLead");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("Id", "DefaultMarketerId");

                    b.ToTable("MarketingOrganization");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActiveItemColor")
                        .HasMaxLength(50);

                    b.Property<string>("ActiveItemTextColor")
                        .HasMaxLength(50);

                    b.Property<string>("ActivePresenceColor")
                        .HasMaxLength(50);

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AffiliateInformation");

                    b.Property<int>("CategoryId");

                    b.Property<string>("City");

                    b.Property<string>("ColumnBgColor")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("HoverItemColor")
                        .HasMaxLength(50);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2000);

                    b.Property<string>("Iso2");

                    b.Property<string>("MentionBadgeColor")
                        .HasMaxLength(50);

                    b.Property<string>("MenuBgHoverColor")
                        .HasMaxLength(50);

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationType");

                    b.Property<string>("PostalCode");

                    b.Property<string>("PrimaryColor")
                        .HasMaxLength(50);

                    b.Property<string>("ProviderInformation");

                    b.Property<string>("ProvinceState");

                    b.Property<string>("SecondaryColor")
                        .HasMaxLength(50);

                    b.Property<string>("TertiaryColor")
                        .HasMaxLength(50);

                    b.Property<string>("TextColor")
                        .HasMaxLength(50);

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.OrganizationSubscription", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("StripeSubscriptionId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("StripeSubscriptionId")
                        .IsUnique();

                    b.ToTable("OrganizationSubscription");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.PremiumOrganization", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.ToTable("PremiumOrganization");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.StripeSubscription", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CancelAtPeriodEnd");

                    b.Property<DateTime?>("CanceledAt");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTime?>("CurrentPeriodEnd");

                    b.Property<DateTime?>("CurrentPeriodStart");

                    b.Property<string>("CustomerId");

                    b.Property<DateTime?>("EndedAt");

                    b.Property<DateTime?>("StartDate");

                    b.Property<DateTime?>("TrialEnd");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("StripeSubscription");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.StripeSubscriptionItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PlanId");

                    b.Property<long>("Quantity");

                    b.Property<string>("SubscriptionId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("StripeSubscriptionItem");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("AccountManagerInformation");

                    b.Property<decimal>("AccountManagerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("AgencyStream")
                        .HasColumnType("Money");

                    b.Property<bool>("AutoApproveTimeEntries");

                    b.Property<string>("ContractorInformation");

                    b.Property<decimal>("ContractorStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("DefaultAccountManagerId");

                    b.Property<Guid>("DefaultContractorId");

                    b.Property<Guid>("DefaultProjectManagerId");

                    b.Property<bool>("Discoverable");

                    b.Property<int>("EstimationBasis");

                    b.Property<int>("FutureDaysAllowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<string>("MarketerInformation");

                    b.Property<int>("PreviousDaysAllowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(14);

                    b.Property<string>("ProjectManagerInformation");

                    b.Property<decimal>("ProjectManagerStream")
                        .HasColumnType("Money");

                    b.Property<string>("ProviderInformation");

                    b.Property<string>("RecruiterInformation");

                    b.Property<decimal>("SystemStream")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("Id", "DefaultAccountManagerId");

                    b.HasIndex("Id", "DefaultContractorId");

                    b.HasIndex("Id", "DefaultProjectManagerId");

                    b.ToTable("ProviderOrganization");
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.RecruitingOrganizations.Models.RecruitingOrganization", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<decimal>("CombinedRecruitingBonus")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[RecruiterBonus]+[RecruitingAgencyBonus]+[ServiceFeePerLead]");

                    b.Property<decimal>("CombinedRecruitingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[RecruiterStream]+[RecruitingAgencyStream]");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("DefaultRecruiterId");

                    b.Property<bool>("Discoverable");

                    b.Property<decimal>("RecruiterBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruiterStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyBonus")
                        .HasColumnType("Money");

                    b.Property<decimal>("RecruitingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("ServiceFeePerLead");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("Id", "DefaultRecruiterId");

                    b.ToTable("RecruitingOrganization");
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentIntents.Models.StripePaymentIntent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("Amount");

                    b.Property<decimal?>("AmountCapturable");

                    b.Property<decimal?>("AmountReceived");

                    b.Property<DateTime?>("CancelledAt");

                    b.Property<string>("CaptureMethod");

                    b.Property<string>("ConfirmationMethod");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("InvoiceId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("TransferGroup");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId")
                        .IsUnique()
                        .HasFilter("[InvoiceId] IS NOT NULL");

                    b.ToTable("StripePaymentIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentTerms.Models.CategoryPaymentTerm", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("PaymentTermId");

                    b.HasKey("CategoryId", "PaymentTermId");

                    b.HasIndex("PaymentTermId");

                    b.ToTable("CategoryPaymentTerm");
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentTerms.Models.OrganizationPaymentTerm", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("PaymentTermId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("IsDefault");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("OrganizationId", "PaymentTermId");

                    b.HasIndex("PaymentTermId");

                    b.ToTable("OrganizationPaymentTerm");
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentTerms.Models.PaymentTerm", b =>
                {
                    b.Property<int>("PaymentTermId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("NetValue");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("PaymentTermId");

                    b.ToTable("PaymentTerm");
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.BonusTransfer", b =>
                {
                    b.Property<string>("TransferId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("TransferId");

                    b.ToTable("BonusTransfer");
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.IndividualPayoutIntent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<string>("InvoiceId")
                        .IsRequired();

                    b.Property<string>("InvoiceItemId")
                        .IsRequired();

                    b.Property<string>("InvoiceTransferId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("PersonId");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("InvoiceItemId");

                    b.HasIndex("InvoiceTransferId");

                    b.HasIndex("PersonId");

                    b.HasIndex("OrganizationId", "PersonId");

                    b.ToTable("IndividualPayoutIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.InvoiceTransfer", b =>
                {
                    b.Property<string>("TransferId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("InvoiceId")
                        .IsRequired();

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("TransferId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceTransfer");
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.OrganizationPayoutIntent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("AgencyOwnerId");

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<string>("InvoiceId")
                        .IsRequired();

                    b.Property<string>("InvoiceItemId")
                        .IsRequired();

                    b.Property<string>("InvoiceTransferId");

                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("InvoiceItemId");

                    b.HasIndex("InvoiceTransferId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("OrganizationPayoutIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.People.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("Address2")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasMaxLength(200);

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("DetailsSubmitted");

                    b.Property<string>("DisplayName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<long?>("DobDay");

                    b.Property<long?>("DobMonth");

                    b.Property<long?>("DobYear");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Gender");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Iso2")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(2)")
                        .HasMaxLength(2)
                        .HasDefaultValue("US");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("MaidenName");

                    b.Property<string>("PostalCode");

                    b.Property<string>("ProvinceState")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("ReferralCode");

                    b.Property<int>("Status");

                    b.Property<string>("TaxId");

                    b.Property<bool>("TosAcceptance");

                    b.Property<string>("TosIpAddress");

                    b.Property<DateTime?>("TosShownAndAcceptedDate");

                    b.Property<string>("TosUserAgent");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("Iso2");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("AgencyPro.Core.Plans.Models.StripePlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<string>("Interval");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("ProductId");

                    b.Property<string>("StripeBlob");

                    b.Property<string>("StripeId");

                    b.Property<int>("TrialPeriodDays");

                    b.Property<string>("UniqueId");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("StripePlan");
                });

            modelBuilder.Entity("AgencyPro.Core.Positions.CategoryPosition", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("PositionId");

                    b.HasKey("CategoryId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("CategoryPosition");
                });

            modelBuilder.Entity("AgencyPro.Core.Positions.Models.OrganizationPosition", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<int>("PositionId");

                    b.HasKey("OrganizationId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("OrganizationPosition");
                });

            modelBuilder.Entity("AgencyPro.Core.Positions.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("AgencyPro.Core.Products.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption");

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<bool>("Shippable");

                    b.Property<string>("StatementDescriptor");

                    b.Property<string>("StripeBlob");

                    b.Property<string>("StripeId");

                    b.Property<string>("Type");

                    b.Property<string>("UniqueId");

                    b.Property<string>("UnitLabel");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("AgencyPro.Core.Projects.Models.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Abbreviation");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<Guid>("AccountManagerOrganizationId");

                    b.Property<bool>("AutoApproveTimeEntries");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<Guid>("ProjectManagerId");

                    b.Property<Guid>("ProjectManagerOrganizationId");

                    b.Property<int>("Status");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("AccountManagerOrganizationId", "Abbreviation")
                        .IsUnique()
                        .HasName("ProjectAbbreviationIndex")
                        .HasFilter("[Abbreviation] IS NOT NULL");

                    b.HasIndex("AccountManagerOrganizationId", "AccountManagerId");

                    b.HasIndex("ProjectManagerOrganizationId", "ProjectManagerId");

                    b.HasIndex("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("AgencyPro.Core.Proposals.Models.FixedPriceProposal", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("AgreementText");

                    b.Property<decimal?>("BudgetBasis")
                        .HasColumnType("Money");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<decimal>("CustomerRateBasis");

                    b.Property<decimal>("DailyCapacity")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("(([WeeklyMaxHourBasis] * [VelocityBasis]) / 7)");

                    b.Property<int>("EstimationBasis");

                    b.Property<int>("ExtraDayBasis");

                    b.Property<decimal>("OtherPercentBasis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(3,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("ProposalType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<decimal>("RetainerPercent");

                    b.Property<int>("Status");

                    b.Property<int>("StoryHours")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([StoryPointBasis]*[EstimationBasis])");

                    b.Property<int>("StoryPointBasis");

                    b.Property<decimal>("TotalDays")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("(((([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis]))/(([WeeklyMaxHourBasis] * [VelocityBasis]) / 7))+[ExtraDayBasis])");

                    b.Property<decimal>("TotalHours")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("(([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis]))");

                    b.Property<decimal>("TotalPriceQuoted")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("((([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis])) * [CustomerRateBasis])");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.Property<decimal>("VelocityBasis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(3,2)")
                        .HasDefaultValue(1m);

                    b.Property<decimal>("WeeklyCapacity")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([WeeklyMaxHourBasis] * [VelocityBasis])");

                    b.Property<decimal>("WeeklyMaxHourBasis");

                    b.HasKey("Id");

                    b.ToTable("Proposal");
                });

            modelBuilder.Entity("AgencyPro.Core.Proposals.Models.ProposalAcceptance", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<Guid>("AcceptedBy");

                    b.Property<DateTimeOffset>("AcceptedCompletionDate");

                    b.Property<string>("AgreementText");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<decimal>("CustomerRate")
                        .HasColumnType("Money");

                    b.Property<int>("NetTerms");

                    b.Property<string>("ProposalBlob");

                    b.Property<int>("ProposalType");

                    b.Property<decimal?>("RetainerAmount");

                    b.Property<decimal>("TotalCost");

                    b.Property<decimal>("TotalDays");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<decimal>("Velocity");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerOrganizationId", "CustomerId");

                    b.ToTable("ProposalAcceptance");
                });

            modelBuilder.Entity("AgencyPro.Core.Retainers.Models.ProjectRetainerIntent", b =>
                {
                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("AccountManagerId");

                    b.Property<decimal>("CurrentBalance");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<decimal>("TopOffAmount");

                    b.HasKey("ProjectId");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProviderOrganizationId", "AccountManagerId");

                    b.HasIndex("CustomerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId");

                    b.ToTable("ProjectRetainerIntent");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.AccountManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("AccountManager");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Contractor", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("HoursAvailable")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(40);

                    b.Property<bool>("IsAvailable");

                    b.Property<DateTime?>("LastWorkedUtc");

                    b.Property<Guid>("RecruiterId");

                    b.Property<Guid>("RecruiterOrganizationId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("RecruiterId");

                    b.HasIndex("RecruiterOrganizationId", "RecruiterId");

                    b.ToTable("Contractor");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Customer", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("MarketerId");

                    b.Property<Guid>("MarketerOrganizationId");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("MarketerOrganizationId", "MarketerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Marketer", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.ToTable("Marketer");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.ProjectManager", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.ToTable("ProjectManager");
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Recruiter", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("Id");

                    b.ToTable("Recruiter");
                });

            modelBuilder.Entity("AgencyPro.Core.Skills.Models.ContractorSkill", b =>
                {
                    b.Property<Guid>("SkillId");

                    b.Property<Guid>("ContractorId");

                    b.Property<DateTimeOffset>("Created");

                    b.Property<int>("SelfAssessment");

                    b.Property<DateTimeOffset>("Updated");

                    b.HasKey("SkillId", "ContractorId");

                    b.HasIndex("ContractorId");

                    b.ToTable("ContractorSkill");
                });

            modelBuilder.Entity("AgencyPro.Core.Skills.Models.OrganizationSkill", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("SkillId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("Priority");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("OrganizationId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("OrganizationSkill");
                });

            modelBuilder.Entity("AgencyPro.Core.Skills.Models.Skill", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Name");

                    b.Property<int>("Priority");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("Skill");
                });

            modelBuilder.Entity("AgencyPro.Core.Stories.Models.Story", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset?>("AssignedDateTime");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid?>("ContractorId");

                    b.Property<Guid?>("ContractorOrganizationId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<DateTimeOffset?>("CustomerAcceptanceDate");

                    b.Property<decimal?>("CustomerApprovedHours");

                    b.Property<string>("Description")
                        .HasMaxLength(5000);

                    b.Property<DateTimeOffset?>("DueDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("ProjectId");

                    b.Property<DateTimeOffset?>("ProjectManagerAcceptanceDate");

                    b.Property<int>("Status");

                    b.Property<string>("StoryId");

                    b.Property<int?>("StoryPoints");

                    b.Property<Guid?>("StoryTemplateId");

                    b.Property<string>("Title")
                        .HasMaxLength(500);

                    b.Property<decimal>("TotalHoursLogged");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StoryTemplateId");

                    b.HasIndex("ContractorOrganizationId", "ContractorId");

                    b.ToTable("Story");
                });

            modelBuilder.Entity("AgencyPro.Core.StoryTemplates.Models.StoryTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<decimal>("Hours");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<int?>("StoryPoints");

                    b.Property<string>("Title");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("ProviderOrganizationId");

                    b.ToTable("StoryTemplate");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeApplicationFee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("StripeApplicationFee");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeCheckoutSession", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("StripeCheckoutSession");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AmountDue")
                        .HasColumnType("Money");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("Money");

                    b.Property<decimal>("AmountRemaining")
                        .HasColumnType("Money");

                    b.Property<decimal>("AttemptCount");

                    b.Property<bool>("Attempted");

                    b.Property<bool>("AutomaticCollection");

                    b.Property<string>("BillingReason");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId");

                    b.Property<DateTimeOffset?>("DueDate");

                    b.Property<decimal>("EndingBalance");

                    b.Property<string>("HostedInvoiceUrl");

                    b.Property<string>("InvoicePdf");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Number");

                    b.Property<string>("Status");

                    b.Property<string>("StripePaymentIntentId");

                    b.Property<string>("SubscriptionId");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("Money");

                    b.Property<decimal>("Total")
                        .HasColumnType("Money");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("StripeInvoice");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<Guid?>("ContractId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("InvoiceId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("PeriodEnd");

                    b.Property<DateTime?>("PeriodStart");

                    b.Property<int>("Quantity");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("StripeInvoiceItem");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoiceLine", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Currency");

                    b.Property<string>("Description");

                    b.Property<bool>("Discountable");

                    b.Property<string>("InvoiceId")
                        .IsRequired();

                    b.Property<string>("InvoiceItemId");

                    b.Property<DateTime?>("PeriodEnd");

                    b.Property<DateTime?>("PeriodStart");

                    b.Property<string>("SubscriptionId");

                    b.Property<string>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("InvoiceItemId");

                    b.ToTable("StripeInvoiceLine");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripePayout", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTimeOffset>("ArrivalDate");

                    b.Property<bool>("Automatic");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.ToTable("StripePayout");
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeSource", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("Amount");

                    b.Property<string>("ClientSecret");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("CustomerId");

                    b.Property<string>("Flow");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Status");

                    b.Property<string>("Type");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("StripeSource");
                });

            modelBuilder.Entity("AgencyPro.Core.TimeEntries.Models.TimeEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountManagerId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<Guid>("ContractId");

                    b.Property<Guid>("ContractorId");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("CreatedById");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("CustomerOrganizationId");

                    b.Property<DateTimeOffset>("EndDate");

                    b.Property<decimal>("InstantAccountManagerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantContractorStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantMarketerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantMarketingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantProjectManagerStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantRecruiterStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantRecruitingAgencyStream")
                        .HasColumnType("Money");

                    b.Property<decimal>("InstantSystemStream")
                        .HasColumnType("Money");

                    b.Property<string>("InvoiceItemId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("MarketerId");

                    b.Property<Guid>("MarketingAgencyOwnerId");

                    b.Property<Guid>("MarketingOrganizationId");

                    b.Property<string>("Notes");

                    b.Property<Guid>("ProjectId");

                    b.Property<Guid>("ProjectManagerId");

                    b.Property<Guid>("ProviderAgencyOwnerId");

                    b.Property<Guid>("ProviderOrganizationId");

                    b.Property<Guid>("RecruiterId");

                    b.Property<Guid>("RecruitingAgencyOwnerId");

                    b.Property<Guid>("RecruitingOrganizationId");

                    b.Property<string>("RejectionReason");

                    b.Property<DateTimeOffset>("StartDate");

                    b.Property<int>("Status");

                    b.Property<Guid?>("StoryId");

                    b.Property<int>("TimeType");

                    b.Property<decimal>("TotalAccountManagerStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantAccountManagerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalAgencyStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalContractorStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantContractorStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalCustomerAmount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([InstantSystemStream]+[InstantAccountManagerStream]+[InstantProjectManagerStream]+[InstantMarketerStream]+[InstantRecruiterStream]+[InstantContractorStream]+[InstantAgencyStream]+[InstantRecruitingAgencyStream]+[InstantMarketingAgencyStream])*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalHours")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalMarketerStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantMarketerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalMarketingAgencyStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantMarketingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalMarketingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([InstantMarketingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))+([InstantMarketerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))");

                    b.Property<int>("TotalMinutes")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("DATEDIFF(minute, [StartDate], [EndDate])");

                    b.Property<decimal>("TotalProjectManagerStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantProjectManagerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalRecruiterStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantRecruiterStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalRecruitingAgencyStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantRecruitingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<decimal>("TotalRecruitingStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("([InstantRecruitingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))+([InstantRecruiterStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))");

                    b.Property<decimal>("TotalSystemStream")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasComputedColumnSql("[InstantSystemStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<Guid>("UpdatedById");

                    b.HasKey("Id");

                    b.HasIndex("AccountManagerId");

                    b.HasIndex("ContractId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("InvoiceItemId");

                    b.HasIndex("MarketerId");

                    b.HasIndex("MarketingAgencyOwnerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ProviderAgencyOwnerId");

                    b.HasIndex("RecruiterId");

                    b.HasIndex("RecruitingAgencyOwnerId");

                    b.HasIndex("StoryId");

                    b.HasIndex("TimeType");

                    b.HasIndex("CustomerOrganizationId", "CustomerId");

                    b.HasIndex("MarketingOrganizationId", "MarketerId");

                    b.HasIndex("ProviderOrganizationId", "AccountManagerId");

                    b.HasIndex("ProviderOrganizationId", "ContractorId");

                    b.HasIndex("ProviderOrganizationId", "ProjectManagerId");

                    b.HasIndex("RecruitingOrganizationId", "RecruiterId");

                    b.ToTable("TimeEntry");
                });

            modelBuilder.Entity("AgencyPro.Core.Transactions.Models.StripeBalanceTransaction", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AvailableOn");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<decimal>("Fee");

                    b.Property<decimal>("Gross");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Net");

                    b.Property<string>("PayoutId");

                    b.Property<string>("Status");

                    b.Property<string>("TransactionType");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("PayoutId");

                    b.ToTable("StripeBalanceTransaction");
                });

            modelBuilder.Entity("AgencyPro.Core.Transfers.Models.StripeTransfer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<decimal>("AmountReversed");

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description");

                    b.Property<string>("DestinationId");

                    b.Property<string>("DestinationPaymentId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTimeOffset>("Updated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.ToTable("StripeTransfer");
                });

            modelBuilder.Entity("AgencyPro.Core.UserAccount.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset>("Created")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsAdmin");

                    b.Property<DateTimeOffset?>("LastLogin");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<DateTimeOffset?>("PasswordChanged");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(254);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("SendMail");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity("AgencyPro.Core.UserAccount.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("AgencyPro.Core.Widgets.Models.CategoryWidget", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("WidgetId");

                    b.Property<string>("CategoryConfiguration");

                    b.Property<int>("Priority");

                    b.Property<bool>("Sticky");

                    b.HasKey("CategoryId", "WidgetId");

                    b.HasIndex("WidgetId");

                    b.ToTable("CategoryWidget");
                });

            modelBuilder.Entity("AgencyPro.Core.Widgets.Models.OrganizationPersonWidget", b =>
                {
                    b.Property<Guid>("OrganizationId");

                    b.Property<Guid>("PersonId");

                    b.Property<int>("CategoryId");

                    b.Property<int>("WidgetId");

                    b.HasKey("OrganizationId", "PersonId", "CategoryId", "WidgetId");

                    b.HasIndex("CategoryId", "WidgetId");

                    b.ToTable("OrganizationPersonWidget");
                });

            modelBuilder.Entity("AgencyPro.Core.Widgets.Models.Widget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccessFlag");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayMetadata");

                    b.Property<string>("Name");

                    b.Property<string>("Schema");

                    b.HasKey("Id");

                    b.ToTable("Widget");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<Guid?>("ApplicationUserId");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.CandidateNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("CandidateId");

                    b.HasIndex("CandidateId");

                    b.ToTable("CandidateNotification");

                    b.HasDiscriminator().HasValue((byte)3);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ContractNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("ContractId");

                    b.HasIndex("ContractId");

                    b.ToTable("ContractNotification");

                    b.HasDiscriminator().HasValue((byte)4);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.LeadNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("LeadId");

                    b.HasIndex("LeadId");

                    b.ToTable("LeadNotification");

                    b.HasDiscriminator().HasValue((byte)2);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.PersonNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonNotification");

                    b.HasDiscriminator().HasValue((byte)6);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ProjectNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectNotification");

                    b.HasDiscriminator().HasValue((byte)7);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ProposalNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("ProposalId");

                    b.HasIndex("ProposalId");

                    b.ToTable("ProposalNotification");

                    b.HasDiscriminator().HasValue((byte)8);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.StoryNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("StoryId");

                    b.HasIndex("StoryId");

                    b.ToTable("StoryNotification");

                    b.HasDiscriminator().HasValue((byte)9);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.SystemNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.HasDiscriminator().HasValue((byte)1);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.TimeEntryNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("TimeEntryId");

                    b.HasIndex("TimeEntryId");

                    b.HasDiscriminator().HasValue((byte)10);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.UserNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("PersonId")
                        .HasColumnName("UserNotification_PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("UserNotification");

                    b.HasDiscriminator().HasValue((byte)0);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.WorkOrderNotification", b =>
                {
                    b.HasBaseType("AgencyPro.Core.Notifications.Models.Notification");

                    b.Property<Guid>("WorkOrderId");

                    b.HasIndex("WorkOrderId");

                    b.ToTable("WorkOrderNotification");

                    b.HasDiscriminator().HasValue((byte)11);
                });

            modelBuilder.Entity("AgencyPro.Core.Agreements.Models.MarketingAgreement", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.MarketingOrganizations.Models.MarketingOrganization", "MarketingOrganization")
                        .WithMany("MarketingAgreements")
                        .HasForeignKey("MarketingOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("MarketingAgreements")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Agreements.Models.RecruitingAgreement", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("RecruitingAgreements")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.RecruitingOrganizations.Models.RecruitingOrganization", "RecruitingOrganization")
                        .WithMany("RecruitingAgreements")
                        .HasForeignKey("RecruitingOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.CategoryBillingCategory", b =>
                {
                    b.HasOne("AgencyPro.Core.BillingCategories.Models.BillingCategory", "BillingCategory")
                        .WithMany("CategoryBillingCategories")
                        .HasForeignKey("BillingCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("AvailableBillingCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.OrganizationBillingCategory", b =>
                {
                    b.HasOne("AgencyPro.Core.BillingCategories.Models.BillingCategory", "BillingCategory")
                        .WithMany("OrganizationBillingCategories")
                        .HasForeignKey("BillingCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("BillingCategories")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.BillingCategories.Models.ProjectBillingCategory", b =>
                {
                    b.HasOne("AgencyPro.Core.BillingCategories.Models.BillingCategory", "BillingCategory")
                        .WithMany("ProjectBillingCategories")
                        .HasForeignKey("BillingCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("ProjectBillingCategories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.BonusIntents.Models.IndividualBonusIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Candidates.Models.Candidate", "Candidate")
                        .WithOne("IndividualBonusIntent")
                        .HasForeignKey("AgencyPro.Core.BonusIntents.Models.IndividualBonusIntent", "CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Leads.Models.Lead", "Lead")
                        .WithOne("IndividualBonusIntent")
                        .HasForeignKey("AgencyPro.Core.BonusIntents.Models.IndividualBonusIntent", "LeadId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithMany("BonusIntents")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PayoutIntents.Models.BonusTransfer", "BonusTransfer")
                        .WithMany("IndividualBonusIntents")
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithMany("BonusIntents")
                        .HasForeignKey("OrganizationId", "PersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.BonusIntents.Models.OrganizationBonusIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Candidates.Models.Candidate", "Candidate")
                        .WithOne("OrganizationBonusIntent")
                        .HasForeignKey("AgencyPro.Core.BonusIntents.Models.OrganizationBonusIntent", "CandidateId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Leads.Models.Lead", "Lead")
                        .WithOne("OrganizationBonusIntent")
                        .HasForeignKey("AgencyPro.Core.BonusIntents.Models.OrganizationBonusIntent", "LeadId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("BonusIntents")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PayoutIntents.Models.BonusTransfer", "BonusTransfer")
                        .WithMany("OrganizationBonusIntents")
                        .HasForeignKey("TransferId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Candidates.Models.Candidate", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("Candidates")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("Candidates")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Recruiter", "Recruiter")
                        .WithMany("Candidates")
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "RecruiterOrganization")
                        .WithMany("Candidates")
                        .HasForeignKey("RecruiterOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationProjectManager")
                        .WithMany("Candidates")
                        .HasForeignKey("ProjectManagerOrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "OrganizationRecruiter")
                        .WithMany("Candidates")
                        .HasForeignKey("RecruiterOrganizationId", "RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.Candidates.Models.CandidateStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("CandidateId");

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<byte>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("CandidateId");

                            b1.ToTable("CandidateStatusTransition");

                            b1.HasOne("AgencyPro.Core.Candidates.Models.Candidate", "Candidate")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("CandidateId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.Cards.Models.AccountCard", b =>
                {
                    b.HasOne("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", "FinancialAccount")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Cards.Models.StripeCard", "StripeCard")
                        .WithOne("AccountCard")
                        .HasForeignKey("AgencyPro.Core.Cards.Models.AccountCard", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Cards.Models.CustomerCard", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "Customer")
                        .WithMany("Cards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer")
                        .WithMany("Cards")
                        .HasForeignKey("CustomerId1")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Cards.Models.StripeCard", "StripeCard")
                        .WithOne("CustomerCard")
                        .HasForeignKey("AgencyPro.Core.Cards.Models.CustomerCard", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Categories.Models.CategorySkill", b =>
                {
                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("AvailableSkills")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Skills.Models.Skill", "Skill")
                        .WithMany("SkillCategories")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Charges.Models.StripeCharge", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "Customer")
                        .WithMany("Charges")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", "Destination")
                        .WithMany("DestinationCharges")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("Charges")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PaymentIntents.Models.StripePaymentIntent", "PaymentIntent")
                        .WithMany("Charges")
                        .HasForeignKey("PaymentIntentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Retainers.Models.ProjectRetainerIntent", "RetainerIntent")
                        .WithMany("Credits")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Comments.Models.Comment", b =>
                {
                    b.HasOne("AgencyPro.Core.Candidates.Models.Candidate", "Candidate")
                        .WithMany("Comments")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Contracts.Models.Contract", "Contract")
                        .WithMany("Comments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Leads.Models.Lead", "Lead")
                        .WithMany("Comments")
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Stories.Models.Story", "Story")
                        .WithMany("Comments")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "Creator")
                        .WithMany("Comments")
                        .HasForeignKey("OrganizationId", "CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("Comments")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Common.Models.LanguageCountry", b =>
                {
                    b.HasOne("AgencyPro.Core.Geo.Models.Country", "Country")
                        .WithMany("Languages")
                        .HasForeignKey("Iso2")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Common.Models.Language", "Language")
                        .WithMany("Countries")
                        .HasForeignKey("LanguageCode")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Contracts.Models.Contract", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("Contracts")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "BuyerOrganization")
                        .WithMany("BuyerContracts")
                        .HasForeignKey("BuyerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Contractor", "Contractor")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractorOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Marketer", "Marketer")
                        .WithMany("Contracts")
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.MarketingOrganizations.Models.MarketingOrganization", "MarketerOrganization")
                        .WithMany("MarketerContracts")
                        .HasForeignKey("MarketerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("Contracts")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("Contracts")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Recruiter", "Recruiter")
                        .WithMany("Contracts")
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.RecruitingOrganizations.Models.RecruitingOrganization", "RecruiterOrganization")
                        .WithMany("RecruiterContracts")
                        .HasForeignKey("RecruiterOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("Contracts")
                        .HasForeignKey("AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("Contracts")
                        .HasForeignKey("BuyerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", "OrganizationContractor")
                        .WithMany("Contracts")
                        .HasForeignKey("ContractorOrganizationId", "ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "OrganizationMarketer")
                        .WithMany("Contracts")
                        .HasForeignKey("MarketerOrganizationId", "MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationProjectManager")
                        .WithMany("Contracts")
                        .HasForeignKey("ProjectManagerOrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "OrganizationRecruiter")
                        .WithMany("Contracts")
                        .HasForeignKey("RecruiterOrganizationId", "RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("Contracts")
                        .HasForeignKey("BuyerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.Contracts.Models.ContractStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("ContractId");

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<byte>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("ContractId");

                            b1.ToTable("ContractStatusTransition");

                            b1.HasOne("AgencyPro.Core.Contracts.Models.Contract")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("ContractId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("AccountManagerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "BuyerOrganization")
                        .WithMany("BuyerCustomerAccounts")
                        .HasForeignKey("CustomerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PaymentTerms.Models.PaymentTerm", "PaymentTerm")
                        .WithMany("CustomerAccounts")
                        .HasForeignKey("PaymentTermId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.CustomerAccounts.Models.CustomerAccountStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<Guid>("AccountManagerId");

                            b1.Property<Guid>("AccountManagerOrganizationId");

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<Guid>("CustomerId");

                            b1.Property<Guid>("CustomerOrganizationId");

                            b1.Property<int>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId");

                            b1.ToTable("CustomerAccountStatusTransition");

                            b1.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.ExceptionLog.ExceptionLog", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("ExceptionsRaised")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.FinancialAccounts.Models.IndividualFinancialAccount", b =>
                {
                    b.HasOne("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", "FinancialAccount")
                        .WithOne("IndividualFinancialAccount")
                        .HasForeignKey("AgencyPro.Core.FinancialAccounts.Models.IndividualFinancialAccount", "FinancialAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("IndividualFinancialAccount")
                        .HasForeignKey("AgencyPro.Core.FinancialAccounts.Models.IndividualFinancialAccount", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.FinancialAccounts.Models.OrganizationFinancialAccount", b =>
                {
                    b.HasOne("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", "FinancialAccount")
                        .WithOne("OrganizationFinancialAccount")
                        .HasForeignKey("AgencyPro.Core.FinancialAccounts.Models.OrganizationFinancialAccount", "FinancialAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("OrganizationFinancialAccount")
                        .HasForeignKey("AgencyPro.Core.FinancialAccounts.Models.OrganizationFinancialAccount", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Geo.Models.EnabledCountry", b =>
                {
                    b.HasOne("AgencyPro.Core.Geo.Models.Country", "Country")
                        .WithOne("EnabledCountry")
                        .HasForeignKey("AgencyPro.Core.Geo.Models.EnabledCountry", "Iso2")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Geo.Models.ProvinceState", b =>
                {
                    b.HasOne("AgencyPro.Core.Geo.Models.Country", "Country")
                        .WithMany("ProvinceStates")
                        .HasForeignKey("Iso2")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Invoices.Models.ProjectInvoice", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("Invoices")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "BuyerOrganization")
                        .WithMany("BuyerInvoices")
                        .HasForeignKey("BuyerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithOne("ProjectInvoice")
                        .HasForeignKey("AgencyPro.Core.Invoices.Models.ProjectInvoice", "InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("Invoices")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("Invoices")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "ProviderOrganization")
                        .WithMany("ProviderInvoices")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("Invoices")
                        .HasForeignKey("BuyerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("Invoices")
                        .HasForeignKey("ProviderOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationProjectManager")
                        .WithMany("Invoices")
                        .HasForeignKey("ProviderOrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("Invoices")
                        .HasForeignKey("BuyerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Leads.Models.Lead", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("Leads")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Marketer", "Marketer")
                        .WithMany("Leads")
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "MarketerOrganization")
                        .WithMany("Leads")
                        .HasForeignKey("MarketerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("Lead")
                        .HasForeignKey("AgencyPro.Core.Leads.Models.Lead", "PersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("Leads")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("Leads")
                        .HasForeignKey("AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "OrganizationMarketer")
                        .WithMany("Leads")
                        .HasForeignKey("MarketerOrganizationId", "MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.Leads.Models.LeadStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<Guid>("LeadId");

                            b1.Property<int>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("LeadId");

                            b1.ToTable("LeadStatusTransition");

                            b1.HasOne("AgencyPro.Core.Leads.Models.Lead")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("LeadId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.Levels.Level", b =>
                {
                    b.HasOne("AgencyPro.Core.Positions.Models.Position", "Position")
                        .WithMany("Levels")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Models.AuditLog", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("AuditLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Models.Note", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Orders.Model.ProposalWorkOrder", b =>
                {
                    b.HasOne("AgencyPro.Core.Proposals.Models.FixedPriceProposal", "Proposal")
                        .WithMany("WorkOrders")
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Orders.Model.WorkOrder", "WorkOrder")
                        .WithMany("Proposals")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Orders.Model.WorkOrder", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("WorkOrders")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("WorkOrders")
                        .HasForeignKey("AccountManagerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("WorkOrders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "BuyerOrganization")
                        .WithMany("BuyerWorkOrders")
                        .HasForeignKey("CustomerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("WorkOrders")
                        .HasForeignKey("AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("WorkOrders")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("OrganizationPeople")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithMany("OrganizationPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.IndividualBuyerAccount", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "BuyerAccount")
                        .WithOne("IndividualBuyerAccount")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.IndividualBuyerAccount", "BuyerAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithOne("BuyerAccount")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.IndividualBuyerAccount", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("OrganizationAccountManagers")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("AccountManagers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("AccountManager")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationBuyerAccount", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "BuyerAccount")
                        .WithOne("OrganizationBuyerAccount")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationBuyerAccount", "BuyerAccountId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("OrganizationBuyerAccount")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationBuyerAccount", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Contractor", "Contractor")
                        .WithMany("OrganizationContractors")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Levels.Level", "Level")
                        .WithMany("Contractors")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("Contractors")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Positions.Models.Position", "Position")
                        .WithMany("Contractors")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("Contractor")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", "OrganizationId", "ContractorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("OrganizationCustomers")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("Customers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("Customer")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Marketer", "Marketer")
                        .WithMany("OrganizationMarketers")
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("Marketers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("Marketer")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "OrganizationId", "MarketerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("ProjectManagers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("OrganizationProjectManagers")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("ProjectManager")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("Recruiters")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Recruiter", "Recruiter")
                        .WithMany("OrganizationRecruiters")
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithOne("Recruiter")
                        .HasForeignKey("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "OrganizationId", "RecruiterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.MarketingOrganizations.Models.MarketingOrganization", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("MarketingOrganization")
                        .HasForeignKey("AgencyPro.Core.Organizations.MarketingOrganizations.Models.MarketingOrganization", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "DefaultOrganizationMarketer")
                        .WithMany("OrganizationDefaults")
                        .HasForeignKey("Id", "DefaultMarketerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.Organization", b =>
                {
                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("Organizations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("OwnedAgencies")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.OrganizationSubscription", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("OrganizationSubscription")
                        .HasForeignKey("AgencyPro.Core.Organizations.Models.OrganizationSubscription", "Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.StripeSubscription", "StripeSubscription")
                        .WithOne("OrganizationSubscription")
                        .HasForeignKey("AgencyPro.Core.Organizations.Models.OrganizationSubscription", "StripeSubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.PremiumOrganization", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("PremiumOrganization")
                        .HasForeignKey("AgencyPro.Core.Organizations.Models.PremiumOrganization", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.Models.StripeSubscriptionItem", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.StripeSubscription", "Subscription")
                        .WithMany("Items")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("ProviderOrganization")
                        .HasForeignKey("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "DefaultAccountManager")
                        .WithMany("DefaultOrganizations")
                        .HasForeignKey("Id", "DefaultAccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", "DefaultContractor")
                        .WithMany("DefaultOrganizations")
                        .HasForeignKey("Id", "DefaultContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "DefaultProjectManager")
                        .WithMany("DefaultOrganizations")
                        .HasForeignKey("Id", "DefaultProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Organizations.RecruitingOrganizations.Models.RecruitingOrganization", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithOne("RecruitingOrganization")
                        .HasForeignKey("AgencyPro.Core.Organizations.RecruitingOrganizations.Models.RecruitingOrganization", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "DefaultOrganizationRecruiter")
                        .WithMany("RecruitingOrganizationDefaults")
                        .HasForeignKey("Id", "DefaultRecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentIntents.Models.StripePaymentIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "StripeInvoice")
                        .WithOne("PaymentIntent")
                        .HasForeignKey("AgencyPro.Core.PaymentIntents.Models.StripePaymentIntent", "InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentTerms.Models.CategoryPaymentTerm", b =>
                {
                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("AvailablePaymentTerms")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PaymentTerms.Models.PaymentTerm", "PaymentTerm")
                        .WithMany("CategoryPaymentTerms")
                        .HasForeignKey("PaymentTermId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PaymentTerms.Models.OrganizationPaymentTerm", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("PaymentTerms")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PaymentTerms.Models.PaymentTerm", "PaymentTerm")
                        .WithMany("OrganizationPaymentTerms")
                        .HasForeignKey("PaymentTermId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.BonusTransfer", b =>
                {
                    b.HasOne("AgencyPro.Core.Transfers.Models.StripeTransfer", "Transfer")
                        .WithOne("BonusTransfer")
                        .HasForeignKey("AgencyPro.Core.PayoutIntents.Models.BonusTransfer", "TransferId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.IndividualPayoutIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("IndividualPayoutIntents")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", "InvoiceItem")
                        .WithMany("IndividualPayoutIntents")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PayoutIntents.Models.InvoiceTransfer", "InvoiceTransfer")
                        .WithMany("IndividualPayoutIntents")
                        .HasForeignKey("InvoiceTransferId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("IndividualPayoutIntents")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithMany("PayoutIntents")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithMany("Payouts")
                        .HasForeignKey("OrganizationId", "PersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.InvoiceTransfer", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("InvoiceTransfers")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Transfers.Models.StripeTransfer", "Transfer")
                        .WithOne("InvoiceTransfer")
                        .HasForeignKey("AgencyPro.Core.PayoutIntents.Models.InvoiceTransfer", "TransferId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.PayoutIntents.Models.OrganizationPayoutIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("OrganizationPayoutIntents")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", "InvoiceItem")
                        .WithMany("OrganizationPayoutIntents")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.PayoutIntents.Models.InvoiceTransfer", "InvoiceTransfer")
                        .WithMany("OrganizationPayoutIntents")
                        .HasForeignKey("InvoiceTransferId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("PayoutIntents")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.People.Models.Person", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser", "ApplicationUser")
                        .WithOne("Person")
                        .HasForeignKey("AgencyPro.Core.People.Models.Person", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Geo.Models.Country")
                        .WithMany("UserResidences")
                        .HasForeignKey("Iso2")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Positions.CategoryPosition", b =>
                {
                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("Positions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Positions.Models.Position", "Position")
                        .WithMany("Categories")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Positions.Models.OrganizationPosition", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "Organization")
                        .WithMany("Positions")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Positions.Models.Position", "Position")
                        .WithMany("Organizations")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Projects.Models.Project", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("Projects")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "BuyerOrganization")
                        .WithMany("BuyerProjects")
                        .HasForeignKey("CustomerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "ProviderOrganization")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectManagerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("Projects")
                        .HasForeignKey("AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationProjectManager")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectManagerOrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.Projects.Models.ProjectStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<Guid>("ProjectId");

                            b1.Property<int>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("ProjectId");

                            b1.ToTable("ProjectStatusTransition");

                            b1.HasOne("AgencyPro.Core.Projects.Models.Project")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("ProjectId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.Proposals.Models.FixedPriceProposal", b =>
                {
                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithOne("Proposal")
                        .HasForeignKey("AgencyPro.Core.Proposals.Models.FixedPriceProposal", "Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsMany("AgencyPro.Core.Proposals.Models.ProposalStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<Guid>("ProposalId");

                            b1.Property<int>("Status");

                            b1.HasKey("Id");

                            b1.HasIndex("ProposalId");

                            b1.ToTable("ProposalStatusTransition");

                            b1.HasOne("AgencyPro.Core.Proposals.Models.FixedPriceProposal")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("ProposalId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.Proposals.Models.ProposalAcceptance", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("ProposalsAccepted")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Proposals.Models.FixedPriceProposal", "Proposal")
                        .WithOne("ProposalAcceptance")
                        .HasForeignKey("AgencyPro.Core.Proposals.Models.ProposalAcceptance", "Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("ProposalsAccepted")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Retainers.Models.ProjectRetainerIntent", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("RetainerIntents")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("RetainerIntents")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "CustomerOrganization")
                        .WithMany("BuyerRetainerIntents")
                        .HasForeignKey("CustomerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithOne("ProjectRetainerIntent")
                        .HasForeignKey("AgencyPro.Core.Retainers.Models.ProjectRetainerIntent", "ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "ProviderOrganization")
                        .WithMany("ProviderRetainerIntents")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("RetainerIntents")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("RetainerIntents")
                        .HasForeignKey("ProviderOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.CustomerAccounts.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("RetainerIntents")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.AccountManager", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("AccountManager")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.AccountManager", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Contractor", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("Contractor")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.Contractor", "Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Recruiter", "Recruiter")
                        .WithMany("Contractors")
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "OrganizationRecruiter")
                        .WithMany("Contractors")
                        .HasForeignKey("RecruiterOrganizationId", "RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Customer", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("Customer")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.Customer", "Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "OrganizationMarketer")
                        .WithMany("Customers")
                        .HasForeignKey("MarketerOrganizationId", "MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Marketer", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("Marketer")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.Marketer", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.ProjectManager", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("ProjectManager")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.ProjectManager", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Roles.Models.Recruiter", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithOne("Recruiter")
                        .HasForeignKey("AgencyPro.Core.Roles.Models.Recruiter", "Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Skills.Models.ContractorSkill", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Contractor", "Contractor")
                        .WithMany("ContractorSkills")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Skills.Models.Skill", "Skill")
                        .WithMany("ContractorSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Skills.Models.OrganizationSkill", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.ProviderOrganizations.ProviderOrganization", "Organization")
                        .WithMany("Skills")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Skills.Models.Skill", "Skill")
                        .WithMany("OrganizationSkill")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Stories.Models.Story", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.Contractor", "Contractor")
                        .WithMany("Stories")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("Stories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.StoryTemplates.Models.StoryTemplate", "StoryTemplate")
                        .WithMany("Stories")
                        .HasForeignKey("StoryTemplateId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", "OrganizationContractor")
                        .WithMany("Stories")
                        .HasForeignKey("ContractorOrganizationId", "ContractorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsMany("AgencyPro.Core.Stories.Models.StoryStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<int>("Status");

                            b1.Property<Guid>("StoryId");

                            b1.HasKey("Id");

                            b1.HasIndex("StoryId");

                            b1.ToTable("StoryStatusTransition");

                            b1.HasOne("AgencyPro.Core.Stories.Models.Story")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("StoryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.StoryTemplates.Models.StoryTemplate", b =>
                {
                    b.HasOne("AgencyPro.Core.Organizations.Models.Organization", "ProviderOrganization")
                        .WithMany("StoryTemplates")
                        .HasForeignKey("ProviderOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeCheckoutSession", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "Customer")
                        .WithMany("CheckoutSessions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoice", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "BuyerAccount")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Organizations.Models.StripeSubscription", "SubscriptionInvoice")
                        .WithMany("Invoices")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", b =>
                {
                    b.HasOne("AgencyPro.Core.Contracts.Models.Contract", "Contract")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "Customer")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeInvoiceLine", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoice", "Invoice")
                        .WithMany("Lines")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", "InvoiceItem")
                        .WithMany("InvoiceLines")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AgencyPro.Core.Stripe.Model.StripeSource", b =>
                {
                    b.HasOne("AgencyPro.Core.BuyerAccounts.Models.BuyerAccount", "Customer")
                        .WithMany("PaymentSources")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.TimeEntries.Models.TimeEntry", b =>
                {
                    b.HasOne("AgencyPro.Core.Roles.Models.AccountManager", "AccountManager")
                        .WithMany("TimeEntries")
                        .HasForeignKey("AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Contracts.Models.Contract", "Contract")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Contractor", "Contractor")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "Customer")
                        .WithMany("BuyerTimeEntries")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AgencyPro.Core.Stripe.Model.StripeInvoiceItem", "InvoiceItem")
                        .WithMany("TimeEntries")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgencyPro.Core.Roles.Models.Marketer", "Marketer")
                        .WithMany("TimeEntries")
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "MarketingAgencyOwner")
                        .WithMany("MarketingTimeEntries")
                        .HasForeignKey("MarketingAgencyOwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.ProjectManager", "ProjectManager")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "ProviderAgencyOwner")
                        .WithMany("ProviderTimeEntries")
                        .HasForeignKey("ProviderAgencyOwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Recruiter", "Recruiter")
                        .WithMany("TimeEntries")
                        .HasForeignKey("RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Roles.Models.Customer", "RecruitingAgencyOwner")
                        .WithMany("RecruitingTimeEntries")
                        .HasForeignKey("RecruitingAgencyOwnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Stories.Models.Story", "Story")
                        .WithMany("TimeEntries")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AgencyPro.Core.BillingCategories.Models.BillingCategory", "BillingCategory")
                        .WithMany("TimeEntries")
                        .HasForeignKey("TimeType")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationCustomer", "OrganizationCustomer")
                        .WithMany("TimeEntries")
                        .HasForeignKey("CustomerOrganizationId", "CustomerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationMarketer", "OrganizationMarketer")
                        .WithMany("TimeEntries")
                        .HasForeignKey("MarketingOrganizationId", "MarketerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationAccountManager", "OrganizationAccountManager")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ProviderOrganizationId", "AccountManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationContractor", "OrganizationContractor")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ProviderOrganizationId", "ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationProjectManager", "OrganizationProjectManager")
                        .WithMany("TimeEntries")
                        .HasForeignKey("ProviderOrganizationId", "ProjectManagerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationRoles.Models.OrganizationRecruiter", "OrganizationRecruiter")
                        .WithMany("TimeEntries")
                        .HasForeignKey("RecruitingOrganizationId", "RecruiterId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsMany("AgencyPro.Core.TimeEntries.Models.TimeEntryStatusTransition", "StatusTransitions", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTimeOffset>("Created")
                                .ValueGeneratedOnAdd()
                                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                            b1.Property<int>("Status");

                            b1.Property<Guid>("TimeEntryId");

                            b1.HasKey("Id");

                            b1.HasIndex("TimeEntryId");

                            b1.ToTable("TimeEntryStatusTransition");

                            b1.HasOne("AgencyPro.Core.TimeEntries.Models.TimeEntry")
                                .WithMany("StatusTransitions")
                                .HasForeignKey("TimeEntryId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("AgencyPro.Core.Transactions.Models.StripeBalanceTransaction", b =>
                {
                    b.HasOne("AgencyPro.Core.Stripe.Model.StripePayout", "Payout")
                        .WithMany("BalanceTransactions")
                        .HasForeignKey("PayoutId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Transfers.Models.StripeTransfer", b =>
                {
                    b.HasOne("AgencyPro.Core.FinancialAccounts.Models.FinancialAccount", "DestinationAccount")
                        .WithMany("Transfers")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Widgets.Models.CategoryWidget", b =>
                {
                    b.HasOne("AgencyPro.Core.Categories.Models.Category", "Category")
                        .WithMany("WidgetCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.Widgets.Models.Widget", "Widget")
                        .WithMany("WidgetCategories")
                        .HasForeignKey("WidgetId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Widgets.Models.OrganizationPersonWidget", b =>
                {
                    b.HasOne("AgencyPro.Core.Widgets.Models.CategoryWidget", "CategoryWidget")
                        .WithMany("OrganizationPersonWidgets")
                        .HasForeignKey("CategoryId", "WidgetId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.OrganizationPeople.Models.OrganizationPerson", "OrganizationPerson")
                        .WithMany("OrganizationPersonWidgets")
                        .HasForeignKey("OrganizationId", "PersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany("UserClaims")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany("UserLogins")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany("UserTokens")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.CandidateNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Candidates.Models.Candidate", "Candidate")
                        .WithMany("CandidateNotifications")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ContractNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Contracts.Models.Contract", "Contract")
                        .WithMany("Notifications")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.LeadNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Leads.Models.Lead", "Lead")
                        .WithMany("LeadNotifications")
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.PersonNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.People.Models.Person", "Person")
                        .WithMany("PersonNotifications")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ProjectNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Projects.Models.Project", "Project")
                        .WithMany("Notifications")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.ProposalNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Proposals.Models.FixedPriceProposal", "Proposal")
                        .WithMany("Notifications")
                        .HasForeignKey("ProposalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.StoryNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Stories.Models.Story", "Story")
                        .WithMany("Notifications")
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.TimeEntryNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.TimeEntries.Models.TimeEntry", "TimeEntry")
                        .WithMany()
                        .HasForeignKey("TimeEntryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.UserNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.UserAccount.Models.ApplicationUser", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("AgencyPro.Core.Notifications.Models.WorkOrderNotification", b =>
                {
                    b.HasOne("AgencyPro.Core.Orders.Model.WorkOrder", "WorkOrder")
                        .WithMany("WorkOrderNotifications")
                        .HasForeignKey("WorkOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
