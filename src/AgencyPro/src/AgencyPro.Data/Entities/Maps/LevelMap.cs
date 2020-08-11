// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Levels;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class LevelMap : EntityMap<Level>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Level> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();


            builder.HasOne(x => x.Position)
                .WithMany(x => x.Levels)
                .HasForeignKey(x => x.PositionId);
        }
    }
}