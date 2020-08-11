// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Models;

namespace AgencyPro.Core.Geo.Models
{

    public class EnabledCountry : AuditableEntity
    {
        public string Iso2 { get; set; }
        public virtual Country Country { get; set; }
        public bool Enabled { get; set; }

        public new DateTimeOffset? Updated { get; set; }
    }
}