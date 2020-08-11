// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Core.Categories.Services;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Core.Positions;
using AgencyPro.Core.Widgets.Models;

namespace AgencyPro.Core.Categories.Models
{
    public class Category : BaseObjectState, ICategory
    {
        public ICollection<CategoryWidget> WidgetCategories { get; set; }
        public ICollection<CategorySkill> AvailableSkills { get; set; }
        public ICollection<Organization> Organizations { get; set; }
        public ICollection<CategoryPaymentTerm> AvailablePaymentTerms { get; set; }
        public ICollection<CategoryBillingCategory> AvailableBillingCategories { get; set; }
        public ICollection<CategoryPosition> Positions { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ContractorTitle { get; set; }
        public string ContractorTitlePlural { get; set; }
        public string AccountManagerTitle { get; set; }
        public string AccountManagerTitlePlural { get; set; }
        public string ProjectManagerTitle { get; set; }
        public string ProjectManagerTitlePlural { get; set; }
        public string RecruiterTitle { get; set; }
        public string MarketerTitle { get; set; }
        public string StoryTitle { get; set; }
        public string StoryTitlePlural { get; set; }
        public string RecruiterTitlePlural { get; set; }
        public string MarketerTitlePlural { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerTitlePlural { get; set; }
        public bool Searchable { get; set; }

        public decimal DefaultRecruiterStream { get; set; }
        public decimal DefaultMarketerStream { get; set; }
        public decimal DefaultProjectManagerStream { get; set; }
        public decimal DefaultAccountManagerStream { get; set; }
        public decimal DefaultContractorStream { get; set; }
        public decimal DefaultAgencyStream { get; set; }

        public decimal DefaultMarketingAgencyStream { get; set; }
        public decimal DefaultRecruitingAgencyStream { get; set; }
        public decimal DefaultMarketerBonus { get; set; }
        public decimal DefaultMarketingAgencyBonus { get; set; }

    }
}