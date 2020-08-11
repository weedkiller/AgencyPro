// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels
{
    public class MarketingOrganizationUpgradeInput
    {
        [Range(0, 100)]
        public virtual decimal MarketingAgencyStream { get; set; }
        [Range(0, 100)]
        public virtual decimal MarketerBonus { get; set; }
        [Range(0, 100)]
        public virtual decimal MarketingAgencyBonus { get; set; }
        [Range(0, 100)]
        public virtual decimal MarketerStream { get; set; }
        
        public bool Discoverable { get; set; }


    }
}