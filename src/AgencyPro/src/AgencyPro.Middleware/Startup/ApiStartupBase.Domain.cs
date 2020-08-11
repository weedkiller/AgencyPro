// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BillingCategories.Services;
using AgencyPro.Core.Candidates.Services;
using AgencyPro.Core.Categories.Services;
using AgencyPro.Core.Chart.Services;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.FinancialAccounts.Services;
using AgencyPro.Core.Geo.Services;
using AgencyPro.Core.Leads.Services;
using AgencyPro.Core.Lookup.Services;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Services;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Projects.Services;
using AgencyPro.Core.Proposals.Services;
using AgencyPro.Core.Roles.Services;
using AgencyPro.Core.Services;
using AgencyPro.Core.Skills.Services;
using AgencyPro.Core.Stories.Services;
using AgencyPro.Core.TimeEntries.Services;
using AgencyPro.Services.BillingCategories;
using AgencyPro.Services.Candidates;
using AgencyPro.Services.Candidates.Messaging;
using AgencyPro.Services.Categories;
using AgencyPro.Services.Chart;
using AgencyPro.Services.Contracts;
using AgencyPro.Services.CustomerAccounts;
using AgencyPro.Services.FinancialAccounts;
using AgencyPro.Services.Geo;
using AgencyPro.Services.Invoices;
using AgencyPro.Services.Leads;
using AgencyPro.Services.Lookup;
using AgencyPro.Services.OrganizationPeople;
using AgencyPro.Services.OrganizationRoles.OrganizationAccountManagers;
using AgencyPro.Services.OrganizationRoles.OrganizationContractors;
using AgencyPro.Services.OrganizationRoles.OrganizationCustomers;
using AgencyPro.Services.OrganizationRoles.OrganizationMarketers;
using AgencyPro.Services.OrganizationRoles.OrganizationProjectManagers;
using AgencyPro.Services.OrganizationRoles.OrganizationRecruiters;
using AgencyPro.Services.Organizations;
using AgencyPro.Services.OrganizationSkills;
using AgencyPro.Services.Projects;
using AgencyPro.Services.Proposals;
using AgencyPro.Services.Proposals.Messaging;
using AgencyPro.Services.Roles.AccountManagers;
using AgencyPro.Services.Roles.Contractors;
using AgencyPro.Services.Roles.Marketers;
using AgencyPro.Services.Roles.ProjectManagers;
using AgencyPro.Services.Roles.Recruiters;
using AgencyPro.Services.Stories;
using Microsoft.Extensions.DependencyInjection;
using System;
using AgencyPro.Core.ActivityFeed.Services;
using AgencyPro.Core.Agreements.Services;
using AgencyPro.Core.BonusIntents.Services;
using AgencyPro.Core.BuyerAccounts.Services;
using AgencyPro.Core.Cards.Services;
using AgencyPro.Core.Charges.Services;
using AgencyPro.Core.Comments.Services;
using AgencyPro.Core.Commission.Services;
using AgencyPro.Core.CustomerBalance.Services;
using AgencyPro.Core.DisperseFunds.Services;
using AgencyPro.Core.FinancialInfo.Services;
using AgencyPro.Core.ForgotPassword.Services;
using AgencyPro.Core.Invoices.Services;
using AgencyPro.Core.Levels.Services;
using AgencyPro.Core.Login.Services;
using AgencyPro.Core.Orders.Services;
using AgencyPro.Core.Organizations.MarketingOrganizations.Services;
using AgencyPro.Core.Organizations.ProviderOrganizations.Services;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Services;
using AgencyPro.Core.PaymentIntents.Services;
using AgencyPro.Core.PaymentTerms.Services;
using AgencyPro.Core.Plans.Services;
using AgencyPro.Core.Positions.Services;
using AgencyPro.Core.Products.Services;
using AgencyPro.Core.Registration.Services;
using AgencyPro.Core.ResetPassword.Services;
using AgencyPro.Core.Retainers.Services;
using AgencyPro.Core.StoryTemplates.Services;
using AgencyPro.Core.Stripe.Services;
using AgencyPro.Core.StripeSources.Services;
using AgencyPro.Core.Subscriptions.Services;
using AgencyPro.Core.UserAccount.Models;
using AgencyPro.Services.Account;
using AgencyPro.Services.ActivityFeed;
using AgencyPro.Services.Agreements;
using AgencyPro.Services.Agreements.Messaging;
using AgencyPro.Services.BonusIntents;
using AgencyPro.Services.BuyerAccounts;
using AgencyPro.Services.Cards;
using AgencyPro.Services.Charges;
using AgencyPro.Services.Comments;
using AgencyPro.Services.Commission;
using AgencyPro.Services.ContractorSkills;
using AgencyPro.Services.Contracts.EventHandlers;
using AgencyPro.Services.CustomerBalance;
using AgencyPro.Services.DisperseFunds;
using AgencyPro.Services.DisperseFunds.EventHandlers;
using AgencyPro.Services.FinancialInfo;
using AgencyPro.Services.ForgotPassword;
using AgencyPro.Services.ForgotPassword.EventHandlers;
using AgencyPro.Services.Invoices.Messaging;
using AgencyPro.Services.Leads.EventHandlers;
using AgencyPro.Services.Levels;
using AgencyPro.Services.Login;
using AgencyPro.Services.Login.EventHandlers;
using AgencyPro.Services.MarketingOrganizations;
using AgencyPro.Services.Orders;
using AgencyPro.Services.Orders.Messaging;
using AgencyPro.Services.OrganizationPayoutIntents;
using AgencyPro.Services.OrganizationPeople.Messaging;
using AgencyPro.Services.PaymentTerms;
using AgencyPro.Services.Positions;
using AgencyPro.Services.Products;
using AgencyPro.Services.Projects.Messaging;
using AgencyPro.Services.ProviderOrganizations;
using AgencyPro.Services.RecruitingOrganizations;
using AgencyPro.Services.Registration;
using AgencyPro.Services.Registration.EventHandlers;
using AgencyPro.Services.ResetPassword;
using AgencyPro.Services.ResetPassword.EventHandlers;
using AgencyPro.Services.Retainers;
using AgencyPro.Services.Stories.EmailNotifications;
using AgencyPro.Services.StoryTemplates;
using AgencyPro.Services.Stripe;
using AgencyPro.Services.StripeSources;
using AgencyPro.Services.TimeEntries;
using AgencyPro.Services.TimeEntries.EventHandlers;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using CustomerService = AgencyPro.Services.Roles.Customers.CustomerService;
using PersonService = AgencyPro.Services.People.PersonService;
using PlanService = AgencyPro.Services.Plans.PlanService;
using SubscriptionService = AgencyPro.Services.Subscriptions.SubscriptionService;

