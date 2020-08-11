// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AgencyPro.Core.Common;
using AgencyPro.Core.Geo.Services;
using AgencyPro.Core.Geo.ViewModels;
using AgencyPro.Core.Infrastructure.Caching;
using AgencyPro.Core.UserAccount.Services;
using AgencyPro.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AgencyPro.Person.API.Controllers.v2
{
    [Route("geo")]
    public class GeoController : ControllerBase
    {
        private readonly IGeoService _geoService;
        private readonly IMemoryCache _cache;
        private static MemoryCacheEntryOptions _cacheOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoController" /> class.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        /// <param name="geoService">The country service.</param>
        /// <param name="cache">The memory cache.</param>
        public GeoController(IUserInfo userInfo, IGeoService geoService, IMemoryCache cache)
        {
            _geoService = geoService;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove,
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <param name="page">Index of the page.</param>
        /// <param name="size">Size of the page.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countries")]
        [ProducesResponseType(typeof(CountryOutput), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCountries([FromUri] int page = 0, int size = 0)
        {
            PackedList<CountryOutput> result;
            if (page > 0 && size > 0)
            {
                result = await _geoService.GetCountries(page, size);
            }
            else
            {
                if (!_cache.TryGetValue(DataCacheKey.Countries, out result))
                {
                    result = await _geoService.GetCountries(page, size);
                    _cache.Set(DataCacheKey.Countries, result, _cacheOptions);
                }
            }

           // AddHeader("X-TOTAL-RECORDS", result.Total);

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <param name="page">Index of the page.</param>
        /// <param name="size">Size of the page.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("languages")]
        [ProducesResponseType(typeof(LanguageOutput), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetLanguages([FromUri] int page = 0, int size = 0)
        {
            var result = await _geoService.GetLanguages(page, size);
           // AddHeader("X-TOTAL-RECORDS", result.Total);

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all enabled countries.
        /// </summary>
        /// <param name="page">Index of the page.</param>
        /// <param name="size">Size of the page.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("countries/enabled")]
        [ProducesResponseType(typeof(CountryEnabledOutput), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetEnabledCountries([FromUri] int page = 0, int size = 0)
        {
            var result = await _geoService.GetEnabledCountries(page, size);
           // AddHeader("X-TOTAL-RECORDS", result.Total);

            return Ok(result.Data);
        }

        /// <summary>
        /// Gets all countries' states.
        /// </summary>
        /// <param name="page">Index of the page.</param>
        /// <param name="size">Size of the page.</param>
        /// <param name="iso2">The iso2.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("states")]
        [ProducesResponseType(typeof(ProvinceStateOutput), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProvinceStates([FromUri] int page = 0, int size = 0, string iso2 = null)
        {
            var result = await _geoService.GetStates(page, size, iso2);
            //AddHeader("X-TOTAL-RECORDS", result.Total);

            return Ok(result.Data);
        }

        ///// <summary>
        ///// Enables a contry
        ///// </summary>
        ///// <param name="iso2">The iso2.</param>
        ///// <returns></returns>
        //[HttpPatch("country/{iso2}/enable")]
        //[ProducesResponseType(typeof(IActionResult), (int) HttpStatusCode.OK)]
        //[ClaimsAuthorize(ClaimTypes.Role, UserAccessLevel.Admin)]
        //public async Task<IActionResult> PatchEnable(string iso2)
        //{
        //    return await EnableDisableCounty(iso2, true);
        //}

        ///// <summary>
        ///// Disabled a country
        ///// </summary>
        ///// <param name="iso2">The iso2.</param>
        ///// <returns></returns>        
        //[HttpPatch("country/{iso2}/disable")]
        //[ClaimsAuthorize(ClaimTypes.Role, UserAccessLevel.Admin)]
        //public async Task<IActionResult> PatchDisable(string iso2)
        //{
        //    return await EnableDisableCounty(iso2, false);
        //}

        //private async Task<IActionResult> EnableDisableCounty(string iso2, bool enable)
        //{
        //   // Module = ModelType.EnabledCountry;

        //    if (string.IsNullOrEmpty(iso2))
        //    {
        //        return BadRequest();
        //    }

        //    await _geoService.EnableDisableCountry(iso2, UserInfo.UserId, enable);

        //    return Ok(ModelAction.Update, EventStatus.Success);
        //}

        ///// <summary>
        ///// Sets languages spoke by a country
        ///// </summary>
        ///// <param name="iso2">The iso2.</param>
        ///// <param name="languageCode">The language code.</param>
        ///// <param name="main">if set to <c>true</c> [main].</param>
        ///// <returns></returns>        
        //[HttpPatch("country/{iso2}/lang/{languageCode}")]
        //[ClaimsAuthorize(ClaimTypes.Role, UserAccessLevel.Admin)]
        //public async Task<IActionResult> PatchCountyLanguage(string iso2, string languageCode,
        //    [FromUri] bool main = false)
        //{
        //    Module = ModelType.CountryLanguage;

        //    if (string.IsNullOrEmpty(iso2) || string.IsNullOrEmpty(languageCode))
        //    {
        //        return BadRequest(AppConstants.InformationMessages.InvalidRequestParameters);
        //    }

        //    iso2 = iso2.ToUpper();
        //    languageCode = languageCode.ToLower();
        //    await _geoService.SetCountryLanguage(iso2, languageCode, main);

        //    return Ok(ModelAction.Update, EventStatus.Success);
        //}
    }
}