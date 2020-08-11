// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BillingCategories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationBillingCategoryMap : EntityMap<OrganizationBillingCategory>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationBillingCategory> builder)
        {
            builder.HasKey(x => new
            {
                x.OrganizationId,
                x.BillingCategoryId
            });

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.BillingCategories)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasOne(x => x.BillingCategory)
                .WithMany(x => x.OrganizationBillingCategories)
                .HasForeignKey(x => x.BillingCategoryId);

            AddAuditProperties(builder);

        }
    }
}