namespace AgencyPro.Middleware.Startup
{
    
    public abstract partial class ApiStartupBase
    {
        protected void ConfigureDomainServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IService<>), typeof(Services.Service<>));
            
            services.AddScoped(typeof(IGeoService), typeof(GeoService));
            services.AddScoped(typeof(ILookupService), typeof(LookupService));

            services.AddScoped(typeof(IPersonService), typeof(PersonService));
            services.AddScoped(typeof(IAccountManagerService), typeof(AccountManagerService));
            services.AddScoped(typeof(IProjectManagerService), typeof(ProjectManagerService));
            services.AddScoped(typeof(IRecruiterService), typeof(RecruiterService));
            services.AddScoped(typeof(IMarketerService), typeof(MarketerService));
            services.AddScoped(typeof(IContractorService), typeof(ContractorService));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            services.AddScoped(typeof(ICandidateService), typeof(CandidateService));
            services.AddScoped(typeof(ILeadService), typeof(LeadService));
            services.AddScoped(typeof(IStoryService), typeof(StoryService));
            services.AddScoped(typeof(IProjectInvoiceService), typeof(ProjectInvoiceService));
            services.AddScoped(typeof(ITimeMatrixService), typeof(TimeMatrixService));
            services.AddScoped(typeof(ILeadMatrixService), typeof(LeadMatrixService));
            services.AddScoped(typeof(ITimeEntryService), typeof(TimeEntryService));
            services.AddScoped(typeof(IChartService), typeof(ChartService));
            services.AddScoped(typeof(ICommentService), typeof(CommentService));
            services.AddScoped(typeof(IPlanService), typeof(PlanService));
            services.AddScoped(typeof(IProductService), typeof(ProductService));
            services.AddScoped(typeof(IPaymentIntentService), typeof(PaymentIntentService));


            services.AddScoped(typeof(IOrganizationAccountManagerService), typeof(OrganizationAccountManagerService));
            services.AddScoped(typeof(IOrganizationProjectManagerService), typeof(OrganizationProjectManagerService));
            services.AddScoped(typeof(IOrganizationRecruiterService), typeof(OrganizationRecruiterService));
            services.AddScoped(typeof(IOrganizationMarketerService), typeof(OrganizationMarketerService));
            services.AddScoped(typeof(IOrganizationContractorService), typeof(OrganizationContractorService));
            services.AddScoped(typeof(IOrganizationCustomerService), typeof(OrganizationCustomerService));
            services.AddScoped(typeof(IOrganizationPersonService), typeof(OrganizationPersonService));
            services.AddScoped(typeof(ICardService), typeof(CardService));
            services.AddScoped(typeof(IChargeService), typeof(ChargeService));
            services.AddScoped(typeof(ISourceService), typeof(SourceService));

            services.AddScoped(typeof(IOrganizationService), typeof(OrganizationService));
            services.AddScoped(typeof(IMarketingOrganizationService), typeof(MarketingOrganizationService));
            services.AddScoped(typeof(IProviderOrganizationService), typeof(ProviderOrganizationService));
            services.AddScoped(typeof(IRecruitingOrganizationService), typeof(RecruitingOrganizationService));
            services.AddScoped(typeof(ICustomerAccountService), typeof(CustomerAccountService));
            services.AddScoped(typeof(IProjectService), typeof(ProjectService));
            services.AddScoped(typeof(IContractService), typeof(ContractService));
            services.AddScoped(typeof(IProposalService), typeof(ProposalService));
            services.AddScoped(typeof(ICategoryService), typeof(CategoryService));
            services.AddScoped(typeof(IInvoiceProjectSummaryService), typeof(InvoiceProjectSummaryService));
            services.AddScoped(typeof(IBillingCategoryService), typeof(BillingCategoryService));
            services.AddScoped(typeof(IFinancialAccountService), typeof(FinancialAccountService));
            services.AddScoped(typeof(IOrganizationSkillService), typeof(OrganizationSkillService));
            services.AddScoped(typeof(IContractorSkillService), typeof(ContractorSkillService));
            services.AddScoped(typeof(IPaymentTermService), typeof(PaymentTermService));
            services.AddScoped(typeof(IFinancialInfoService), typeof(FinancialInfoService));
            services.AddScoped(typeof(IWorkOrderService), typeof(WorkOrderService));
            services.AddScoped(typeof(IStoryTemplateService), typeof(StoryTemplateService));
            services.AddScoped(typeof(IMarketingAgreementService), typeof(MarketingAgreementService));
            services.AddScoped(typeof(IRecruitingAgreementService), typeof(RecruitingAgreementService));
            services.AddScoped(typeof(IBuyerAccountService), typeof(BuyerAccountService));
            services.AddScoped(typeof(IRegistrationService), typeof(RegistrationService));
            services.AddScoped(typeof(IForgotPasswordService), typeof(ForgotPasswordService));
            services.AddScoped(typeof(IResetPasswordService), typeof(ResetPasswordService));
            services.AddScoped(typeof(ILoginService), typeof(LoginService));
            services.AddScoped(typeof(ICommissionService), typeof(CommissionService));
            services.AddScoped(typeof(IDisperseFundsService), typeof(DisperseFundsService));
            services.AddScoped(typeof(ICustomerBalanceService), typeof(CustomerBalanceService));
            services.AddScoped(typeof(ILevelService), typeof(LevelService));
            services.AddScoped(typeof(IPositionService), typeof(PositionService));
            services.AddScoped(typeof(IProposalPDFService), typeof(ProposalPDFService));
            services.AddScoped(typeof(IOrganizationBonusIntentService), typeof(OrganizationBonusIntentService));
            services.AddScoped(typeof(IIndividualBonusIntentService), typeof(IndividualBonusIntentService));
            services.AddScoped(typeof(IRetainerService), typeof(RetainerService));

            services.AddScoped<MultiProjectEventHandler>();
            services.AddScoped<MultiLeadEventHandler>();
            services.AddScoped<TimeEntryEventHandlers>();
            services.AddScoped<MultiCandidateEventsHandler>();
            services.AddScoped<MultiProposalEventHandler>();
            services.AddScoped<StoryEventHandlers>();
            services.AddScoped<MultiContractEventHandler>();
            services.AddScoped<MultiMarketingAgreementEventHandler>();
            services.AddScoped<InvoicesEventHandlers>();
            services.AddScoped<MultiWorkOrderEventsHandler>();
            services.AddScoped<OrganizationPersonEventHandler>();
            services.AddScoped<RegistrationEventHandlers>();
            services.AddScoped<ForgotPasswordEventHandlers>();
            services.AddScoped<ResetPasswordEventHandlers>();
            services.AddScoped<LoginEventHandlers>();
            services.AddScoped<FundsDispersalEventHandlers>();

            services.AddTransient(typeof(Lazy<>), typeof(Lazier<>));
            services.AddTransient(typeof(ISubscriptionService), typeof(SubscriptionService));

            services.AddScoped<SignInManager<ApplicationUser>, UserAccountSignInManager>();
            services.AddScoped<IProfileService, IdentityWithAdditionalClaimsProfileService>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalUserClaimsPrincipalFactory>();
            services.AddScoped<IResourceOwnerPasswordValidator, UserAccountSignInManager>();
            services.AddScoped<IActivityFeedService, ActivityFeedService>();
            services.AddScoped<UserManager<ApplicationUser>, UserAccountManager>();
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            // configure stripe
            services.AddScoped(typeof(IStripeService), typeof(StripeService));
            services.AddScoped(typeof(Stripe.TokenService));
            services.AddScoped(typeof(Stripe.AccountService));
            services.AddScoped(typeof(Stripe.ProductService));
            services.AddScoped(typeof(Stripe.PlanService));
            services.AddScoped(typeof(Stripe.SubscriptionService));
            services.AddScoped(typeof(Stripe.PersonService));
            services.AddScoped(typeof(Stripe.CapabilityService));
            services.AddScoped(typeof(Stripe.FileService));
            services.AddScoped(typeof(Stripe.CustomerService));
            services.AddScoped(typeof(Stripe.SubscriptionItemService));
            services.AddScoped(typeof(Stripe.TransferService));
            services.AddScoped(typeof(Stripe.SourceService));
            services.AddScoped(typeof(Stripe.InvoiceService));
            services.AddScoped(typeof(Stripe.InvoiceItemService));
            services.AddScoped(typeof(Stripe.PaymentIntentService));
            services.AddScoped(typeof(Stripe.ExternalAccountService));
            services.AddScoped(typeof(Stripe.CardService));


        }
    }
    internal class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        {
        }
    }
}