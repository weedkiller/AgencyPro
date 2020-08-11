// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.CustomerAccounts.Enums;
using AgencyPro.Core.CustomerAccounts.Services;

namespace AgencyPro.Core.CustomerAccounts.ViewModels
{
    public class CustomerAccountOutput : CustomerAccountInput, ICustomerAccount
    {
        public virtual string CustomerImageUrl { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerEmail { get; set; }
        public virtual string CustomerPhoneNumber { get; set; }
        public virtual string CustomerOrganizationName { get; set; }
        public virtual string CustomerOrganizationImageUrl { get; set; }

        public virtual string AccountManagerName { get; set; }
        public virtual string AccountManagerImageUrl { get; set; }
        public virtual string AccountManagerEmail { get; set; }
        public virtual string AccountManagerPhoneNumber { get; set; }
        public virtual string AccountManagerOrganizationName { get; set; }
        public virtual string AccountManagerOrganizationImageUrl { get; set; }

        public virtual int TotalProjects { get; set; }
        public virtual int TotalContracts { get; set; }
        public virtual int TotalInvoices { get; set; }

        public virtual int BuyerNumber { get; set; }

        public virtual int Number { get; set; }

        public virtual AccountStatus AccountStatus { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual DateTimeOffset Updated { get; set; }

       
        public virtual decimal MarketerStream { get; set; }
        public virtual decimal MarketingAgencyStream { get; set; }
        public virtual bool IsDeactivated { get; set; }

        public virtual bool IsCorporate { get; set; }
        public virtual bool IsInternal { get; set; }

        public virtual AccountStatus Status => IsDeactivated ? AccountStatus.Inactive : AccountStatus.Active;

        public string PaymentTermName { get; set; }

        public decimal AmountDue { get; set; }
        public decimal AmountPaid { get; set; }

        public Dictionary<DateTimeOffset, AccountStatus> StatusTransitions { get; set; }

    }
}