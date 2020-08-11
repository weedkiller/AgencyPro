// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Stories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class StoryMap : EntityMap<Story>
    {
     
        public override void ConfigureInternal(EntityTypeBuilder<Story> builder)
        {
            builder
                .HasOne(x => x.OrganizationContractor)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => new
                {
                    x.ContractorOrganizationId,
                    x.ContractorId
                }).OnDelete(DeleteBehavior.SetNull);

            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.StoryId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.Stories)
                .HasForeignKey(x => x.ContractorId)
                .IsRequired(false);

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            AddAuditProperties(builder);

        }
    }
}