// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgencyPro.Identity.API.Helpers
{
    public class SelectListItemHelper
    {
        public static IEnumerable<SelectListItem> GetProvincesList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "California", Value = "B"},
                new SelectListItem{Text = "Alaska", Value = "B"},
                new SelectListItem{Text = "Illinois", Value = "B"},
                new SelectListItem{Text = "Texas", Value = "B"},
                new SelectListItem{Text = "Washington", Value = "B"}
            };
            return items;
        }

        public static IEnumerable<SelectListItem> GetCountryList()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "United States", Value = "B"},
                new SelectListItem{Text = "Canada", Value = "B"},
                new SelectListItem{Text = "United Kingdom", Value = "B"},
                new SelectListItem{Text = "Texas", Value = "B"},
                new SelectListItem{Text = "Washington", Value = "B"}

            };
            return items;
        }
    }
}
