// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Products.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProductMap : EntityMap<Product>
    {
        public override void SeedInternal(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Lead Generation Package",
                    UniqueId = "marketing",
                    Description = "Marketing Features for Flow",
                    Shippable = false,
                    StatementDescriptor = "Marketer Module",
                    Type = "service",
                    IsActive = true
                },
                new Product()
                {
                    Id = 2,
                    Name = "Staffing Package",
                    UniqueId = "staffing",
                    Description = "Staffing Features for Flow",
                    Shippable = false,
                    StatementDescriptor = "Recruiter Module",
                    Type = "service",
                    IsActive = true
                },
                new Product()
                {
                    Id = 3,
                    Name = "Service Provider Package",
                    UniqueId = "provider",
                    Description = "Service Provider features for Flow",
                    Shippable = false,
                    StatementDescriptor = "Provider Module",
                    Type = "service",
                    IsActive = true
                }

            });
        }

        public override void ConfigureInternal(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}