// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AgencyPro.Core.Agreements.Models;
using AgencyPro.Core.Contracts.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.Services;
using AgencyPro.Core.Organizations.Models;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.Models
{
    public class MarketingOrganization : AuditableEntity, IMarketingOrganization
    {
        public Guid Id { get; set; }
        [ForeignKey("Id")] public Organization Organization { get; set; }

        public decimal MarketerStream { get; set; }
        public decimal MarketingAgencyStream { get; set; }
        public decimal MarketerBonus { get; set; }
        public decimal MarketingAgencyBonus { get; set; }

        public bool Discoverable { get; set; }

        public decimal ServiceFeePerLead { get; set; }
        
        public ICollection<Contract> MarketerContracts { get; set; }
        public OrganizationMarketer DefaultOrganizationMarketer { get; set; }
        public Guid DefaultMarketerId { get; set; }

        public ICollection<MarketingAgreement> MarketingAgreements { get; set; }

        public decimal CombinedMarketingStream
        {
            get => MarketerStream + MarketingAgencyStream;
            set
            {

            }
        }

        public decimal CombinedMarketingBonus
        {
            get => MarketerBonus + MarketingAgencyBonus + ServiceFeePerLead;
            set
            {

            }
        }
        

    }
}