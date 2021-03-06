﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Contracts.Enums;
using AgencyPro.Core.Contracts.Services;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.TimeEntries.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Models;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Contracts.Models
{
    public class Contract : AuditableEntity, IContract
    {

        private ICollection<ContractStatusTransition> _statusTransitions;

        public virtual ICollection<ContractStatusTransition> StatusTransitions
        {
            get => _statusTransitions ?? (_statusTransitions = new Collection<ContractStatusTransition>());
            set => _statusTransitions = value;
        }

        public OrganizationContractor OrganizationContractor { get; set; }
        public OrganizationMarketer OrganizationMarketer { get; set; }
        public OrganizationRecruiter OrganizationRecruiter { get; set; }
        public Project Project { get; set; }

        public ProviderOrganization ProviderOrganization { get; set; }
        public MarketingOrganization MarketerOrganization { get; set; }
        public RecruitingOrganization RecruiterOrganization { get; set; }

        private ICollection<Comment> _comments;
        public ICollection<Comment> Comments
        {
            get => _comments ?? (_comments = new Collection<Comment>());
            set => _comments = value;
        }


        private ICollection<ContractNotification> _notifications;

        public virtual ICollection<ContractNotification> Notifications
        {
            get => _notifications ?? (_notifications = new Collection<ContractNotification>());
            set => _notifications = value;
        }

        public ICollection<TimeEntry> TimeEntries { get; set; }

        public Guid Id { get; set; }

        public int ProviderNumber { get; set; }
        public int BuyerNumber { get; set; }
        public int MarketingNumber { get; set; }
        public int RecruitingNumber { get; set; }

        public Guid ContractorId { get; set; }
        public Contractor Contractor { get; set; }
        public Guid ContractorOrganizationId { get; set; }
        public Guid AccountManagerId { get; set; }
        public Guid AccountManagerOrganizationId { get; set; }
        public AccountManager AccountManager { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public Guid ProjectManagerId { get; set; }
        public Guid ProjectManagerOrganizationId { get; set; }
        public OrganizationProjectManager OrganizationProjectManager { get; set; }
        public OrganizationAccountManager OrganizationAccountManager { get; set; }

        public Guid MarketerId { get; set; }
        public Marketer Marketer { get; set; }
        public Guid MarketerOrganizationId { get; set; }

        public Guid RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; }
        public Guid RecruiterOrganizationId { get; set; }
        
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Guid BuyerOrganizationId { get; set; }
        public Organization BuyerOrganization { get; set; }
        public OrganizationCustomer OrganizationCustomer { get; set; }
        
        public CustomerAccount CustomerAccount { get; set; }


        public int MaxWeeklyHours { get; set; }

        public decimal ContractorStream { get; set; }
        public decimal MarketerStream { get; set; }
        public decimal AccountManagerStream { get; set; }
        public decimal ProjectManagerStream { get; set; }
        public decimal RecruiterStream { get; set; }
        public decimal SystemStream { get; set; }
        public decimal AgencyStream { get; set; }
        public decimal RecruitingAgencyStream { get; set; }
        public decimal MarketingAgencyStream { get; set; }

        public decimal CustomerRate
        {
            get => ContractorStream + MarketerStream + AccountManagerStream + ProjectManagerStream +
                   RecruiterStream + SystemStream + AgencyStream + RecruitingAgencyStream + MarketingAgencyStream;
            private set { }
        }

        public decimal MaxCustomerWeekly
        {
            get => CustomerRate * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxContractorWeekly
        {
            get => ContractorStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxRecruiterWeekly
        {
            get => RecruiterStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxMarketerWeekly
        {
            get => MarketerStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxProjectManagerWeekly
        {
            get => ProjectManagerStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxAccountManagerWeekly
        {
            get => AccountManagerStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxAgencyWeekly
        {
            get => AgencyStream * MaxWeeklyHours;
            private set { }
        }

        public decimal MaxSystemWeekly
        {
            get => SystemStream * MaxWeeklyHours;
            private set { }
        }

        public Guid ProjectId { get; set; }
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public bool IsDeleted { get; set; }

        public decimal MaxRecruitingAgencyWeekly
        {
            get => RecruitingAgencyStream * MaxWeeklyHours;

            private set { }
        }

        public decimal MaxMarketingAgencyWeekly
        {
            get => MarketingAgencyStream * MaxWeeklyHours;

            private set { }
        }

        public DateTimeOffset? ContractorPauseDate { get; set; }
        public DateTimeOffset? CustomerPauseDate { get; set; }
        public DateTimeOffset? AgencyOwnerPauseDate { get; set; }
        public DateTimeOffset? AccountManagerPauseDate { get; set; }

        public ContractStatus Status { get; set; }

        public bool IsPaused
        {
            get => (ContractorPauseDate.HasValue) ||
                        (CustomerPauseDate.HasValue) ||
                        (AgencyOwnerPauseDate.HasValue) ||
                        (AccountManagerPauseDate.HasValue);
            private set
            {

            }
        }

        public DateTimeOffset? ContractorEndDate { get; set; }
        public DateTimeOffset? CustomerEndDate { get; set; }
        public DateTimeOffset? AgencyOwnerEndDate { get; set; }
        public DateTimeOffset? AccountManagerEndDate { get; set; }

        public bool IsEnded
        {
            get => (ContractorEndDate.HasValue) ||
                       (CustomerEndDate.HasValue) ||
                       (AgencyOwnerEndDate.HasValue) ||
                       (AccountManagerEndDate.HasValue);
            private set
            {

            }
        }

        public decimal TotalHoursLogged { get; set; }

        public ICollection<StripeInvoiceItem> InvoiceItems { get; set; }
    }
}