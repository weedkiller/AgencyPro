// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AutoMapper;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Categories.ViewModels;

namespace AgencyPro.Core.Categories.Extensions
{
    public class CategoryProjectionMap : Profile
    {
        public CategoryProjectionMap()
        {
            CreateMap<Category, CategoryInput>()
                .Include<Category, CategoryOutput>()
                .Include<Category, CategoryDetailsOutput>()
                .Include<Category, AccountManagerCategoryOutput>();

            CreateMap<Category, CategoryOutput>();
            CreateMap<Category, CategoryDetailsOutput>();
            CreateMap<Category, AccountManagerCategoryOutput>();
        }
    }
}