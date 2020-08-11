// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Linq;
using AgencyPro.Core.Geo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Identity.API.Controllers
{
    [AllowAnonymous]
    [Route("ProvinceState")]
    public class ProvinceStateController : Controller
    {
        private readonly IGeoService _geoService;

        public ProvinceStateController(IGeoService geoService)
        {
            _geoService = geoService;
        }

        [HttpGet("states")]
        public ActionResult GetProvinces([FromQuery]string iso2 = "US")
        {
            var country = _geoService.Repository.Queryable()
                .Include(x=>x.ProvinceStates)
                .FirstOrDefault(x => x.Iso2 == iso2);
            
            var result = new SelectList(country.ProvinceStates, "Code", "Name");

            return Json(result);

        }
    }
}
