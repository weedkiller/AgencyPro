// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CandidateNotificationMap : EntityMap<CandidateNotification>
    {
        public override void ConfigureInternal(EntityTypeBuilder<CandidateNotification> builder)
        {
            builder.HasOne(x=>x.Candidate)
                .WithMany(x=>x.CandidateNotifications)
                .HasForeignKey(x=>x.CandidateId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}