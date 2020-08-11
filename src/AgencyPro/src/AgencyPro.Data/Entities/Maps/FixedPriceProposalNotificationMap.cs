// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class FixedPriceProposalNotificationMap : EntityMap<ProposalNotification>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProposalNotification> builder)
        {
            builder.HasOne(x => x.Proposal)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.ProposalId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}