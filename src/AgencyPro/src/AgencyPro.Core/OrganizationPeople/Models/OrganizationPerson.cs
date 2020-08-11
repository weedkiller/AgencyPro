// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Comments.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationPeople.Services;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Core.People.Enums;
using AgencyPro.Core.People.Models;
using AgencyPro.Core.Widgets.Models;

namespace AgencyPro.Core.OrganizationPeople.Models
{
    public class OrganizationPerson : AuditableEntity, IOrganizationPerson
    {
        public Organization Organization { get; set; }
        public Person Person { get; set; }

        public ICollection<OrganizationPersonWidget> OrganizationPersonWidgets { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<IndividualPayoutIntent> Payouts { get; set; } 
        public ICollection<IndividualBonusIntent> BonusIntents { get; set; }

        public OrganizationAccountManager AccountManager { get; set; }
        public OrganizationProjectManager ProjectManager { get; set; }
        public OrganizationCustomer Customer { get; set; }
        public OrganizationContractor Contractor { get; set; }
        public OrganizationRecruiter Recruiter { get; set; }
        public OrganizationMarketer Marketer { get; set; }

        public bool IsDeleted { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid PersonId { get; set; }

        public PersonStatus Status { get; set; }

        public bool IsHidden { get; set; }

        public long PersonFlags { get; set; }
        public long AgencyFlags { get; set; }

        public bool IsOrganizationOwner { get; set; }
        public bool IsDefault { get; set; }
        public string ConcurrencyStamp { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }

        public string AffiliateCode { get; set; }

    }
}