// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CategoryBillingCategoryMap : EntityMap<CategoryBillingCategory>
    {
        public override void SeedInternal(EntityTypeBuilder<CategoryBillingCategory> entity)
        {
            entity.HasData(new List<CategoryBillingCategory>()
            {
                new CategoryBillingCategory()
                {
                    CategoryId = 1,
                    BillingCategoryId = 1,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 1,
                    BillingCategoryId = 2,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 2,
                    BillingCategoryId = 1,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 2,
                    BillingCategoryId = 2,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 3,
                    BillingCategoryId = 1,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 3,
                    BillingCategoryId = 2,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 4,
                    BillingCategoryId = 1,
                },
                new CategoryBillingCategory()
                {
                    CategoryId = 4,
                    BillingCategoryId = 2,
                },
               
            });
        }

        public override void ConfigureInternal(EntityTypeBuilder<CategoryBillingCategory> builder)
        {
            builder.HasKey(x => new
            {
                x.CategoryId,
                x.BillingCategoryId
            });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.AvailableBillingCategories)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.BillingCategory)
                .WithMany(x => x.CategoryBillingCategories)
                .HasForeignKey(x => x.BillingCategoryId);

            builder.Ignore(x => x.ObjectState);
        }
    }
}