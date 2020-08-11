// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.ExceptionLog;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ExceptionLogMap : EntityMap<ExceptionLog>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ExceptionLog> builder)
        {
            builder
                .Property(e => e.Id).ValueGeneratedOnAdd();

            builder
                .Property(s => s.Created)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder
                .Property(p => p.Message)
                .IsRequired()
                .HasMaxLength(800);

            builder
                .Property(p => p.Source)
                .HasMaxLength(400);

            builder
                .Property(p => p.RequestUri)
                .HasMaxLength(200);

            builder
                .Property(p => p.Method)
                .HasMaxLength(20);
        }
    }
}