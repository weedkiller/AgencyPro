// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Common.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.ExceptionLog;
using AgencyPro.Core.Geo.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Core.TimeEntries.Models;
using AgencyPro.Core.Widgets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.Charges.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.Plans.Models;
using AgencyPro.Core.Products.Models;
using AgencyPro.Core.Stripe.Model;
using AgencyPro.Core.Transactions.Models;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Data.EFCore
{
    public class AppDbContext
        : DataContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> UsersAccounts { get; set; }
        public DbSet<ExceptionLog> Exceptions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EnabledCountry> EnabledCountries { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageCountry> CountryLanguages { get; set; }
        public DbSet<ProvinceState> ProvinceStates { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LeadNotification> LeadNotifications { get; set; }
        public DbSet<CandidateNotification> CandidateNotifications { get; set; }
        public DbSet<ContractNotification> ContractNotifications { get; set; }
        public DbSet<ProjectNotification> ProjectNotifications { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AccountManager> AccountManagers { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Marketer> Marketers { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<Recruiter> Recruiters { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<OrganizationSkill> OrganizationSkills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySkill> SkillCategories { get; set; }
        public DbSet<FixedPriceProposal> FixedPriceProposals { get; set; }
        public DbSet<OrganizationPerson> OrganizationPeople { get; set; }
        public DbSet<OrganizationContractor> OrganizationContractors { get; set; }
        public DbSet<OrganizationCustomer> OrganizationCustomers { get; set; }
        public DbSet<OrganizationMarketer> OrganizationMarketers { get; set; }
        public DbSet<OrganizationRecruiter> OrganizationRecruiters { get; set; }
        public DbSet<OrganizationProjectManager> OrganizationProjectManagers { get; set; }
        public DbSet<OrganizationAccountManager> OrganizationAccountManagers { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<ProjectInvoice> Invoices { get; set; }
        public DbSet<OrganizationPersonWidget> OrganizationPersonWidgets { get; set; }
        public DbSet<CategoryWidget> WidgetCategories { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        public DbQuery<TimeMatrix> TimeMatrix { get; set; }
        public DbSet<BillingCategory> BillingCategories { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<StripePlan> Plans { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceTransfer> InvoiceTransfers { get; set; }

        public DbSet<StripeApplicationFee> ApplicationFees { get; set; }
        public DbSet<StripeBalanceTransaction> BalanceTransactions { get; set; }
        public DbSet<StripeCharge> Charges { get; set; }
        public DbSet<MarketingAgreement> MarketingAgreements { get; set; }
        public DbSet<RecruitingAgreement> RecruitingAgreements { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<ProposalNotification> ProposalNotifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var internalBuilder = modelBuilder.GetInfrastructure();
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                internalBuilder
                    .Entity(entity.Name, ConfigurationSource.Convention)
                    .Relational(ConfigurationSource.Convention)
                    .ToTable(entity.ClrType.Name);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Query<LeadMatrix>().ToQuery(() =>

                Leads.Where(x => x.IsDeleted == false).GroupBy(t => new
                {
                    t.MarketerId,
                    t.MarketerOrganizationId,
                    t.Created.Date,
                    t.Status
                }).Select(g => new LeadMatrix()
                {
                    MarketerId = g.Key.MarketerId,
                    MarketerOrganizationId = g.Key.MarketerOrganizationId,
                    Status = g.Key.Status,
                    Date = g.Key.Date,
                    Count = g.Count()
                }));

            modelBuilder.Query<TimeMatrix>().ToQuery(() =>

                TimeEntries.Where(x => x.IsDeleted == false).GroupBy(t => new
                {
                    t.ContractId,
                    t.StartDate.Date,
                    t.RecruiterId,
                    t.RecruitingOrganizationId,
                    t.MarketerId,
                    t.MarketingOrganizationId,
                    t.ContractorId,
                    t.ProviderOrganizationId,
                    t.AccountManagerId,
                    t.ProjectManagerId,
                    t.CustomerId,
                    t.CustomerOrganizationId,
                    t.ProjectId,
                    t.StoryId,
                    t.Status,
                    t.TimeType,
                    t.TotalAccountManagerStream,
                    t.TotalAgencyStream,
                    t.TotalRecruitingAgencyStream,
                    t.TotalMarketingAgencyStream,
                    t.TotalContractorStream,
                    t.TotalCustomerAmount,
                    t.TotalMarketerStream,
                    t.TotalProjectManagerStream,
                    t.TotalRecruiterStream,
                    t.TotalSystemStream

                }).Select(g => new TimeMatrix()
                {
                    ProjectId = g.Key.ProjectId,
                    StoryId = g.Key.StoryId,
                    TimeType = g.Key.TimeType,
                    TimeStatus = g.Key.Status,
                    TotalAccountManagerStream = g.Key.TotalAccountManagerStream,
                    TotalAgencyStream = g.Key.TotalAgencyStream,
                    TotalMarketingAgencyStream = g.Key.TotalMarketingAgencyStream,
                    TotalRecruitingAgencyStream = g.Key.TotalRecruitingAgencyStream,
                    TotalContractorStream = g.Key.TotalContractorStream,
                    TotalCustomerAmount = g.Key.TotalCustomerAmount,
                    TotalMarketerStream = g.Key.TotalMarketerStream,
                    TotalProjectManagerStream = g.Key.TotalProjectManagerStream,
                    TotalRecruiterStream = g.Key.TotalRecruiterStream,
                    TotalSystemStream = g.Key.TotalSystemStream,

                    RecruiterId = g.Key.RecruiterId,

                    RecruiterOrganizationId = g.Key.RecruitingOrganizationId,
                    MarketerId = g.Key.MarketerId,
                    MarketerOrganizationId = g.Key.MarketingOrganizationId,
                    ContractorId = g.Key.ContractorId,
                    ProviderOrganizationId = g.Key.ProviderOrganizationId,
                    AccountManagerId = g.Key.AccountManagerId,
                    ProjectManagerId = g.Key.ProjectManagerId,
                    CustomerId = g.Key.CustomerId,
                    CustomerOrganizationId = g.Key.CustomerOrganizationId,
                    Date = g.Key.Date,
                    ContractId = g.Key.ContractId,
                    Hours = g.Sum(e => e.TotalHours)

                })
            );
            modelBuilder.Query<TimeMatrix>().HasOne(x => x.Contract).WithOne()
                .HasForeignKey<TimeMatrix>(x => x.ContractId);

            modelBuilder.Entity<Notification>()
                .ToTable("Notification")
                .HasDiscriminator(x => x.Type)
                .HasValue<LeadNotification>(NotificationType.Lead)
                .HasValue<CandidateNotification>(NotificationType.Candidate)
                .HasValue<PersonNotification>(NotificationType.Person)
                .HasValue<WorkOrderNotification>(NotificationType.WorkOrder)
                .HasValue<ContractNotification>(NotificationType.Contract)
                .HasValue<ProjectNotification>(NotificationType.Project)
                .HasValue<ProposalNotification>(NotificationType.Proposal)
                .HasValue<StoryNotification>(NotificationType.Story)
                .HasValue<TimeEntryNotification>(NotificationType.TimeEntry)
                .HasValue<UserNotification>(NotificationType.User)
                .HasValue<SystemNotification>(NotificationType.System);

            modelBuilder.Entity<Notification>()
               .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Notification>()
               .Property(p => p.Message)
                .HasMaxLength(200);

            modelBuilder.Entity<Notification>()
                .Property<DateTimeOffset>("Created")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            modelBuilder.Entity<Notification>()
                .Property<DateTimeOffset>("Updated")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");


            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.Project).WithOne();

            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationContractor).WithOne();
            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationAccountManager).WithOne();
            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationCustomer).WithOne();
            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationRecruiter).WithOne();
            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationMarketer).WithOne();
            //modelBuilder.Query<TimeMatrix>().HasOne(x => x.OrganizationProjectManager).WithOne();
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            //https://stackoverflow.com/questions/53678014/the-update-statement-conflicted-with-the-foreign-key-constraint-in-ef-core
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}