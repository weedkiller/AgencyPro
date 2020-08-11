// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Plans.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PlanMap : EntityMap<StripePlan>
    {


        public override void SeedInternal(EntityTypeBuilder<StripePlan> entity)
        {
            entity.HasData(new List<StripePlan>()
            {
                new StripePlan()
                {
                    Name = "Monthly Plan",
                    UniqueId = "marketing",
                    Amount = 0,
                    Id = 1,
                    Interval = "month",
                    ProductId = "marketing",
                    IsActive = true,
                },
                new StripePlan()
                {
                    Name = "Monthly Plan",
                    UniqueId = "staffing",
                    Amount = 50m,
                    Id = 2,
                    Interval = "month",
                    ProductId = "staffing",
                    IsActive = true,
                },
                new StripePlan()
                {
                    Name = "Monthly Plan",
                    UniqueId = "provider",
                    Amount = 50m,
                    Id = 3,
                    Interval = "month",
                    ProductId = "provider",
                    IsActive = true
                }
            });
        }

        public override void ConfigureInternal(EntityTypeBuilder<StripePlan> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            
        }
    }
}