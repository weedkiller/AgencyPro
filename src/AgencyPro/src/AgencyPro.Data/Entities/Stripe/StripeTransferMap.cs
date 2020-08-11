// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Transfers.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Stripe
{
    public class StripeTransferMap : EntityMap<StripeTransfer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StripeTransfer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.DestinationAccount)
                .WithMany(x => x.Transfers)
                .HasForeignKey(x => x.DestinationId);


            AddAuditProperties(builder);
        }
    }
}