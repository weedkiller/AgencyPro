// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Categories.ViewModels;
using AgencyPro.Core.Services;

namespace AgencyPro.Core.Categories.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<List<T>> GetCategories<T>() where T : CategoryOutput;
        Task<T> GetCategory<T>(int categoryId) where T : CategoryOutput;
    }
}