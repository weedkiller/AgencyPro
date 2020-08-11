// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeSubscriptionItemMap : EntityMap<StripeSubscriptionItem>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeSubscriptionItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasOne(x => x.Subscription)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SubscriptionId)
                .IsRequired();
            
            builder.HasQueryFilter(x => x.IsDeleted == false);


            AddAuditProperties(builder);
        }
    }
}