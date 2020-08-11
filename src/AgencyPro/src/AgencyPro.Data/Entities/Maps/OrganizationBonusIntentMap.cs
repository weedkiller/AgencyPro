// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationBonusIntentMap : EntityMap<OrganizationBonusIntent>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationBonusIntent> builder)
        {
            // id properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.BonusIntents)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired();
            
            builder.HasOne(x => x.BonusTransfer)
                .WithMany(x => x.OrganizationBonusIntents)
                .HasForeignKey(x => x.TransferId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}