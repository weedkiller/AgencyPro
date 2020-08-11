// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.ProviderOrganizations.ViewModels;
using AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Organizations.ViewModels
{
    [BindProperties]
    public class OrganizationUpgradeInput : ValidatableModel
    {
        [BindProperty]
        public RecruitingOrganizationUpgradeInput RecruitingOrganizationInput { get; set; }

        [BindProperty]
        public MarketingOrganizationUpgradeInput MarketingOrganizationInput { get; set; }

        [BindProperty]
        public ProviderOrganizationUpgradeInput ProviderOrganizationInput { get; set; }

        [BindProperty]
        public virtual int CategoryId { get; set; }
        
        [BindNever]
        public OrganizationType OrganizationType
        {
            get
            {
                var type = OrganizationType.Buyer;

                if (RecruitingOrganizationInput != null) type.Add(OrganizationType.Recruiting);
                if (MarketingOrganizationInput != null) type.Add(OrganizationType.Marketing);
                if (ProviderOrganizationInput != null) type.Add(OrganizationType.Provider);

                return type;
            }
        }

        public bool IsSubscriptionRequired => (int) OrganizationType > (int)OrganizationType.Marketing;

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((int) OrganizationType <= 1) yield break;
        }
    }
}