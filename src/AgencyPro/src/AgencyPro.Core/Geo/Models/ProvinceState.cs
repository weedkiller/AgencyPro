// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Geo.Models
{
    public class ProvinceState : BaseObjectState
    {
        public string Iso2 { get; set; }
        public Country Country { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}