// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.BonusIntents;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Organizations.Services;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.Positions.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.StoryTemplates.Models;

namespace AgencyPro.Core.Organizations.Models
{
    public class Organization : AuditableEntity, IOrganization
    {
        public Category Category { get; set; }

        public PremiumOrganization PremiumOrganization { get; set; }
        public MarketingOrganization MarketingOrganization { get; set; }
        public RecruitingOrganization RecruitingOrganization { get; set; }
        public ProviderOrganization ProviderOrganization { get; set; }

        public ICollection<OrganizationProjectManager> ProjectManagers { get; set; }
        public ICollection<OrganizationAccountManager> AccountManagers { get; set; }
        public ICollection<OrganizationContractor> Contractors { get; set; }
        public ICollection<OrganizationCustomer> Customers { get; set; }
        public ICollection<OrganizationRecruiter> Recruiters { get; set; }
        public ICollection<OrganizationMarketer> Marketers { get; set; }
        public ICollection<OrganizationPosition> Positions { get; set; }

        public ICollection<ProjectInvoice> ProviderInvoices { get; set; }
        public ICollection<ProjectInvoice> BuyerInvoices { get; set; }

        public ICollection<Contract> BuyerContracts { get; set; }


        public ICollection<OrganizationPerson> OrganizationPeople { get; set; }
        public ICollection<OrganizationBillingCategory> BillingCategories { get; set; }
        public ICollection<OrganizationPaymentTerm> PaymentTerms { get; set; }

        public ICollection<IndividualPayoutIntent> IndividualPayoutIntents { get; set; }

        public OrganizationFinancialAccount OrganizationFinancialAccount { get; set; }
        public OrganizationBuyerAccount OrganizationBuyerAccount { get; set; }
        public OrganizationSubscription OrganizationSubscription { get; set; }

        public ICollection<CustomerAccount> BuyerCustomerAccounts { get; set; }

        public ICollection<Project> BuyerProjects { get; set; }
        public ICollection<OrganizationPayoutIntent> PayoutIntents { get; set; }
        public ICollection<OrganizationBonusIntent> BonusIntents { get; set; }

        public ICollection<Lead> Leads { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<WorkOrder> BuyerWorkOrders { get; set; }
        public ICollection<StoryTemplate> StoryTemplates { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }

        public string ColumnBgColor { get; set; }
        public string MenuBgHoverColor { get; set; }
        public string HoverItemColor { get; set; }
        public string TextColor { get; set; }
        public string ActiveItemColor { get; set; }
        public string ActivePresenceColor { get; set; }
        public string ActiveItemTextColor { get; set; }
        public string MentionBadgeColor { get; set; }


        public int CategoryId { get; set; }

        public OrganizationType OrganizationType { get; set; }
        
        public Guid UpdatedById { get; set; }
        public Guid CreatedById { get; set; }

        public string AffiliateInformation { get; set; }
        public string ProviderInformation { get; set; }
        
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
        public string PostalCode { get; set; }

        public Guid CustomerId { get; set; }
        public Roles.Models.Customer Customer { get; set; }
        public ICollection<ProjectRetainerIntent> BuyerRetainerIntents { get; set; }
        public IEnumerable<ProjectRetainerIntent> ProviderRetainerIntents { get; set; }
    }
}