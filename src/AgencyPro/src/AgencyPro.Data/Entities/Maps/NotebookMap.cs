// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class NotebookMap : EntityMap<Note>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Note> builder)
        {
            builder
                .Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Description).HasMaxLength(400);

            builder.Property(p => p.Meta).HasMaxLength(200);

            builder
                .HasOne(t => t.ApplicationUser)
                .WithMany(x => x.Notes)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}