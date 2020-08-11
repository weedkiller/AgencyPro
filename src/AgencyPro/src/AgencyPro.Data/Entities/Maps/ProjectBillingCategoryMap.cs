// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{

    public class ProjectBillingCategoryMap : EntityMap<ProjectBillingCategory>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProjectBillingCategory> builder)
        {
            builder.HasKey(x => new
            {
                x.ProjectId,
                x.BillingCategoryId
            });

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectBillingCategories)
                .HasForeignKey(x => x.ProjectId);

            builder.HasOne(x => x.BillingCategory)
                .WithMany(x => x.ProjectBillingCategories)
                .HasForeignKey(x => x.BillingCategoryId);

            builder.Ignore(x => x.ObjectState);
        }
    }
}