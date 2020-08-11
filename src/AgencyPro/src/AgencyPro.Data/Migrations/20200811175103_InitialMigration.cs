// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgencyPro.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsStoryBucket = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyerAccount",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Balance = table.Column<decimal>(type: "Money", nullable: false),
                    Delinquent = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ContractorTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Contractor"),
                    ContractorTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Contractors"),
                    AccountManagerTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Account Manager"),
                    AccountManagerTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Account Managers"),
                    ProjectManagerTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Project Manager"),
                    ProjectManagerTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Project Managers"),
                    RecruiterTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Recruiter"),
                    MarketerTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Marketer"),
                    StoryTitle = table.Column<string>(nullable: true),
                    StoryTitlePlural = table.Column<string>(nullable: true),
                    RecruiterTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Recruiters"),
                    MarketerTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Marketers"),
                    CustomerTitle = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Customer"),
                    CustomerTitlePlural = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Customers"),
                    Searchable = table.Column<bool>(nullable: false, defaultValue: false),
                    DefaultRecruiterStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 2.5m),
                    DefaultMarketerStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 2.5m),
                    DefaultProjectManagerStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 7.5m),
                    DefaultAccountManagerStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 5m),
                    DefaultContractorStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 25m),
                    DefaultAgencyStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 15m),
                    DefaultMarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 1m),
                    DefaultRecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 2m),
                    DefaultMarketerBonus = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 10m),
                    DefaultMarketingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 10m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Iso3 = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false),
                    NumericCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    OfficialName = table.Column<string>(maxLength: 200, nullable: true),
                    Capital = table.Column<string>(maxLength: 200, nullable: true),
                    Currency = table.Column<string>(type: "char(3)", maxLength: 3, nullable: true),
                    PhoneCode = table.Column<string>(maxLength: 20, nullable: true),
                    Longitude = table.Column<string>(maxLength: 20, nullable: true),
                    Latitude = table.Column<string>(maxLength: 20, nullable: true),
                    PostalCodeFormat = table.Column<string>(maxLength: 200, nullable: true),
                    PostalCodeRegex = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Iso2);
                });

            migrationBuilder.CreateTable(
                name: "FinancialAccount",
                columns: table => new
                {
                    AccountId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    RefreshToken = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    StripePublishableKey = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChargesEnabled = table.Column<bool>(nullable: false),
                    PayoutsEnabled = table.Column<bool>(nullable: false),
                    CardIssuingCapabilityStatus = table.Column<string>(nullable: true),
                    CardPaymentsCapabilityStatus = table.Column<string>(nullable: true),
                    TransfersCapabilityStatus = table.Column<string>(nullable: true),
                    DefaultCurrency = table.Column<string>(nullable: true),
                    MerchantCategoryCode = table.Column<string>(nullable: true),
                    SupportEmail = table.Column<string>(nullable: true),
                    SupportPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialAccount", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                columns: table => new
                {
                    PaymentTermId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    NetValue = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.PaymentTermId);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Shippable = table.Column<bool>(nullable: false),
                    StatementDescriptor = table.Column<string>(nullable: true),
                    UnitLabel = table.Column<string>(nullable: true),
                    StripeId = table.Column<string>(nullable: true),
                    StripeBlob = table.Column<string>(nullable: true),
                    UniqueId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Caption = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Name = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripeApplicationFee",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeApplicationFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripeCard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AddressCity = table.Column<string>(nullable: true),
                    AddressCountry = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CvcCheck = table.Column<string>(nullable: true),
                    DynamicLast4 = table.Column<string>(nullable: true),
                    ExpMonth = table.Column<int>(nullable: false),
                    ExpYear = table.Column<int>(nullable: false),
                    Fingerprint = table.Column<string>(nullable: true),
                    Funding = table.Column<string>(nullable: true),
                    Last4 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripePayout",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Amount = table.Column<decimal>(nullable: false),
                    ArrivalDate = table.Column<DateTimeOffset>(nullable: false),
                    Automatic = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripePayout", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripePlan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Interval = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    StripeId = table.Column<string>(nullable: true),
                    StripeBlob = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    TrialPeriodDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripePlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripeSubscription",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CanceledAt = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndedAt = table.Column<DateTime>(nullable: true),
                    TrialEnd = table.Column<DateTime>(nullable: true),
                    CurrentPeriodEnd = table.Column<DateTime>(nullable: true),
                    CurrentPeriodStart = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    CancelAtPeriodEnd = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeSubscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 254, nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 254, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    LastUpdated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    PasswordChanged = table.Column<DateTimeOffset>(nullable: true),
                    LastLogin = table.Column<DateTimeOffset>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    SendMail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccessFlag = table.Column<long>(nullable: false),
                    Schema = table.Column<string>(nullable: true),
                    DisplayMetadata = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StripeCheckoutSession",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CustomerId = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCheckoutSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeCheckoutSession_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeSource",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ClientSecret = table.Column<string>(nullable: true),
                    Flow = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Amount = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeSource_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBillingCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    BillingCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBillingCategory", x => new { x.CategoryId, x.BillingCategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryBillingCategory_BillingCategory_BillingCategoryId",
                        column: x => x.BillingCategoryId,
                        principalTable: "BillingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryBillingCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EnabledCountry",
                columns: table => new
                {
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnabledCountry", x => x.Iso2);
                    table.ForeignKey(
                        name: "FK_EnabledCountry_Country_Iso2",
                        column: x => x.Iso2,
                        principalTable: "Country",
                        principalColumn: "Iso2",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceState",
                columns: table => new
                {
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceState", x => new { x.Iso2, x.Code });
                    table.ForeignKey(
                        name: "FK_ProvinceState_Country_Iso2",
                        column: x => x.Iso2,
                        principalTable: "Country",
                        principalColumn: "Iso2",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StripeTransfer",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountReversed = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DestinationId = table.Column<string>(nullable: true),
                    DestinationPaymentId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeTransfer_FinancialAccount_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "FinancialAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageCountry",
                columns: table => new
                {
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false),
                    LanguageCode = table.Column<string>(maxLength: 20, nullable: false),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageCountry", x => new { x.LanguageCode, x.Iso2 });
                    table.ForeignKey(
                        name: "FK_LanguageCountry_Country_Iso2",
                        column: x => x.Iso2,
                        principalTable: "Country",
                        principalColumn: "Iso2",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageCountry_Language_LanguageCode",
                        column: x => x.LanguageCode,
                        principalTable: "Language",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPaymentTerm",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    PaymentTermId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPaymentTerm", x => new { x.CategoryId, x.PaymentTermId });
                    table.ForeignKey(
                        name: "FK_CategoryPaymentTerm_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryPaymentTerm_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerm",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPosition",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPosition", x => new { x.CategoryId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_CategoryPosition_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryPosition_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PositionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Level_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategorySkill",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySkill", x => new { x.SkillId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategorySkill_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategorySkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountCard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AccountId = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountCard_FinancialAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "FinancialAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountCard_StripeCard_Id",
                        column: x => x.Id,
                        principalTable: "StripeCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeBalanceTransaction",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    TransactionType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Gross = table.Column<decimal>(nullable: false),
                    Net = table.Column<decimal>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AvailableOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PayoutId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeBalanceTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeBalanceTransaction_StripePayout_PayoutId",
                        column: x => x.PayoutId,
                        principalTable: "StripePayout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeInvoice",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    SubscriptionId = table.Column<string>(nullable: true),
                    AmountPaid = table.Column<decimal>(type: "Money", nullable: false),
                    AmountRemaining = table.Column<decimal>(type: "Money", nullable: false),
                    AmountDue = table.Column<decimal>(type: "Money", nullable: false),
                    AttemptCount = table.Column<decimal>(nullable: false),
                    Attempted = table.Column<bool>(nullable: false),
                    AutomaticCollection = table.Column<bool>(nullable: false),
                    BillingReason = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    EndingBalance = table.Column<decimal>(nullable: false),
                    HostedInvoiceUrl = table.Column<string>(nullable: true),
                    InvoicePdf = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StripePaymentIntentId = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Total = table.Column<decimal>(type: "Money", nullable: false),
                    Subtotal = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeInvoice_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeInvoice_StripeSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "StripeSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeSubscriptionItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    SubscriptionId = table.Column<string>(nullable: false),
                    PlanId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeSubscriptionItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeSubscriptionItem_StripeSubscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "StripeSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityType = table.Column<string>(maxLength: 100, nullable: false),
                    EntityId = table.Column<string>(maxLength: 32, nullable: true),
                    Event = table.Column<string>(maxLength: 200, nullable: true),
                    DataTime = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLog_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HResult = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 800, nullable: false),
                    Source = table.Column<string>(maxLength: 400, nullable: true),
                    RequestUri = table.Column<string>(maxLength: 200, nullable: true),
                    Method = table.Column<string>(maxLength: 20, nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExceptionLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExceptionLog_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: true),
                    Meta = table.Column<string>(maxLength: 200, nullable: true),
                    Starred = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ReferralCode = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    LastName = table.Column<string>(maxLength: 30, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 200, nullable: true),
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true, defaultValue: "US"),
                    ProvinceState = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true, computedColumnSql: "[FirstName] + ' ' + [LastName]"),
                    TosAcceptance = table.Column<bool>(nullable: false),
                    TaxId = table.Column<string>(nullable: true),
                    TosShownAndAcceptedDate = table.Column<DateTime>(nullable: true),
                    TosIpAddress = table.Column<string>(nullable: true),
                    TosUserAgent = table.Column<string>(nullable: true),
                    DetailsSubmitted = table.Column<bool>(nullable: false),
                    DobDay = table.Column<long>(nullable: true),
                    DobMonth = table.Column<long>(nullable: true),
                    DobYear = table.Column<long>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    MaidenName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_UserAccount_Id",
                        column: x => x.Id,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_Country_Iso2",
                        column: x => x.Iso2,
                        principalTable: "Country",
                        principalColumn: "Iso2",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_UserAccount_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserClaim_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_UserAccount_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLogin_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_UserAccount_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserToken_UserAccount_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryWidget",
                columns: table => new
                {
                    WidgetId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Sticky = table.Column<bool>(nullable: false),
                    CategoryConfiguration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryWidget", x => new { x.CategoryId, x.WidgetId });
                    table.ForeignKey(
                        name: "FK_CategoryWidget_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryWidget_Widget_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonusTransfer",
                columns: table => new
                {
                    TransferId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTransfer", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK_BonusTransfer_StripeTransfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "StripeTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTransfer",
                columns: table => new
                {
                    TransferId = table.Column<string>(nullable: false),
                    InvoiceId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTransfer", x => x.TransferId);
                    table.ForeignKey(
                        name: "FK_InvoiceTransfer_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceTransfer_StripeTransfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "StripeTransfer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripePaymentIntent",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Amount = table.Column<decimal>(nullable: true),
                    AmountCapturable = table.Column<decimal>(nullable: true),
                    AmountReceived = table.Column<decimal>(nullable: true),
                    CancelledAt = table.Column<DateTime>(nullable: true),
                    CaptureMethod = table.Column<string>(nullable: true),
                    InvoiceId = table.Column<string>(nullable: true),
                    ConfirmationMethod = table.Column<string>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    TransferGroup = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripePaymentIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripePaymentIntent_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountManager_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualFinancialAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    FinancialAccountId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualFinancialAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualFinancialAccount_FinancialAccount_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualFinancialAccount_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marketer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marketer_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectManager",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectManager_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruiter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recruiter_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ProviderNumber = table.Column<int>(nullable: false),
                    BuyerNumber = table.Column<int>(nullable: false),
                    MarketingNumber = table.Column<int>(nullable: false),
                    RecruitingNumber = table.Column<int>(nullable: false),
                    ContractorId = table.Column<Guid>(nullable: false),
                    ContractorOrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: false),
                    ProjectManagerId = table.Column<Guid>(nullable: false),
                    ProjectManagerOrganizationId = table.Column<Guid>(nullable: false),
                    MarketerId = table.Column<Guid>(nullable: false),
                    MarketerOrganizationId = table.Column<Guid>(nullable: false),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    RecruiterOrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    BuyerOrganizationId = table.Column<Guid>(nullable: false),
                    MaxWeeklyHours = table.Column<int>(nullable: false),
                    ContractorStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    AccountManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    ProjectManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    SystemStream = table.Column<decimal>(type: "Money", nullable: false),
                    AgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    CustomerRate = table.Column<decimal>(nullable: false, computedColumnSql: "[ContractorStream]+[RecruiterStream]+[ProjectManagerStream]+[AccountManagerStream]+[MarketerStream]+[AgencyStream]+[MarketingAgencyStream]+[RecruitingAgencyStream]+[SystemStream]"),
                    MaxCustomerWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([ContractorStream]+[RecruiterStream]+[ProjectManagerStream]+[AccountManagerStream]+[MarketerStream]+[AgencyStream]+[MarketingAgencyStream]+[RecruitingAgencyStream]+[SystemStream])*[MaxWeeklyHours]"),
                    MaxContractorWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([ContractorStream]*[MaxWeeklyHours])"),
                    MaxRecruiterWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([RecruiterStream]*[MaxWeeklyHours])"),
                    MaxMarketerWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([MarketerStream]*[MaxWeeklyHours])"),
                    MaxProjectManagerWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([ProjectManagerStream]*[MaxWeeklyHours])"),
                    MaxAccountManagerWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([AccountManagerStream]*[MaxWeeklyHours])"),
                    MaxAgencyWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([AgencyStream]*[MaxWeeklyHours])"),
                    MaxSystemWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([SystemStream]*[MaxWeeklyHours])"),
                    ProjectId = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MaxRecruitingAgencyWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([RecruitingAgencyStream]*[MaxWeeklyHours])"),
                    MaxMarketingAgencyWeekly = table.Column<decimal>(nullable: false, computedColumnSql: "([MarketingAgencyStream]*[MaxWeeklyHours])"),
                    ContractorPauseDate = table.Column<DateTimeOffset>(nullable: true),
                    CustomerPauseDate = table.Column<DateTimeOffset>(nullable: true),
                    AgencyOwnerPauseDate = table.Column<DateTimeOffset>(nullable: true),
                    AccountManagerPauseDate = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    IsPaused = table.Column<bool>(nullable: false, computedColumnSql: "case when (coalesce([AgencyOwnerPauseDate],[AccountManagerPauseDate],[ContractorPauseDate],[CustomerPauseDate]) is null) then cast(0 as bit) else cast(1 as bit) end"),
                    ContractorEndDate = table.Column<DateTimeOffset>(nullable: true),
                    CustomerEndDate = table.Column<DateTimeOffset>(nullable: true),
                    AgencyOwnerEndDate = table.Column<DateTimeOffset>(nullable: true),
                    AccountManagerEndDate = table.Column<DateTimeOffset>(nullable: true),
                    IsEnded = table.Column<bool>(nullable: false, computedColumnSql: "case when (coalesce([AgencyOwnerEndDate],[AccountManagerEndDate],[ContractorEndDate],[CustomerEndDate]) is null) then cast(0 as bit) else cast(1 as bit) end"),
                    TotalHoursLogged = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Marketer_MarketerId",
                        column: x => x.MarketerId,
                        principalTable: "Marketer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Recruiter_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractId = table.Column<Guid>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractStatusTransition_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StripeInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Amount = table.Column<decimal>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InvoiceId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PeriodStart = table.Column<DateTime>(nullable: true),
                    PeriodEnd = table.Column<DateTime>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeInvoiceItem_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StripeInvoiceItem_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeInvoiceItem_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeInvoiceLine",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    InvoiceId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Discountable = table.Column<bool>(nullable: false),
                    PeriodStart = table.Column<DateTime>(nullable: true),
                    PeriodEnd = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    InvoiceItemId = table.Column<string>(nullable: true),
                    SubscriptionId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeInvoiceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeInvoiceLine_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeInvoiceLine_StripeInvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "StripeInvoiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccount",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BuyerNumber = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    AccountStatus = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    AgencyOwnerDeactivationDate = table.Column<DateTimeOffset>(nullable: true),
                    AccountManagerDeactivationDate = table.Column<DateTimeOffset>(nullable: true),
                    CustomerDeactivationDate = table.Column<DateTimeOffset>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    PaymentTermId = table.Column<int>(nullable: false, defaultValue: 1),
                    IsInternal = table.Column<bool>(nullable: false, computedColumnSql: "case when [AccountManagerOrganizationId]=[CustomerOrganizationId] then cast(1 as bit) else cast(0 as bit) end"),
                    IsCorporate = table.Column<bool>(nullable: false, computedColumnSql: "case when [AccountManagerOrganizationId]=[CustomerOrganizationId] AND [AccountManagerId]=[CustomerId] then cast(1 as bit) else cast(0 as bit) end"),
                    IsDeactivated = table.Column<bool>(nullable: false, computedColumnSql: "case when (coalesce([AgencyOwnerDeactivationDate],[AccountManagerDeactivationDate],[CustomerDeactivationDate]) is null) then cast(0 as bit) else cast(1 as bit) end"),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    StripeCustomerId = table.Column<string>(nullable: true),
                    AutoApproveTimeEntries = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccount", x => new { x.CustomerOrganizationId, x.CustomerId, x.AccountManagerOrganizationId, x.AccountManagerId });
                    table.ForeignKey(
                        name: "FK_CustomerAccount_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAccount_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerm",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccountStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccountStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAccountStatusTransition_CustomerAccount_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManage~",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId, x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    MarketerId = table.Column<Guid>(nullable: false),
                    MarketerOrganizationId = table.Column<Guid>(nullable: false),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    IsInternal = table.Column<bool>(nullable: false, computedColumnSql: "case when [MarketerOrganizationId]=[ProviderOrganizationId] then cast(1 as bit) else cast(0 as bit) end"),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    OrganizationName = table.Column<string>(maxLength: 50, nullable: true),
                    ReferralCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    ProvinceState = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketerBonus = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsContacted = table.Column<bool>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: true),
                    AccountManagerId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true),
                    CallbackDate = table.Column<DateTime>(nullable: true),
                    MeetingNotes = table.Column<string>(maxLength: 5000, nullable: true),
                    RejectionReason = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lead_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_Marketer_MarketerId",
                        column: x => x.MarketerId,
                        principalTable: "Marketer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lead_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeadId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadStatusTransition_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAccountManager",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AccountManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAccountManager", x => new { x.OrganizationId, x.AccountManagerId });
                    table.ForeignKey(
                        name: "FK_OrganizationAccountManager_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    ProjectManagerId = table.Column<Guid>(nullable: false),
                    ProjectManagerOrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    AutoApproveTimeEntries = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_OrganizationAccountManager_AccountManagerOrganizationId_AccountManagerId",
                        columns: x => new { x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_CustomerAccount_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId, x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBillingCategory",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    BillingCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBillingCategory", x => new { x.ProjectId, x.BillingCategoryId });
                    table.ForeignKey(
                        name: "FK_ProjectBillingCategory_BillingCategory_BillingCategoryId",
                        column: x => x.BillingCategoryId,
                        principalTable: "BillingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBillingCategory_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectStatusTransition_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    VelocityBasis = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 1m),
                    WeeklyMaxHourBasis = table.Column<decimal>(nullable: false),
                    AgreementText = table.Column<string>(nullable: true),
                    BudgetBasis = table.Column<decimal>(type: "Money", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    ProposalType = table.Column<int>(nullable: false, defaultValue: 1),
                    WeeklyCapacity = table.Column<decimal>(nullable: false, computedColumnSql: "([WeeklyMaxHourBasis] * [VelocityBasis])"),
                    DailyCapacity = table.Column<decimal>(nullable: false, computedColumnSql: "(([WeeklyMaxHourBasis] * [VelocityBasis]) / 7)"),
                    StoryPointBasis = table.Column<int>(nullable: false),
                    EstimationBasis = table.Column<int>(nullable: false),
                    OtherPercentBasis = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    ExtraDayBasis = table.Column<int>(nullable: false),
                    CustomerRateBasis = table.Column<decimal>(nullable: false),
                    StoryHours = table.Column<int>(nullable: false, computedColumnSql: "([StoryPointBasis]*[EstimationBasis])"),
                    TotalHours = table.Column<decimal>(nullable: false, computedColumnSql: "(([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis]))"),
                    TotalPriceQuoted = table.Column<decimal>(nullable: false, computedColumnSql: "((([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis])) * [CustomerRateBasis])"),
                    TotalDays = table.Column<decimal>(nullable: false, computedColumnSql: "(((([StoryPointBasis]*[EstimationBasis]) * (1 + [OtherPercentBasis]))/(([WeeklyMaxHourBasis] * [VelocityBasis]) / 7))+[ExtraDayBasis])"),
                    RetainerPercent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposal_Project_Id",
                        column: x => x.Id,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProposalId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalStatusTransition_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInvoice",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ProjectId = table.Column<Guid>(nullable: false),
                    RefNo = table.Column<string>(nullable: true),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    BuyerOrganizationId = table.Column<Guid>(nullable: false),
                    ProjectManagerId = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInvoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_OrganizationAccountManager_ProviderOrganizationId_AccountManagerId",
                        columns: x => new { x.ProviderOrganizationId, x.AccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInvoice_CustomerAccount_BuyerOrganizationId_CustomerId_ProviderOrganizationId_AccountManagerId",
                        columns: x => new { x.BuyerOrganizationId, x.CustomerId, x.ProviderOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRetainerIntent",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    TopOffAmount = table.Column<decimal>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRetainerIntent", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectRetainerIntent_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectRetainerIntent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectRetainerIntent_OrganizationAccountManager_ProviderOrganizationId_AccountManagerId",
                        columns: x => new { x.ProviderOrganizationId, x.AccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectRetainerIntent_CustomerAccount_CustomerOrganizationId_CustomerId_ProviderOrganizationId_AccountManagerId",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId, x.ProviderOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StripeCharge",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Disputed = table.Column<bool>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    InvoiceId = table.Column<string>(nullable: false),
                    OrderId = table.Column<string>(nullable: true),
                    ReceiptEmail = table.Column<string>(nullable: true),
                    ReceiptUrl = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DestinationId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BalanceTransactionId = table.Column<string>(nullable: true),
                    Captured = table.Column<bool>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    OnBehalfOfId = table.Column<string>(nullable: true),
                    Refunded = table.Column<bool>(nullable: false),
                    StatementDescriptorSuffix = table.Column<string>(nullable: true),
                    StatementDescriptor = table.Column<string>(nullable: true),
                    PaymentIntentId = table.Column<string>(nullable: true),
                    OutcomeType = table.Column<string>(nullable: true),
                    OutcomeSellerMessage = table.Column<string>(nullable: true),
                    OutcomeRiskScore = table.Column<long>(nullable: false),
                    OutcomeRiskLevel = table.Column<string>(nullable: true),
                    OutcomeReason = table.Column<string>(nullable: true),
                    OutcomeNetworkStatus = table.Column<string>(nullable: true),
                    ReceiptNumber = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StripeCharge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StripeCharge_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeCharge_FinancialAccount_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "FinancialAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeCharge_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeCharge_StripePaymentIntent_PaymentIntentId",
                        column: x => x.PaymentIntentId,
                        principalTable: "StripePaymentIntent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StripeCharge_ProjectRetainerIntent_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectRetainerIntent",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    InvoiceItemId = table.Column<string>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    StoryId = table.Column<Guid>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    TimeType = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    InstantContractorStream = table.Column<decimal>(type: "Money", nullable: false),
                    RejectionReason = table.Column<string>(nullable: true),
                    InstantRecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantMarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantProjectManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantAccountManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantSystemStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantRecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    InstantMarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingOrganizationId = table.Column<Guid>(nullable: false),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    MarketerId = table.Column<Guid>(nullable: false),
                    MarketingOrganizationId = table.Column<Guid>(nullable: false),
                    ProjectManagerId = table.Column<Guid>(nullable: false),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    ContractorId = table.Column<Guid>(nullable: false),
                    ProviderAgencyOwnerId = table.Column<Guid>(nullable: false),
                    MarketingAgencyOwnerId = table.Column<Guid>(nullable: false),
                    RecruitingAgencyOwnerId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    TotalContractorStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantContractorStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalRecruiterStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantRecruiterStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalMarketerStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantMarketerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalProjectManagerStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantProjectManagerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalAccountManagerStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantAccountManagerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalSystemStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantSystemStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalAgencyStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalRecruitingAgencyStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantRecruitingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalRecruitingStream = table.Column<decimal>(nullable: false, computedColumnSql: "([InstantRecruitingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))+([InstantRecruiterStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))"),
                    TotalMarketingStream = table.Column<decimal>(nullable: false, computedColumnSql: "([InstantMarketingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))+([InstantMarketerStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0))"),
                    TotalMarketingAgencyStream = table.Column<decimal>(nullable: false, computedColumnSql: "[InstantMarketingAgencyStream]*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalCustomerAmount = table.Column<decimal>(nullable: false, computedColumnSql: "([InstantSystemStream]+[InstantAccountManagerStream]+[InstantProjectManagerStream]+[InstantMarketerStream]+[InstantRecruiterStream]+[InstantContractorStream]+[InstantAgencyStream]+[InstantRecruitingAgencyStream]+[InstantMarketingAgencyStream])*(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    TotalMinutes = table.Column<int>(nullable: false, computedColumnSql: "DATEDIFF(minute, [StartDate], [EndDate])"),
                    TotalHours = table.Column<decimal>(nullable: false, computedColumnSql: "(DATEDIFF(second, [StartDate], [EndDate]) / 3600.0)"),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeEntry_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_StripeInvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "StripeInvoiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Marketer_MarketerId",
                        column: x => x.MarketerId,
                        principalTable: "Marketer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_Recruiter_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_BillingCategory_TimeType",
                        column: x => x.TimeType,
                        principalTable: "BillingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeEntry_OrganizationAccountManager_ProviderOrganizationId_AccountManagerId",
                        columns: x => new { x.ProviderOrganizationId, x.AccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeEntryStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeEntryId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntryStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeEntryStatusTransition_TimeEntry_TimeEntryId",
                        column: x => x.TimeEntryId,
                        principalTable: "TimeEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AccountManagerId = table.Column<Guid>(nullable: false),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false),
                    BuyerNumber = table.Column<int>(nullable: false),
                    ProviderNumber = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProviderResponseTime = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrder_AccountManager_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalTable: "AccountManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrder_OrganizationAccountManager_AccountManagerOrganizationId_AccountManagerId",
                        columns: x => new { x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrder_CustomerAccount_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId, x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalWorkOrder",
                columns: table => new
                {
                    WorkOrderId = table.Column<Guid>(nullable: false),
                    ProposalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalWorkOrder", x => new { x.WorkOrderId, x.ProposalId });
                    table.ForeignKey(
                        name: "FK_ProposalWorkOrder_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProposalWorkOrder_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationBillingCategory",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    BillingCategoryId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBillingCategory", x => new { x.OrganizationId, x.BillingCategoryId });
                    table.ForeignKey(
                        name: "FK_OrganizationBillingCategory_BillingCategory_BillingCategoryId",
                        column: x => x.BillingCategoryId,
                        principalTable: "BillingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualBonusIntent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    BonusType = table.Column<int>(nullable: false),
                    TransferId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: true),
                    CandidateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualBonusIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualBonusIntent_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualBonusIntent_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualBonusIntent_BonusTransfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "BonusTransfer",
                        principalColumn: "TransferId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationBonusIntent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    BonusType = table.Column<int>(nullable: false),
                    TransferId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: true),
                    CandidateId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBonusIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationBonusIntent_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationBonusIntent_BonusTransfer_TransferId",
                        column: x => x.TransferId,
                        principalTable: "BonusTransfer",
                        principalColumn: "TransferId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCard",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    CustomerId = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCard_BuyerAccount_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCard_StripeCard_Id",
                        column: x => x.Id,
                        principalTable: "StripeCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualBuyerAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    BuyerAccountId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualBuyerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualBuyerAccount_BuyerAccount_BuyerAccountId",
                        column: x => x.BuyerAccountId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationBuyerAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    BuyerAccountId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBuyerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationBuyerAccount_BuyerAccount_BuyerAccountId",
                        column: x => x.BuyerAccountId,
                        principalTable: "BuyerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CandidateStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidateId = table.Column<Guid>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateStatusTransition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    Body = table.Column<string>(nullable: true),
                    Internal = table.Column<bool>(nullable: false),
                    StoryId = table.Column<Guid>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: true),
                    CandidateId = table.Column<Guid>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    AccountManagerId = table.Column<Guid>(nullable: true),
                    AccountManagerOrganizationId = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    CustomerOrganizationId = table.Column<Guid>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_CustomerAccount_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId, x.AccountManagerOrganizationId, x.AccountManagerId },
                        principalTable: "CustomerAccount",
                        principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RequiresAcknowledgement = table.Column<bool>(nullable: false),
                    Acknowledged = table.Column<bool>(nullable: true),
                    CandidateId = table.Column<Guid>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: true),
                    ProposalId = table.Column<Guid>(nullable: true),
                    StoryId = table.Column<Guid>(nullable: true),
                    TimeEntryId = table.Column<Guid>(nullable: true),
                    UserNotification_PersonId = table.Column<Guid>(nullable: true),
                    WorkOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Lead_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Lead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_TimeEntry_TimeEntryId",
                        column: x => x.TimeEntryId,
                        principalTable: "TimeEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notification_UserAccount_UserNotification_PersonId",
                        column: x => x.UserNotification_PersonId,
                        principalTable: "UserAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notification_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 2000, nullable: true),
                    PrimaryColor = table.Column<string>(maxLength: 50, nullable: true),
                    SecondaryColor = table.Column<string>(maxLength: 50, nullable: true),
                    TertiaryColor = table.Column<string>(maxLength: 50, nullable: true),
                    ColumnBgColor = table.Column<string>(maxLength: 50, nullable: true),
                    MenuBgHoverColor = table.Column<string>(maxLength: 50, nullable: true),
                    HoverItemColor = table.Column<string>(maxLength: 50, nullable: true),
                    TextColor = table.Column<string>(maxLength: 50, nullable: true),
                    ActiveItemColor = table.Column<string>(maxLength: 50, nullable: true),
                    ActivePresenceColor = table.Column<string>(maxLength: 50, nullable: true),
                    ActiveItemTextColor = table.Column<string>(maxLength: 50, nullable: true),
                    MentionBadgeColor = table.Column<string>(maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    OrganizationType = table.Column<int>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    AffiliateInformation = table.Column<string>(nullable: true),
                    ProviderInformation = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Iso2 = table.Column<string>(nullable: true),
                    ProvinceState = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationFinancialAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    FinancialAccountId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationFinancialAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationFinancialAccount_FinancialAccount_FinancialAccountId",
                        column: x => x.FinancialAccountId,
                        principalTable: "FinancialAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationFinancialAccount_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPaymentTerm",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PaymentTermId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPaymentTerm", x => new { x.OrganizationId, x.PaymentTermId });
                    table.ForeignKey(
                        name: "FK_OrganizationPaymentTerm_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPaymentTerm_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerm",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPayoutIntent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    AgencyOwnerId = table.Column<Guid>(nullable: false),
                    InvoiceItemId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InvoiceId = table.Column<string>(nullable: false),
                    InvoiceTransferId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPayoutIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationPayoutIntent_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPayoutIntent_StripeInvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "StripeInvoiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPayoutIntent_InvoiceTransfer_InvoiceTransferId",
                        column: x => x.InvoiceTransferId,
                        principalTable: "InvoiceTransfer",
                        principalColumn: "TransferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPayoutIntent_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPerson",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false),
                    PersonFlags = table.Column<long>(nullable: false),
                    AgencyFlags = table.Column<long>(nullable: false),
                    IsOrganizationOwner = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    AffiliateCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPerson", x => new { x.OrganizationId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_OrganizationPerson_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPerson_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPosition",
                columns: table => new
                {
                    PositionId = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPosition", x => new { x.OrganizationId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_OrganizationPosition_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPosition_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSubscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    StripeSubscriptionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationSubscription_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationSubscription_StripeSubscription_StripeSubscriptionId",
                        column: x => x.StripeSubscriptionId,
                        principalTable: "StripeSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PremiumOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PremiumOrganization_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    StoryPoints = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Hours = table.Column<decimal>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryTemplate_Organization_ProviderOrganizationId",
                        column: x => x.ProviderOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IndividualPayoutIntent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    PersonId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<string>(nullable: false),
                    InvoiceItemId = table.Column<string>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InvoiceTransferId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividualPayoutIntent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_StripeInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "StripeInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_StripeInvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "StripeInvoiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_InvoiceTransfer_InvoiceTransferId",
                        column: x => x.InvoiceTransferId,
                        principalTable: "InvoiceTransfer",
                        principalColumn: "TransferId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IndividualPayoutIntent_OrganizationPerson_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationMarketer",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    MarketerId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ReferralCode = table.Column<string>(nullable: true),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    IsSystemDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    MarketerBonus = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationMarketer", x => new { x.OrganizationId, x.MarketerId });
                    table.ForeignKey(
                        name: "FK_OrganizationMarketer_Marketer_MarketerId",
                        column: x => x.MarketerId,
                        principalTable: "Marketer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationMarketer_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationMarketer_OrganizationPerson_OrganizationId_MarketerId",
                        columns: x => new { x.OrganizationId, x.MarketerId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPersonWidget",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    WidgetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPersonWidget", x => new { x.OrganizationId, x.PersonId, x.CategoryId, x.WidgetId });
                    table.ForeignKey(
                        name: "FK_OrganizationPersonWidget_CategoryWidget_CategoryId_WidgetId",
                        columns: x => new { x.CategoryId, x.WidgetId },
                        principalTable: "CategoryWidget",
                        principalColumns: new[] { "CategoryId", "WidgetId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationPersonWidget_OrganizationPerson_OrganizationId_PersonId",
                        columns: x => new { x.OrganizationId, x.PersonId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProjectManager",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ProjectManagerId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ProjectManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProjectManager", x => new { x.OrganizationId, x.ProjectManagerId });
                    table.ForeignKey(
                        name: "FK_OrganizationProjectManager_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationProjectManager_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationProjectManager_OrganizationPerson_OrganizationId_ProjectManagerId",
                        columns: x => new { x.OrganizationId, x.ProjectManagerId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationRecruiter",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    RecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    IsSystemDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    RecruiterBonus = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRecruiter", x => new { x.OrganizationId, x.RecruiterId });
                    table.ForeignKey(
                        name: "FK_OrganizationRecruiter_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationRecruiter_Recruiter_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationRecruiter_OrganizationPerson_OrganizationId_RecruiterId",
                        columns: x => new { x.OrganizationId, x.RecruiterId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    MarketerId = table.Column<Guid>(nullable: false),
                    MarketerOrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_OrganizationMarketer_MarketerOrganizationId_MarketerId",
                        columns: x => new { x.MarketerOrganizationId, x.MarketerId },
                        principalTable: "OrganizationMarketer",
                        principalColumns: new[] { "OrganizationId", "MarketerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketingOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketerBonus = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    Discoverable = table.Column<bool>(nullable: false),
                    ServiceFeePerLead = table.Column<decimal>(nullable: false),
                    DefaultMarketerId = table.Column<Guid>(nullable: false),
                    CombinedMarketingStream = table.Column<decimal>(nullable: false, computedColumnSql: "[MarketerStream]+[MarketingAgencyStream]"),
                    CombinedMarketingBonus = table.Column<decimal>(nullable: false, computedColumnSql: "[MarketerBonus]+[MarketingAgencyBonus]+[ServiceFeePerLead]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingOrganization_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketingOrganization_OrganizationMarketer_Id_DefaultMarketerId",
                        columns: x => new { x.Id, x.DefaultMarketerId },
                        principalTable: "OrganizationMarketer",
                        principalColumns: new[] { "OrganizationId", "MarketerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contractor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    RecruiterOrganizationId = table.Column<Guid>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    HoursAvailable = table.Column<int>(nullable: false, defaultValue: 40),
                    LastWorkedUtc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractor_Person_Id",
                        column: x => x.Id,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractor_Recruiter_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractor_OrganizationRecruiter_RecruiterOrganizationId_RecruiterId",
                        columns: x => new { x.RecruiterOrganizationId, x.RecruiterId },
                        principalTable: "OrganizationRecruiter",
                        principalColumns: new[] { "OrganizationId", "RecruiterId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecruitingOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    RecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruiterBonus = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    Discoverable = table.Column<bool>(nullable: false),
                    ServiceFeePerLead = table.Column<decimal>(nullable: false),
                    DefaultRecruiterId = table.Column<Guid>(nullable: false),
                    CombinedRecruitingStream = table.Column<decimal>(nullable: false, computedColumnSql: "[RecruiterStream]+[RecruitingAgencyStream]"),
                    CombinedRecruitingBonus = table.Column<decimal>(nullable: false, computedColumnSql: "[RecruiterBonus]+[RecruitingAgencyBonus]+[ServiceFeePerLead]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitingOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecruitingOrganization_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecruitingOrganization_OrganizationRecruiter_Id_DefaultRecruiterId",
                        columns: x => new { x.Id, x.DefaultRecruiterId },
                        principalTable: "OrganizationRecruiter",
                        principalColumns: new[] { "OrganizationId", "RecruiterId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationCustomer",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationCustomer", x => new { x.OrganizationId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_OrganizationCustomer_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationCustomer_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationCustomer_OrganizationPerson_OrganizationId_CustomerId",
                        columns: x => new { x.OrganizationId, x.CustomerId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorSkill",
                columns: table => new
                {
                    SkillId = table.Column<Guid>(nullable: false),
                    ContractorId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: false),
                    SelfAssessment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorSkill", x => new { x.SkillId, x.ContractorId });
                    table.ForeignKey(
                        name: "FK_ContractorSkill_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationContractor",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    ContractorId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ContractorStream = table.Column<decimal>(type: "Money", nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    Biography = table.Column<string>(nullable: true),
                    PortfolioMediaUrl = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    LevelId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    AutoApproveTimeEntries = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationContractor", x => new { x.OrganizationId, x.ContractorId });
                    table.ForeignKey(
                        name: "FK_OrganizationContractor_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationContractor_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationContractor_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationContractor_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationContractor_OrganizationPerson_OrganizationId_ContractorId",
                        columns: x => new { x.OrganizationId, x.ContractorId },
                        principalTable: "OrganizationPerson",
                        principalColumns: new[] { "OrganizationId", "PersonId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalAcceptance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AcceptedCompletionDate = table.Column<DateTimeOffset>(nullable: false),
                    TotalCost = table.Column<decimal>(nullable: false),
                    NetTerms = table.Column<int>(nullable: false),
                    RetainerAmount = table.Column<decimal>(nullable: true),
                    ProposalBlob = table.Column<string>(nullable: true),
                    CustomerRate = table.Column<decimal>(type: "Money", nullable: false),
                    AgreementText = table.Column<string>(nullable: true),
                    ProposalType = table.Column<int>(nullable: false),
                    TotalDays = table.Column<decimal>(nullable: false),
                    Velocity = table.Column<decimal>(nullable: false),
                    AcceptedBy = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    CustomerOrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalAcceptance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalAcceptance_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProposalAcceptance_Proposal_Id",
                        column: x => x.Id,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProposalAcceptance_OrganizationCustomer_CustomerOrganizationId_CustomerId",
                        columns: x => new { x.CustomerOrganizationId, x.CustomerId },
                        principalTable: "OrganizationCustomer",
                        principalColumns: new[] { "OrganizationId", "CustomerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    AccountManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    ProjectManagerStream = table.Column<decimal>(type: "Money", nullable: false),
                    AgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    ContractorStream = table.Column<decimal>(type: "Money", nullable: false),
                    ProviderInformation = table.Column<string>(nullable: true),
                    ProjectManagerInformation = table.Column<string>(nullable: true),
                    AccountManagerInformation = table.Column<string>(nullable: true),
                    ContractorInformation = table.Column<string>(nullable: true),
                    RecruiterInformation = table.Column<string>(nullable: true),
                    MarketerInformation = table.Column<string>(nullable: true),
                    Discoverable = table.Column<bool>(nullable: false),
                    EstimationBasis = table.Column<int>(nullable: false),
                    FutureDaysAllowed = table.Column<int>(nullable: false, defaultValue: 0),
                    PreviousDaysAllowed = table.Column<int>(nullable: false, defaultValue: 14),
                    SystemStream = table.Column<decimal>(type: "Money", nullable: false),
                    DefaultContractorId = table.Column<Guid>(nullable: false),
                    DefaultProjectManagerId = table.Column<Guid>(nullable: false),
                    DefaultAccountManagerId = table.Column<Guid>(nullable: false),
                    AutoApproveTimeEntries = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderOrganization_Organization_Id",
                        column: x => x.Id,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProviderOrganization_OrganizationAccountManager_Id_DefaultAccountManagerId",
                        columns: x => new { x.Id, x.DefaultAccountManagerId },
                        principalTable: "OrganizationAccountManager",
                        principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProviderOrganization_OrganizationContractor_Id_DefaultContractorId",
                        columns: x => new { x.Id, x.DefaultContractorId },
                        principalTable: "OrganizationContractor",
                        principalColumns: new[] { "OrganizationId", "ContractorId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProviderOrganization_OrganizationProjectManager_Id_DefaultProjectManagerId",
                        columns: x => new { x.Id, x.DefaultProjectManagerId },
                        principalTable: "OrganizationProjectManager",
                        principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    StoryId = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ContractorId = table.Column<Guid>(nullable: true),
                    ContractorOrganizationId = table.Column<Guid>(nullable: true),
                    StoryPoints = table.Column<int>(nullable: true),
                    CustomerApprovedHours = table.Column<decimal>(nullable: true),
                    DueDate = table.Column<DateTimeOffset>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AssignedDateTime = table.Column<DateTimeOffset>(nullable: true),
                    ProjectManagerAcceptanceDate = table.Column<DateTimeOffset>(nullable: true),
                    CustomerAcceptanceDate = table.Column<DateTimeOffset>(nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    StoryTemplateId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TotalHoursLogged = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Story_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Story_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Story_StoryTemplate_StoryTemplateId",
                        column: x => x.StoryTemplateId,
                        principalTable: "StoryTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Story_OrganizationContractor_ContractorOrganizationId_ContractorId",
                        columns: x => new { x.ContractorOrganizationId, x.ContractorId },
                        principalTable: "OrganizationContractor",
                        principalColumns: new[] { "OrganizationId", "ContractorId" },
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Iso2 = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    ProvinceState = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    RecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruiterBonus = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    IsContacted = table.Column<bool>(nullable: false),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    RecruiterOrganizationId = table.Column<Guid>(nullable: false),
                    RejectionReason = table.Column<int>(nullable: false),
                    RejectionDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    ProjectManagerId = table.Column<Guid>(nullable: true),
                    ProjectManagerOrganizationId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedById = table.Column<Guid>(nullable: false),
                    UpdatedById = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_ProjectManager_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "ProjectManager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_ProviderOrganization_ProviderOrganizationId",
                        column: x => x.ProviderOrganizationId,
                        principalTable: "ProviderOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_Recruiter_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_Organization_RecruiterOrganizationId",
                        column: x => x.RecruiterOrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Candidate_OrganizationProjectManager_ProjectManagerOrganizationId_ProjectManagerId",
                        columns: x => new { x.ProjectManagerOrganizationId, x.ProjectManagerId },
                        principalTable: "OrganizationProjectManager",
                        principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Candidate_OrganizationRecruiter_RecruiterOrganizationId_RecruiterId",
                        columns: x => new { x.RecruiterOrganizationId, x.RecruiterId },
                        principalTable: "OrganizationRecruiter",
                        principalColumns: new[] { "OrganizationId", "RecruiterId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketingAgreement",
                columns: table => new
                {
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    MarketingOrganizationId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    InitiatedByProvider = table.Column<bool>(nullable: false),
                    MarketerBonus = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    MarketingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    MarketerStream = table.Column<decimal>(type: "Money", nullable: false),
                    RequireUniqueEmail = table.Column<bool>(nullable: false),
                    MarketerInformation = table.Column<string>(nullable: true),
                    MarketingStream = table.Column<decimal>(nullable: false, computedColumnSql: "[MarketingAgencyStream]+[MarketerStream]"),
                    MarketingBonus = table.Column<decimal>(nullable: false, computedColumnSql: "[MarketerBonus]+[MarketingAgencyBonus]"),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingAgreement", x => new { x.ProviderOrganizationId, x.MarketingOrganizationId });
                    table.ForeignKey(
                        name: "FK_MarketingAgreement_MarketingOrganization_MarketingOrganizationId",
                        column: x => x.MarketingOrganizationId,
                        principalTable: "MarketingOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MarketingAgreement_ProviderOrganization_ProviderOrganizationId",
                        column: x => x.ProviderOrganizationId,
                        principalTable: "ProviderOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSkill",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    SkillId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSkill", x => new { x.OrganizationId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_OrganizationSkill_ProviderOrganization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "ProviderOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecruitingAgreement",
                columns: table => new
                {
                    ProviderOrganizationId = table.Column<Guid>(nullable: false),
                    RecruitingOrganizationId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    Updated = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()"),
                    InitiatedByProvider = table.Column<bool>(nullable: false),
                    RecruiterStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyBonus = table.Column<decimal>(type: "Money", nullable: false),
                    RecruiterBonus = table.Column<decimal>(type: "Money", nullable: false),
                    RecruitingAgencyStream = table.Column<decimal>(type: "Money", nullable: false),
                    RecruiterInformation = table.Column<string>(nullable: true),
                    RecruitingStream = table.Column<decimal>(nullable: false, computedColumnSql: "[RecruitingAgencyStream]+[RecruiterStream]"),
                    RecruitingBonus = table.Column<decimal>(nullable: false, computedColumnSql: "[RecruitingAgencyBonus]+[RecruiterBonus]"),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitingAgreement", x => new { x.ProviderOrganizationId, x.RecruitingOrganizationId });
                    table.ForeignKey(
                        name: "FK_RecruitingAgreement_ProviderOrganization_ProviderOrganizationId",
                        column: x => x.ProviderOrganizationId,
                        principalTable: "ProviderOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecruitingAgreement_RecruitingOrganization_RecruitingOrganizationId",
                        column: x => x.RecruitingOrganizationId,
                        principalTable: "RecruitingOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoryStatusTransition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StoryId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "SYSDATETIMEOFFSET()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryStatusTransition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryStatusTransition_Story_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Story",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountCard_AccountId",
                table: "AccountCard",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ProjectManagerId",
                table: "Candidate",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ProviderOrganizationId",
                table: "Candidate",
                column: "ProviderOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterId",
                table: "Candidate",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_ProjectManagerOrganizationId_ProjectManagerId",
                table: "Candidate",
                columns: new[] { "ProjectManagerOrganizationId", "ProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_RecruiterOrganizationId_RecruiterId",
                table: "Candidate",
                columns: new[] { "RecruiterOrganizationId", "RecruiterId" });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateStatusTransition_CandidateId",
                table: "CandidateStatusTransition",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryBillingCategory_BillingCategoryId",
                table: "CategoryBillingCategory",
                column: "BillingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPaymentTerm_PaymentTermId",
                table: "CategoryPaymentTerm",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPosition_PositionId",
                table: "CategoryPosition",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySkill_CategoryId",
                table: "CategorySkill",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryWidget_WidgetId",
                table: "CategoryWidget",
                column: "WidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CandidateId",
                table: "Comment",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ContractId",
                table: "Comment",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_LeadId",
                table: "Comment",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProjectId",
                table: "Comment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_StoryId",
                table: "Comment",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OrganizationId_CreatedById",
                table: "Comment",
                columns: new[] { "OrganizationId", "CreatedById" });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "Comment",
                columns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AccountManagerId",
                table: "Contract",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractorId",
                table: "Contract",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerId",
                table: "Contract",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_MarketerId",
                table: "Contract",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectId",
                table: "Contract",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectManagerId",
                table: "Contract",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_RecruiterId",
                table: "Contract",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_AccountManagerOrganizationId_AccountManagerId",
                table: "Contract",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractorOrganizationId_ContractorId",
                table: "Contract",
                columns: new[] { "ContractorOrganizationId", "ContractorId" });

            migrationBuilder.CreateIndex(
                name: "ContractProviderNumberIndex",
                table: "Contract",
                columns: new[] { "ContractorOrganizationId", "ProviderNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_MarketerOrganizationId_MarketerId",
                table: "Contract",
                columns: new[] { "MarketerOrganizationId", "MarketerId" });

            migrationBuilder.CreateIndex(
                name: "ContractMarketingNumberIndex",
                table: "Contract",
                columns: new[] { "MarketerOrganizationId", "MarketingNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProjectManagerOrganizationId_ProjectManagerId",
                table: "Contract",
                columns: new[] { "ProjectManagerOrganizationId", "ProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_RecruiterOrganizationId_RecruiterId",
                table: "Contract",
                columns: new[] { "RecruiterOrganizationId", "RecruiterId" });

            migrationBuilder.CreateIndex(
                name: "ContractRecruitingNumberIndex",
                table: "Contract",
                columns: new[] { "RecruiterOrganizationId", "RecruitingNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_BuyerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "Contract",
                columns: new[] { "BuyerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Contractor_RecruiterId",
                table: "Contractor",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractor_RecruiterOrganizationId_RecruiterId",
                table: "Contractor",
                columns: new[] { "RecruiterOrganizationId", "RecruiterId" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorSkill_ContractorId",
                table: "ContractorSkill",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractStatusTransition_ContractId",
                table: "ContractStatusTransition",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_Iso2",
                table: "Country",
                column: "Iso2",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_MarketerOrganizationId_MarketerId",
                table: "Customer",
                columns: new[] { "MarketerOrganizationId", "MarketerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_AccountManagerId",
                table: "CustomerAccount",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_CustomerId",
                table: "CustomerAccount",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_PaymentTermId",
                table: "CustomerAccount",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_AccountManagerOrganizationId_AccountManagerId",
                table: "CustomerAccount",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "AccountNumberIndex",
                table: "CustomerAccount",
                columns: new[] { "AccountManagerOrganizationId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "BuyerNumberIndex",
                table: "CustomerAccount",
                columns: new[] { "CustomerOrganizationId", "BuyerNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountStatusTransition_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "CustomerAccountStatusTransition",
                columns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCard_CustomerId",
                table: "CustomerCard",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCard_CustomerId1",
                table: "CustomerCard",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionLog_UserId",
                table: "ExceptionLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBonusIntent_CandidateId",
                table: "IndividualBonusIntent",
                column: "CandidateId",
                unique: true,
                filter: "[CandidateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBonusIntent_LeadId",
                table: "IndividualBonusIntent",
                column: "LeadId",
                unique: true,
                filter: "[LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBonusIntent_PersonId",
                table: "IndividualBonusIntent",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBonusIntent_TransferId",
                table: "IndividualBonusIntent",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBonusIntent_OrganizationId_PersonId",
                table: "IndividualBonusIntent",
                columns: new[] { "OrganizationId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_IndividualBuyerAccount_BuyerAccountId",
                table: "IndividualBuyerAccount",
                column: "BuyerAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualFinancialAccount_FinancialAccountId",
                table: "IndividualFinancialAccount",
                column: "FinancialAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPayoutIntent_InvoiceId",
                table: "IndividualPayoutIntent",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPayoutIntent_InvoiceItemId",
                table: "IndividualPayoutIntent",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPayoutIntent_InvoiceTransferId",
                table: "IndividualPayoutIntent",
                column: "InvoiceTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPayoutIntent_PersonId",
                table: "IndividualPayoutIntent",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividualPayoutIntent_OrganizationId_PersonId",
                table: "IndividualPayoutIntent",
                columns: new[] { "OrganizationId", "PersonId" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTransfer_InvoiceId",
                table: "InvoiceTransfer",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Language_Code",
                table: "Language",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageCountry_Iso2",
                table: "LanguageCountry",
                column: "Iso2");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_AccountManagerId",
                table: "Lead",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_MarketerId",
                table: "Lead",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_PersonId",
                table: "Lead",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_ProviderOrganizationId",
                table: "Lead",
                column: "ProviderOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_AccountManagerOrganizationId_AccountManagerId",
                table: "Lead",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lead_MarketerOrganizationId_MarketerId",
                table: "Lead",
                columns: new[] { "MarketerOrganizationId", "MarketerId" });

            migrationBuilder.CreateIndex(
                name: "IX_LeadStatusTransition_LeadId",
                table: "LeadStatusTransition",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Level_PositionId",
                table: "Level",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingAgreement_MarketingOrganizationId",
                table: "MarketingAgreement",
                column: "MarketingOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingOrganization_Id_DefaultMarketerId",
                table: "MarketingOrganization",
                columns: new[] { "Id", "DefaultMarketerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Note_UserId",
                table: "Note",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CandidateId",
                table: "Notification",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ContractId",
                table: "Notification",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_LeadId",
                table: "Notification",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_PersonId",
                table: "Notification",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ProjectId",
                table: "Notification",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ProposalId",
                table: "Notification",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_StoryId",
                table: "Notification",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TimeEntryId",
                table: "Notification",
                column: "TimeEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserNotification_PersonId",
                table: "Notification",
                column: "UserNotification_PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_WorkOrderId",
                table: "Notification",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CategoryId",
                table: "Organization",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_CustomerId",
                table: "Organization",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationAccountManager_AccountManagerId",
                table: "OrganizationAccountManager",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBillingCategory_BillingCategoryId",
                table: "OrganizationBillingCategory",
                column: "BillingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBonusIntent_CandidateId",
                table: "OrganizationBonusIntent",
                column: "CandidateId",
                unique: true,
                filter: "[CandidateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBonusIntent_LeadId",
                table: "OrganizationBonusIntent",
                column: "LeadId",
                unique: true,
                filter: "[LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBonusIntent_OrganizationId",
                table: "OrganizationBonusIntent",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBonusIntent_TransferId",
                table: "OrganizationBonusIntent",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBuyerAccount_BuyerAccountId",
                table: "OrganizationBuyerAccount",
                column: "BuyerAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationContractor_ContractorId",
                table: "OrganizationContractor",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationContractor_LevelId",
                table: "OrganizationContractor",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationContractor_PositionId",
                table: "OrganizationContractor",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationCustomer_CustomerId",
                table: "OrganizationCustomer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationFinancialAccount_FinancialAccountId",
                table: "OrganizationFinancialAccount",
                column: "FinancialAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationMarketer_MarketerId",
                table: "OrganizationMarketer",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPaymentTerm_PaymentTermId",
                table: "OrganizationPaymentTerm",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPayoutIntent_InvoiceId",
                table: "OrganizationPayoutIntent",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPayoutIntent_InvoiceItemId",
                table: "OrganizationPayoutIntent",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPayoutIntent_InvoiceTransferId",
                table: "OrganizationPayoutIntent",
                column: "InvoiceTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPayoutIntent_OrganizationId",
                table: "OrganizationPayoutIntent",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPerson_PersonId",
                table: "OrganizationPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPersonWidget_CategoryId_WidgetId",
                table: "OrganizationPersonWidget",
                columns: new[] { "CategoryId", "WidgetId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationPosition_PositionId",
                table: "OrganizationPosition",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProjectManager_ProjectManagerId",
                table: "OrganizationProjectManager",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationRecruiter_RecruiterId",
                table: "OrganizationRecruiter",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSkill_SkillId",
                table: "OrganizationSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSubscription_StripeSubscriptionId",
                table: "OrganizationSubscription",
                column: "StripeSubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Iso2",
                table: "Person",
                column: "Iso2");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id",
                table: "Product",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Project_AccountManagerId",
                table: "Project",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectManagerId",
                table: "Project",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "ProjectAbbreviationIndex",
                table: "Project",
                columns: new[] { "AccountManagerOrganizationId", "Abbreviation" },
                unique: true,
                filter: "[Abbreviation] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Project_AccountManagerOrganizationId_AccountManagerId",
                table: "Project",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectManagerOrganizationId_ProjectManagerId",
                table: "Project",
                columns: new[] { "ProjectManagerOrganizationId", "ProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "Project",
                columns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBillingCategory_BillingCategoryId",
                table: "ProjectBillingCategory",
                column: "BillingCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_AccountManagerId",
                table: "ProjectInvoice",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_CustomerId",
                table: "ProjectInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_ProjectId",
                table: "ProjectInvoice",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_ProjectManagerId",
                table: "ProjectInvoice",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_ProviderOrganizationId_AccountManagerId",
                table: "ProjectInvoice",
                columns: new[] { "ProviderOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_ProviderOrganizationId_ProjectManagerId",
                table: "ProjectInvoice",
                columns: new[] { "ProviderOrganizationId", "ProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvoice_BuyerOrganizationId_CustomerId_ProviderOrganizationId_AccountManagerId",
                table: "ProjectInvoice",
                columns: new[] { "BuyerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRetainerIntent_AccountManagerId",
                table: "ProjectRetainerIntent",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRetainerIntent_CustomerId",
                table: "ProjectRetainerIntent",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRetainerIntent_ProviderOrganizationId_AccountManagerId",
                table: "ProjectRetainerIntent",
                columns: new[] { "ProviderOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRetainerIntent_CustomerOrganizationId_CustomerId_ProviderOrganizationId_AccountManagerId",
                table: "ProjectRetainerIntent",
                columns: new[] { "CustomerOrganizationId", "CustomerId", "ProviderOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectStatusTransition_ProjectId",
                table: "ProjectStatusTransition",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalAcceptance_CustomerId",
                table: "ProposalAcceptance",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalAcceptance_CustomerOrganizationId_CustomerId",
                table: "ProposalAcceptance",
                columns: new[] { "CustomerOrganizationId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProposalStatusTransition_ProposalId",
                table: "ProposalStatusTransition",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalWorkOrder_ProposalId",
                table: "ProposalWorkOrder",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderOrganization_Id_DefaultAccountManagerId",
                table: "ProviderOrganization",
                columns: new[] { "Id", "DefaultAccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderOrganization_Id_DefaultContractorId",
                table: "ProviderOrganization",
                columns: new[] { "Id", "DefaultContractorId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProviderOrganization_Id_DefaultProjectManagerId",
                table: "ProviderOrganization",
                columns: new[] { "Id", "DefaultProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecruitingAgreement_RecruitingOrganizationId",
                table: "RecruitingAgreement",
                column: "RecruitingOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecruitingOrganization_Id_DefaultRecruiterId",
                table: "RecruitingOrganization",
                columns: new[] { "Id", "DefaultRecruiterId" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_ContractorId",
                table: "Story",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_ProjectId",
                table: "Story",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_StoryTemplateId",
                table: "Story",
                column: "StoryTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_ContractorOrganizationId_ContractorId",
                table: "Story",
                columns: new[] { "ContractorOrganizationId", "ContractorId" });

            migrationBuilder.CreateIndex(
                name: "IX_StoryStatusTransition_StoryId",
                table: "StoryStatusTransition",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryTemplate_ProviderOrganizationId",
                table: "StoryTemplate",
                column: "ProviderOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeBalanceTransaction_PayoutId",
                table: "StripeBalanceTransaction",
                column: "PayoutId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCharge_CustomerId",
                table: "StripeCharge",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCharge_DestinationId",
                table: "StripeCharge",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCharge_InvoiceId",
                table: "StripeCharge",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCharge_PaymentIntentId",
                table: "StripeCharge",
                column: "PaymentIntentId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCharge_ProjectId",
                table: "StripeCharge",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeCheckoutSession_CustomerId",
                table: "StripeCheckoutSession",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoice_CustomerId",
                table: "StripeInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoice_SubscriptionId",
                table: "StripeInvoice",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoiceItem_ContractId",
                table: "StripeInvoiceItem",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoiceItem_CustomerId",
                table: "StripeInvoiceItem",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoiceItem_InvoiceId",
                table: "StripeInvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoiceLine_InvoiceId",
                table: "StripeInvoiceLine",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeInvoiceLine_InvoiceItemId",
                table: "StripeInvoiceLine",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StripePaymentIntent_InvoiceId",
                table: "StripePaymentIntent",
                column: "InvoiceId",
                unique: true,
                filter: "[InvoiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StripePlan_Id",
                table: "StripePlan",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StripeSource_CustomerId",
                table: "StripeSource",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeSubscriptionItem_SubscriptionId",
                table: "StripeSubscriptionItem",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_StripeTransfer_DestinationId",
                table: "StripeTransfer",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_AccountManagerId",
                table: "TimeEntry",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ContractId",
                table: "TimeEntry",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ContractorId",
                table: "TimeEntry",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_CustomerId",
                table: "TimeEntry",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_InvoiceItemId",
                table: "TimeEntry",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_MarketerId",
                table: "TimeEntry",
                column: "MarketerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_MarketingAgencyOwnerId",
                table: "TimeEntry",
                column: "MarketingAgencyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProjectId",
                table: "TimeEntry",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProjectManagerId",
                table: "TimeEntry",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProviderAgencyOwnerId",
                table: "TimeEntry",
                column: "ProviderAgencyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_RecruiterId",
                table: "TimeEntry",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_RecruitingAgencyOwnerId",
                table: "TimeEntry",
                column: "RecruitingAgencyOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_StoryId",
                table: "TimeEntry",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_TimeType",
                table: "TimeEntry",
                column: "TimeType");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_CustomerOrganizationId_CustomerId",
                table: "TimeEntry",
                columns: new[] { "CustomerOrganizationId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_MarketingOrganizationId_MarketerId",
                table: "TimeEntry",
                columns: new[] { "MarketingOrganizationId", "MarketerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProviderOrganizationId_AccountManagerId",
                table: "TimeEntry",
                columns: new[] { "ProviderOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProviderOrganizationId_ContractorId",
                table: "TimeEntry",
                columns: new[] { "ProviderOrganizationId", "ContractorId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProviderOrganizationId_ProjectManagerId",
                table: "TimeEntry",
                columns: new[] { "ProviderOrganizationId", "ProjectManagerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_RecruitingOrganizationId_RecruiterId",
                table: "TimeEntry",
                columns: new[] { "RecruitingOrganizationId", "RecruiterId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntryStatusTransition_TimeEntryId",
                table: "TimeEntryStatusTransition",
                column: "TimeEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_Email",
                table: "UserAccount",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "UserAccount",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "UserAccount",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_PhoneNumber",
                table: "UserAccount",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_UserName",
                table: "UserAccount",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_ApplicationUserId",
                table: "UserClaim",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_ApplicationUserId",
                table: "UserLogin",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_ApplicationUserId",
                table: "UserToken",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_AccountManagerId",
                table: "WorkOrder",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_CustomerId",
                table: "WorkOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_AccountManagerOrganizationId_AccountManagerId",
                table: "WorkOrder",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.CreateIndex(
                name: "ProviderNumberIndex",
                table: "WorkOrder",
                columns: new[] { "AccountManagerOrganizationId", "ProviderNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "BuyerNumberIndex",
                table: "WorkOrder",
                columns: new[] { "CustomerOrganizationId", "BuyerNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrder_CustomerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "WorkOrder",
                columns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_ProviderOrganization_ContractorOrganizationId",
                table: "Contract",
                column: "ContractorOrganizationId",
                principalTable: "ProviderOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Organization_BuyerOrganizationId",
                table: "Contract",
                column: "BuyerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationProjectManager_ProjectManagerOrganizationId_ProjectManagerId",
                table: "Contract",
                columns: new[] { "ProjectManagerOrganizationId", "ProjectManagerId" },
                principalTable: "OrganizationProjectManager",
                principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationRecruiter_RecruiterOrganizationId_RecruiterId",
                table: "Contract",
                columns: new[] { "RecruiterOrganizationId", "RecruiterId" },
                principalTable: "OrganizationRecruiter",
                principalColumns: new[] { "OrganizationId", "RecruiterId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Project_ProjectId",
                table: "Contract",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_CustomerAccount_BuyerOrganizationId_CustomerId_AccountManagerOrganizationId_AccountManagerId",
                table: "Contract",
                columns: new[] { "BuyerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                principalTable: "CustomerAccount",
                principalColumns: new[] { "CustomerOrganizationId", "CustomerId", "AccountManagerOrganizationId", "AccountManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Contractor_ContractorId",
                table: "Contract",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                table: "Contract",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_MarketingOrganization_MarketerOrganizationId",
                table: "Contract",
                column: "MarketerOrganizationId",
                principalTable: "MarketingOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_RecruitingOrganization_RecruiterOrganizationId",
                table: "Contract",
                column: "RecruiterOrganizationId",
                principalTable: "RecruitingOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationAccountManager_AccountManagerOrganizationId_AccountManagerId",
                table: "Contract",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" },
                principalTable: "OrganizationAccountManager",
                principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationCustomer_BuyerOrganizationId_CustomerId",
                table: "Contract",
                columns: new[] { "BuyerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationContractor_ContractorOrganizationId_ContractorId",
                table: "Contract",
                columns: new[] { "ContractorOrganizationId", "ContractorId" },
                principalTable: "OrganizationContractor",
                principalColumns: new[] { "OrganizationId", "ContractorId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_OrganizationMarketer_MarketerOrganizationId_MarketerId",
                table: "Contract",
                columns: new[] { "MarketerOrganizationId", "MarketerId" },
                principalTable: "OrganizationMarketer",
                principalColumns: new[] { "OrganizationId", "MarketerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccount_ProviderOrganization_AccountManagerOrganizationId",
                table: "CustomerAccount",
                column: "AccountManagerOrganizationId",
                principalTable: "ProviderOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccount_Organization_CustomerOrganizationId",
                table: "CustomerAccount",
                column: "CustomerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccount_Customer_CustomerId",
                table: "CustomerAccount",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccount_OrganizationAccountManager_AccountManagerOrganizationId_AccountManagerId",
                table: "CustomerAccount",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" },
                principalTable: "OrganizationAccountManager",
                principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccount_OrganizationCustomer_CustomerOrganizationId_CustomerId",
                table: "CustomerAccount",
                columns: new[] { "CustomerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_ProviderOrganization_ProviderOrganizationId",
                table: "Lead",
                column: "ProviderOrganizationId",
                principalTable: "ProviderOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Organization_MarketerOrganizationId",
                table: "Lead",
                column: "MarketerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_OrganizationAccountManager_AccountManagerOrganizationId_AccountManagerId",
                table: "Lead",
                columns: new[] { "AccountManagerOrganizationId", "AccountManagerId" },
                principalTable: "OrganizationAccountManager",
                principalColumns: new[] { "OrganizationId", "AccountManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_OrganizationMarketer_MarketerOrganizationId_MarketerId",
                table: "Lead",
                columns: new[] { "MarketerOrganizationId", "MarketerId" },
                principalTable: "OrganizationMarketer",
                principalColumns: new[] { "OrganizationId", "MarketerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationAccountManager_Organization_OrganizationId",
                table: "OrganizationAccountManager",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationAccountManager_OrganizationPerson_OrganizationId_AccountManagerId",
                table: "OrganizationAccountManager",
                columns: new[] { "OrganizationId", "AccountManagerId" },
                principalTable: "OrganizationPerson",
                principalColumns: new[] { "OrganizationId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProviderOrganization_ProjectManagerOrganizationId",
                table: "Project",
                column: "ProjectManagerOrganizationId",
                principalTable: "ProviderOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Organization_CustomerOrganizationId",
                table: "Project",
                column: "CustomerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_OrganizationProjectManager_ProjectManagerOrganizationId_ProjectManagerId",
                table: "Project",
                columns: new[] { "ProjectManagerOrganizationId", "ProjectManagerId" },
                principalTable: "OrganizationProjectManager",
                principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Customer_CustomerId",
                table: "Project",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_OrganizationCustomer_CustomerOrganizationId_CustomerId",
                table: "Project",
                columns: new[] { "CustomerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInvoice_Organization_BuyerOrganizationId",
                table: "ProjectInvoice",
                column: "BuyerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInvoice_Organization_ProviderOrganizationId",
                table: "ProjectInvoice",
                column: "ProviderOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInvoice_OrganizationProjectManager_ProviderOrganizationId_ProjectManagerId",
                table: "ProjectInvoice",
                columns: new[] { "ProviderOrganizationId", "ProjectManagerId" },
                principalTable: "OrganizationProjectManager",
                principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInvoice_Customer_CustomerId",
                table: "ProjectInvoice",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInvoice_OrganizationCustomer_BuyerOrganizationId_CustomerId",
                table: "ProjectInvoice",
                columns: new[] { "BuyerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRetainerIntent_Organization_CustomerOrganizationId",
                table: "ProjectRetainerIntent",
                column: "CustomerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRetainerIntent_Organization_ProviderOrganizationId",
                table: "ProjectRetainerIntent",
                column: "ProviderOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRetainerIntent_Customer_CustomerId",
                table: "ProjectRetainerIntent",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRetainerIntent_OrganizationCustomer_CustomerOrganizationId_CustomerId",
                table: "ProjectRetainerIntent",
                columns: new[] { "CustomerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_OrganizationProjectManager_ProviderOrganizationId_ProjectManagerId",
                table: "TimeEntry",
                columns: new[] { "ProviderOrganizationId", "ProjectManagerId" },
                principalTable: "OrganizationProjectManager",
                principalColumns: new[] { "OrganizationId", "ProjectManagerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_OrganizationRecruiter_RecruitingOrganizationId_RecruiterId",
                table: "TimeEntry",
                columns: new[] { "RecruitingOrganizationId", "RecruiterId" },
                principalTable: "OrganizationRecruiter",
                principalColumns: new[] { "OrganizationId", "RecruiterId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Story_StoryId",
                table: "TimeEntry",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Contractor_ContractorId",
                table: "TimeEntry",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Customer_CustomerId",
                table: "TimeEntry",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Customer_MarketingAgencyOwnerId",
                table: "TimeEntry",
                column: "MarketingAgencyOwnerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Customer_ProviderAgencyOwnerId",
                table: "TimeEntry",
                column: "ProviderAgencyOwnerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Customer_RecruitingAgencyOwnerId",
                table: "TimeEntry",
                column: "RecruitingAgencyOwnerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_OrganizationCustomer_CustomerOrganizationId_CustomerId",
                table: "TimeEntry",
                columns: new[] { "CustomerOrganizationId", "CustomerId" },
                principalTable: "OrganizationCustomer",
                principalColumns: new[] { "OrganizationId", "CustomerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_OrganizationContractor_ProviderOrganizationId_ContractorId",
                table: "TimeEntry",
                columns: new[] { "ProviderOrganizationId", "ContractorId" },
                principalTable: "OrganizationContractor",
                principalColumns: new[] { "OrganizationId", "ContractorId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_OrganizationMarketer_MarketingOrganizationId_MarketerId",
                table: "TimeEntry",
                columns: new[] { "MarketingOrganizationId", "MarketerId" },
                principalTable: "OrganizationMarketer",
                principalColumns: new[] { "OrganizationId", "MarketerId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_ProviderOrganization_AccountManagerOrganizationId",
                table: "WorkOrder",
                column: "AccountManagerOrganizationId",
                principalTable: "ProviderOrganization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_Organization_CustomerOrganizationId",
                table: "WorkOrder",
                column: "CustomerOrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_Customer_CustomerId",
                table: "WorkOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationBillingCategory_Organization_OrganizationId",
                table: "OrganizationBillingCategory",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualBonusIntent_Candidate_CandidateId",
                table: "IndividualBonusIntent",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualBonusIntent_OrganizationPerson_OrganizationId_PersonId",
                table: "IndividualBonusIntent",
                columns: new[] { "OrganizationId", "PersonId" },
                principalTable: "OrganizationPerson",
                principalColumns: new[] { "OrganizationId", "PersonId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationBonusIntent_Organization_OrganizationId",
                table: "OrganizationBonusIntent",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationBonusIntent_Candidate_CandidateId",
                table: "OrganizationBonusIntent",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCard_Customer_CustomerId1",
                table: "CustomerCard",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualBuyerAccount_Customer_Id",
                table: "IndividualBuyerAccount",
                column: "Id",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationBuyerAccount_Organization_Id",
                table: "OrganizationBuyerAccount",
                column: "Id",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateStatusTransition_Candidate_CandidateId",
                table: "CandidateStatusTransition",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Candidate_CandidateId",
                table: "Comment",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Story_StoryId",
                table: "Comment",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_OrganizationPerson_OrganizationId_CreatedById",
                table: "Comment",
                columns: new[] { "OrganizationId", "CreatedById" },
                principalTable: "OrganizationPerson",
                principalColumns: new[] { "OrganizationId", "PersonId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Candidate_CandidateId",
                table: "Notification",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Story_StoryId",
                table: "Notification",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_Customer_CustomerId",
                table: "Organization",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Person_Id",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketer_Person_Id",
                table: "Marketer");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationPerson_Person_PersonId",
                table: "OrganizationPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMarketer_Organization_OrganizationId",
                table: "OrganizationMarketer");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationPerson_Organization_OrganizationId",
                table: "OrganizationPerson");

            migrationBuilder.DropTable(
                name: "AccountCard");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "CandidateStatusTransition");

            migrationBuilder.DropTable(
                name: "CategoryBillingCategory");

            migrationBuilder.DropTable(
                name: "CategoryPaymentTerm");

            migrationBuilder.DropTable(
                name: "CategoryPosition");

            migrationBuilder.DropTable(
                name: "CategorySkill");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ContractorSkill");

            migrationBuilder.DropTable(
                name: "ContractStatusTransition");

            migrationBuilder.DropTable(
                name: "CustomerAccountStatusTransition");

            migrationBuilder.DropTable(
                name: "CustomerCard");

            migrationBuilder.DropTable(
                name: "EnabledCountry");

            migrationBuilder.DropTable(
                name: "ExceptionLog");

            migrationBuilder.DropTable(
                name: "IndividualBonusIntent");

            migrationBuilder.DropTable(
                name: "IndividualBuyerAccount");

            migrationBuilder.DropTable(
                name: "IndividualFinancialAccount");

            migrationBuilder.DropTable(
                name: "IndividualPayoutIntent");

            migrationBuilder.DropTable(
                name: "LanguageCountry");

            migrationBuilder.DropTable(
                name: "LeadStatusTransition");

            migrationBuilder.DropTable(
                name: "MarketingAgreement");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OrganizationBillingCategory");

            migrationBuilder.DropTable(
                name: "OrganizationBonusIntent");

            migrationBuilder.DropTable(
                name: "OrganizationBuyerAccount");

            migrationBuilder.DropTable(
                name: "OrganizationFinancialAccount");

            migrationBuilder.DropTable(
                name: "OrganizationPaymentTerm");

            migrationBuilder.DropTable(
                name: "OrganizationPayoutIntent");

            migrationBuilder.DropTable(
                name: "OrganizationPersonWidget");

            migrationBuilder.DropTable(
                name: "OrganizationPosition");

            migrationBuilder.DropTable(
                name: "OrganizationSkill");

            migrationBuilder.DropTable(
                name: "OrganizationSubscription");

            migrationBuilder.DropTable(
                name: "PremiumOrganization");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProjectBillingCategory");

            migrationBuilder.DropTable(
                name: "ProjectInvoice");

            migrationBuilder.DropTable(
                name: "ProjectStatusTransition");

            migrationBuilder.DropTable(
                name: "ProposalAcceptance");

            migrationBuilder.DropTable(
                name: "ProposalStatusTransition");

            migrationBuilder.DropTable(
                name: "ProposalWorkOrder");

            migrationBuilder.DropTable(
                name: "ProvinceState");

            migrationBuilder.DropTable(
                name: "RecruitingAgreement");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "StoryStatusTransition");

            migrationBuilder.DropTable(
                name: "StripeApplicationFee");

            migrationBuilder.DropTable(
                name: "StripeBalanceTransaction");

            migrationBuilder.DropTable(
                name: "StripeCharge");

            migrationBuilder.DropTable(
                name: "StripeCheckoutSession");

            migrationBuilder.DropTable(
                name: "StripeInvoiceLine");

            migrationBuilder.DropTable(
                name: "StripePlan");

            migrationBuilder.DropTable(
                name: "StripeSource");

            migrationBuilder.DropTable(
                name: "StripeSubscriptionItem");

            migrationBuilder.DropTable(
                name: "TimeEntryStatusTransition");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "StripeCard");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Lead");

            migrationBuilder.DropTable(
                name: "BonusTransfer");

            migrationBuilder.DropTable(
                name: "InvoiceTransfer");

            migrationBuilder.DropTable(
                name: "CategoryWidget");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "WorkOrder");

            migrationBuilder.DropTable(
                name: "StripePayout");

            migrationBuilder.DropTable(
                name: "StripePaymentIntent");

            migrationBuilder.DropTable(
                name: "ProjectRetainerIntent");

            migrationBuilder.DropTable(
                name: "TimeEntry");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "StripeTransfer");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.DropTable(
                name: "StripeInvoiceItem");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "BillingCategory");

            migrationBuilder.DropTable(
                name: "FinancialAccount");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "StripeInvoice");

            migrationBuilder.DropTable(
                name: "StoryTemplate");

            migrationBuilder.DropTable(
                name: "MarketingOrganization");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "RecruitingOrganization");

            migrationBuilder.DropTable(
                name: "BuyerAccount");

            migrationBuilder.DropTable(
                name: "StripeSubscription");

            migrationBuilder.DropTable(
                name: "CustomerAccount");

            migrationBuilder.DropTable(
                name: "ProviderOrganization");

            migrationBuilder.DropTable(
                name: "PaymentTerm");

            migrationBuilder.DropTable(
                name: "OrganizationCustomer");

            migrationBuilder.DropTable(
                name: "OrganizationAccountManager");

            migrationBuilder.DropTable(
                name: "OrganizationContractor");

            migrationBuilder.DropTable(
                name: "OrganizationProjectManager");

            migrationBuilder.DropTable(
                name: "AccountManager");

            migrationBuilder.DropTable(
                name: "Contractor");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "ProjectManager");

            migrationBuilder.DropTable(
                name: "OrganizationRecruiter");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "Recruiter");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "OrganizationMarketer");

            migrationBuilder.DropTable(
                name: "Marketer");

            migrationBuilder.DropTable(
                name: "OrganizationPerson");
        }
    }
}
