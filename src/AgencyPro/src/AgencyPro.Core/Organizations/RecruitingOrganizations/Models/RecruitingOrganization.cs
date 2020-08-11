// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Services;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.Models
{
    public class RecruitingOrganization : AuditableEntity, IRecruitingOrganization
    {
        public Guid Id { get; set; }
        [ForeignKey("Id")] public Organization Organization { get; set; }

        public ICollection<Contract> RecruiterContracts { get; set; }
        public ICollection<RecruitingAgreement> RecruitingAgreements { get; set; }


        public decimal RecruiterStream { get; set; }
        public decimal RecruitingAgencyStream { get; set; }
        public decimal RecruiterBonus { get; set; }
        public decimal RecruitingAgencyBonus { get; set; }

        public bool Discoverable { get; set; }

        public decimal ServiceFeePerLead { get; set; }

  
        public Guid DefaultRecruiterId { get; set; }
        public OrganizationRecruiter DefaultOrganizationRecruiter { get; set; }

        public decimal CombinedRecruitingStream
        {
            get => RecruiterStream + RecruitingAgencyStream;
            set
            {

            }
        }

        public decimal CombinedRecruitingBonus
        {
            get => RecruiterBonus + RecruitingAgencyBonus + ServiceFeePerLead;
            set
            {

            }
        }
        //public OrganizationJoinSettings ProviderEngagementSettings { get; set; }
        //public decimal CombinedRecruitingStream
        //{
        //    get => RecruiterStream + RecruitingAgencyStream;
        //    set
        //    {

        //    }
        //}
    }
}