// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Geo.Models;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Common.Models
{
    public class LanguageCountry : BaseObjectState
    {
        public string Iso2 { get; set; }
        public Country Country { get; set; }
        public string LanguageCode { get; set; }
        public Language Language { get; set; }
        public bool Default { get; set; }
    }
}