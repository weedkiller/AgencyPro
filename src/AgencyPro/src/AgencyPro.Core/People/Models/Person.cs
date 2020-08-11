// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BonusIntents;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.People.Enums;
using AgencyPro.Core.People.Services;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Core.UserAccount.Models;
using Customer = AgencyPro.Core.Roles.Models.Customer;

namespace AgencyPro.Core.People.Models
{
    public class Person : AuditableEntity, IPerson
    {
        public string ReferralCode { get; set; }
        public string SSNLast4;
        public Customer Customer { get; set; }
        public Contractor Contractor { get; set; }
        public Marketer Marketer { get; set; }
        public ProjectManager ProjectManager { get; set; }
        public AccountManager AccountManager { get; set; }
        public IndividualFinancialAccount IndividualFinancialAccount { get; set; }

        public Recruiter Recruiter { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<PersonNotification> PersonNotifications { get; set; }
        public ICollection<OrganizationPerson> OrganizationPeople { get; set; }
        public ICollection<IndividualPayoutIntent> PayoutIntents { get; set; }
        public ICollection<IndividualBonusIntent> BonusIntents { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
        public string PostalCode { get; set; }
        
        public PersonStatus Status { get; set; }
    
   

        public string DisplayName
        {
            get => FirstName + " " + LastName;
            private set { }
        }

        public bool TosAcceptance { get; set; }
        public string TaxId { get; set; }
        public DateTime? TosShownAndAcceptedDate { get; set; }
        public string TosIpAddress { get; set; }
        public string TosUserAgent { get; set; }
        public bool DetailsSubmitted { get; set; }
        public long? DobDay { get; set; }
        public long? DobMonth { get; set; }
        public long? DobYear { get; set; }
        public string Gender { get; set; }
        public string MaidenName { get; set; }
        
        public Lead Lead { get; set; }
    }
}