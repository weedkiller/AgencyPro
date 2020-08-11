// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Positions;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CategoryPositionMap : EntityMap<CategoryPosition>
    {
        public override void ConfigureInternal(EntityTypeBuilder<CategoryPosition> builder)
        {
            builder.HasKey(x => new
            {
                x.CategoryId,
                x.PositionId
            });

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            builder.HasOne(x => x.Position)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.PositionId)
                .IsRequired();
        }
    }
}