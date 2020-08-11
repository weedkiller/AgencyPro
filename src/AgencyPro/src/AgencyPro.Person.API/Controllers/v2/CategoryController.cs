// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Categories.Services;
using AgencyPro.Core.Categories.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgencyPro.Person.API.Controllers.v2
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("categories")]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<CategoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories<CategoryOutput>();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(List<CategoryOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategory([FromRoute]int categoryId)
        {
            var categories = await _categoryService.GetCategory<CategoryOutput>(categoryId);
            return Ok(categories);
        }
    }
}