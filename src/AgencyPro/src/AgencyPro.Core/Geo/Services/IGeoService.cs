// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Threading.Tasks;
using AgencyPro.Core.Common;
using AgencyPro.Core.Geo.Models;
using AgencyPro.Core.Geo.ViewModels;
using AgencyPro.Core.Services;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.Geo.Services
{
    public interface IGeoService : IService<Country>
    {
        Task<PackedList<CountryOutput>> GetCountries(int page, int size);
        Task<PackedList<LanguageOutput>> GetLanguages(int page, int size);
        Task<PackedList<ProvinceStateOutput>> GetStates(int page, int size, string iso2);
        Task EnableDisableCountry(string iso2, int userId, bool enable);
        Task SetCountryLanguage(string iso2, string languageCode, bool main);
        Task<PackedList<CountryEnabledOutput>> GetEnabledCountries(int page, int size);
    }
}