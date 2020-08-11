// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class BillingCategoryMap : EntityMap<BillingCategory>
    {
        public override void ConfigureInternal(EntityTypeBuilder<BillingCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.ProjectBillingCategories)
                .WithOne(x => x.BillingCategory)
                .HasForeignKey(x => x.BillingCategoryId);

            builder.HasMany(x => x.TimeEntries)
                .WithOne(x => x.BillingCategory)
                .HasForeignKey(x => x.TimeType);

            builder.HasQueryFilter(z => !z.IsDeleted);

        }

        public override void SeedInternal(EntityTypeBuilder<BillingCategory> entity)
        {
            entity.HasData(new List<BillingCategory>()
            {
                new BillingCategory()
                {
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime,
                    Name = "Consulting",
                    Id = 1,
                    IsDeleted = false,
                    IsPrivate = false,
                    IsStoryBucket = true,
                    OrganizationId = null,
                    IsEnabled = true
                },
                new BillingCategory()
                {
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime,
                    Name = "Meetings",
                    Id = 2,
                    IsDeleted = false,
                    IsPrivate = false,
                    IsStoryBucket = false,
                    OrganizationId = null,
                    IsEnabled = true
                },
                new BillingCategory()
                {
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime,
                    Name = "Research",
                    Id = 3,
                    IsDeleted = false,
                    IsPrivate = false,
                    IsStoryBucket = false,
                    OrganizationId = null,
                    IsEnabled = true
                },
                new BillingCategory()
                {
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime,
                    Name = "Training",
                    Id = 4,
                    IsDeleted = false,
                    IsPrivate = false,
                    IsStoryBucket = false,
                    OrganizationId = null,
                    IsEnabled = true
                }
            });
        }
    }
}