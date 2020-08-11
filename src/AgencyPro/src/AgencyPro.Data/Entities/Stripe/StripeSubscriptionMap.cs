// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeSubscriptionMap : EntityMap<StripeSubscription>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeSubscription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasOne(x => x.OrganizationSubscription)
                .WithOne(x => x.StripeSubscription)
                .HasForeignKey<OrganizationSubscription>(x => x.StripeSubscriptionId);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Subscription)
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);
        }
    }
}