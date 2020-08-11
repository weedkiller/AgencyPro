// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Common.Models;
using AgencyPro.Core.Models;
using AgencyPro.Core.People.Models;

namespace AgencyPro.Core.Geo.Models
{
    public class Country : BaseObjectState
    {
        public string Iso2 { get; set; }
        public string Iso3 { get; set; }
        public int NumericCode { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string PhoneCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PostalCodeFormat { get; set; }
        public string PostalCodeRegex { get; set; }
        public virtual ICollection<LanguageCountry> Languages { get; set; }
        public virtual ICollection<Person> UserResidences { get; set; }
        public virtual ICollection<ProvinceState> ProvinceStates { get; set; }
        public virtual EnabledCountry EnabledCountry { get; set; }
    }
}