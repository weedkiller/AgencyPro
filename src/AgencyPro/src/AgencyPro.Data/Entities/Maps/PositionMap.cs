// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Positions;
using AgencyPro.Core.Positions.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PositionMap : EntityMap<Position>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Levels)
                .WithOne(x => x.Position)
                .HasForeignKey(x => x.PositionId);

        }

        public override void SeedInternal(EntityTypeBuilder<Position> entity)
        {
            entity.HasData(new List<Position>()
            {
                new Position()
                {
                    Name = "Software Engineer",
                    Id = 1,
                    
                }
            });
        }
    }
}