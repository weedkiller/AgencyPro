// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationSubscriptionMap : EntityMap<OrganizationSubscription>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationSubscription> builder)
        {
            builder.HasOne(x => x.Organization)
                .WithOne(x => x.OrganizationSubscription)
                .HasForeignKey<OrganizationSubscription>(x => x.Id)
                .IsRequired(true);

            builder.HasOne(x => x.StripeSubscription)
                .WithOne(x => x.OrganizationSubscription)
                .HasForeignKey<OrganizationSubscription>(x => x.StripeSubscriptionId)
                .IsRequired(true);

            AddAuditProperties(builder);

        }
    }
}