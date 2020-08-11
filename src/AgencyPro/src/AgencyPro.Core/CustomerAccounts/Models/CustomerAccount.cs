// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.CustomerAccounts.Services;
using AgencyPro.Core.Invoices.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Retainers.Models;
using AgencyPro.Core.Roles.Models;

namespace AgencyPro.Core.CustomerAccounts.Models
{
    public class CustomerAccount : AuditableEntity, ICustomerAccount
    {
        public bool IsDeleted { get; set; }

        private ICollection<CustomerAccountStatusTransition> _statusTransitions;

        public virtual ICollection<CustomerAccountStatusTransition> StatusTransitions
        {
            get => _statusTransitions ?? (_statusTransitions = new Collection<CustomerAccountStatusTransition>());
            set => _statusTransitions = value;
        }


        public OrganizationCustomer OrganizationCustomer { get; set; }

        public ProviderOrganization ProviderOrganization { get; set; }
        public Organization BuyerOrganization { get; set; }
        public OrganizationAccountManager OrganizationAccountManager { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<WorkOrder> WorkOrders { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<ProjectInvoice> Invoices { get; set; } 
        public ICollection<Contract> Contracts { get; set; }

        public int BuyerNumber { get; set; }
        public int Number { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CustomerOrganizationId { get; set; }
        public Customer Customer { get; set; }
        public AccountStatus AccountStatus { get; set; }

        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }
        public AccountManager AccountManager { get; set;  }
        public string ConcurrencyStamp { get; set; }

        public DateTimeOffset? AgencyOwnerDeactivationDate { get; set; }
        public DateTimeOffset? AccountManagerDeactivationDate { get; set; }
        public DateTimeOffset? CustomerDeactivationDate { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

        public int PaymentTermId { get; set; }
        public PaymentTerm PaymentTerm { get; set; }


        public bool IsInternal
        {
            get { return AccountManagerOrganizationId == CustomerOrganizationId; }
            set
            {

            }
        }

        public bool IsCorporate
        {
            get { return (AccountManagerOrganizationId == CustomerOrganizationId) 
                         && (AccountManagerId == CustomerId); }
            set
            {

            }
        }

        public bool IsDeactivated
        {
            get
            {
                return AccountManagerDeactivationDate.HasValue 
                       || AgencyOwnerDeactivationDate.HasValue 
                       || CustomerDeactivationDate.HasValue;
            }
            set { }
        }

        public decimal MarketerStream { get; set; }
        public decimal MarketingAgencyStream { get; set; }

        public string StripeCustomerId { get; set; }
        public bool AutoApproveTimeEntries { get; set; }
        public ICollection<ProjectRetainerIntent> RetainerIntents { get; set; }
    }
}