// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.Skills.ViewModels
{
    public class SkillInput
    {
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual string IconUrl { get; set; }
        public virtual int Priority { get; set; }
    }
}