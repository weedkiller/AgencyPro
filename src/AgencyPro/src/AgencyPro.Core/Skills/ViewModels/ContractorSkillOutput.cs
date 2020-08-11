// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Skills.ViewModels
{
    public class ContractorSkillOutput
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public virtual int Priority { get; set; }
    }
}