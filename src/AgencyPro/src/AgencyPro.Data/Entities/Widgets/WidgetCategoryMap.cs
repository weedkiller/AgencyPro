// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Widgets.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Widgets
{
    public class WidgetCategoryMap : EntityMap<CategoryWidget>
    {
        public override void SeedInternal(EntityTypeBuilder<CategoryWidget> entity)
        {
           
        }

        public override void ConfigureInternal(EntityTypeBuilder<CategoryWidget> builder)
        {
            builder.HasKey(x => new {x.CategoryId, x.WidgetId});

            builder.HasOne(x => x.Category)
                .WithMany(x => x.WidgetCategories)
                .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Widget)
                .WithMany(x => x.WidgetCategories)
                .HasForeignKey(x => x.WidgetId);
        }
    }
}