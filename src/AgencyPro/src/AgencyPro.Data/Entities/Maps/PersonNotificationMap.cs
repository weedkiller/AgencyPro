// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Notifications.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PersonNotificationMap : EntityMap<PersonNotification>
    {
        public override void ConfigureInternal(EntityTypeBuilder<PersonNotification> builder)
        {
            builder.HasOne(x=>x.Person)
                .WithMany(x=>x.PersonNotifications)
                .HasForeignKey(x=>x.PersonId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}