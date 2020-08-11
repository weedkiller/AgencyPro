// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Skills.ViewModels;
using Newtonsoft.Json;

namespace AgencyPro.Core.Roles.ViewModels.Contractors
{
    public class ContractorDetailsOutput : ContractorOutput
    {
        [JsonIgnore]
        public override decimal RecruiterStream { get; set; }

        public virtual ICollection<ContractorSkillOutput> Skills { get; set; }

    }
}