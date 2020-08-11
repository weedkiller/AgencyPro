// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Categories.Services;
using AgencyPro.Core.Categories.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AgencyPro.Services.Categories
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<List<T>> GetCategories<T>() where T : CategoryOutput
        {
            return Repository.Queryable()
                .ProjectTo<T>(ProjectionMapping)
                .ToListAsync();
        }

        public Task<T> GetCategory<T>(int categoryId) where T : CategoryOutput
        {
            return Repository.Queryable()
                .Where(x=>x.CategoryId == categoryId)
                .ProjectTo<T>(ProjectionMapping)
                .FirstAsync();
        }
    }
}