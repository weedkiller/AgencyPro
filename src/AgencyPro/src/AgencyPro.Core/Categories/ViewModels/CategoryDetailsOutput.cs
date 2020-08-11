// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Organizations.ViewModels;

namespace AgencyPro.Core.Categories.ViewModels
{
    public class CategoryDetailsOutput : CategoryOutput
    {
        public List<OrganizationOutput> Organizations { get; set; }
    }
}