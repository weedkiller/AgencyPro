// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Organizations.Models;
using Newtonsoft.Json;

namespace AgencyPro.Core.Organizations.ViewModels
{
    /// <summary>
    /// Customer's view of a provider organization
    /// </summary>
    public class CustomerProviderOrganizationOutput : OrganizationOutput
    {
        [JsonIgnore]
        public override OrganizationType OrganizationType { get; set; }

        [JsonIgnore]
        public override DateTimeOffset Updated { get; set; }

        public IDictionary<Guid, int> Skills { get; set; }
    }
}