// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Common.Models
{
    public class Language : BaseObjectState
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<LanguageCountry> Countries { get; set; }
    }
}