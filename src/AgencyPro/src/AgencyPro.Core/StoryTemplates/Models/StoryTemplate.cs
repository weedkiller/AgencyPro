// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Models;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Stories.Models;

namespace AgencyPro.Core.StoryTemplates.Models
{
    public class StoryTemplate : AuditableEntity, IStoryTemplate
    {
        public Guid ProviderOrganizationId { get; set; }
        public Organization ProviderOrganization { get; set; }

        public Guid Id { get; set; }
        public int? StoryPoints { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public decimal Hours { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Story> Stories { get; set; }

    }
}