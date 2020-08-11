// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Charges.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeChargeMap : EntityMap<StripeCharge>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeCharge> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Destination)
                .WithMany(x => x.DestinationCharges)
                .HasForeignKey(x=>x.DestinationId);

            builder.HasOne(x => x.RetainerIntent)
                .WithMany(x => x.Credits)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired(false);


            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Charges)
                .HasForeignKey(x => x.CustomerId);

            AddAuditProperties(builder);
        }
    }
}