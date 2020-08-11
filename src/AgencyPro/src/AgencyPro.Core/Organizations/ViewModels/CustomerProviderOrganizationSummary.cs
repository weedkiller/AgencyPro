// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Categories.ViewModels;
using AgencyPro.Core.Skills.ViewModels;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class CustomerProviderOrganizationSummary
    {
        public ICollection<CustomerProviderOrganizationOutput> Organizations { get; set; }

        public ICollection<SkillOutput> AvailableSkills { get; set; }
        public ICollection<CategoryOutput> AvailableCategories { get; set; }
    }
}