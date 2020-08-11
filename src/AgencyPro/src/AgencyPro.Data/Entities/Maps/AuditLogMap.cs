// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class AuditLogMap : EntityMap<AuditLog>
    {
        public override void ConfigureInternal(EntityTypeBuilder<AuditLog> builder)
        {
            builder
                .Property(e => e.Id).ValueGeneratedOnAdd();

            builder
                .Property(s => s.DataTime)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder
                .Property(p => p.EntityType)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.EntityId)
                .HasMaxLength(32);

            builder
                .Property(p => p.Event)
                .HasMaxLength(200);

            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.AuditLogs)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false);
        }
    }
}