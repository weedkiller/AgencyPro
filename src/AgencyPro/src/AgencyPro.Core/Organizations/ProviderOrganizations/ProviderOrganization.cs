// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.CustomerAccounts.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.Orders.Model;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations.Services;
using AgencyPro.Core.Projects.Models;
using AgencyPro.Core.Skills.Models;

namespace AgencyPro.Core.Organizations.ProviderOrganizations
{
    public class ProviderOrganization : AuditableEntity, IProviderOrganization
    {
        public Guid Id { get; set; }

        [ForeignKey("Id")] public Organization Organization { get; set; }

        public ICollection<Contract> Contracts { get; set; }

        public ICollection<MarketingAgreement> MarketingAgreements { get; set; }
        public ICollection<RecruitingAgreement> RecruitingAgreements { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Lead> Leads { get; set; }

        public decimal AccountManagerStream { get; set; }
        public decimal ProjectManagerStream { get; set; }
        public decimal AgencyStream { get; set; }

        public decimal ContractorStream { get; set; }

        public string ProviderInformation { get; set; }
        public string ProjectManagerInformation { get; set; }
        public string AccountManagerInformation { get; set; }
        public string ContractorInformation { get; set; }
        public string RecruiterInformation { get; set; }
        public string MarketerInformation { get; set; }

        public bool Discoverable { get; set; }


        public int EstimationBasis { get; set; }

        public int FutureDaysAllowed { get; set; }
        public int PreviousDaysAllowed { get; set; }

        public decimal SystemStream { get; set; }

        public Guid DefaultContractorId { get; set; }
        public Guid DefaultProjectManagerId { get; set; }
        public Guid DefaultAccountManagerId { get; set; }

        public OrganizationContractor DefaultContractor { get; set; }
        public OrganizationProjectManager DefaultProjectManager { get; set; }
        public OrganizationAccountManager DefaultAccountManager { get; set; }


        public ICollection<WorkOrder> WorkOrders { get; set; }
        
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }

        public bool AutoApproveTimeEntries { get; set; }


        //public ICollection<OrganizationProjectManager> ProjectManagers { get; set; }
        //public ICollection<OrganizationAccountManager> AccountManagers { get; set; }
        //public ICollection<OrganizationContractor> Contractors { get; set; }

        public ICollection<OrganizationSkill> Skills { get; set; }

    }
}