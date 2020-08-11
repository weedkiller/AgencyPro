// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class IndividualBonusIntentMap : EntityMap<IndividualBonusIntent>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IndividualBonusIntent> builder)
        {
            // id properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.BonusIntents)
                .HasForeignKey(x => x.PersonId)
                .IsRequired();

            builder.HasOne(x=>x.OrganizationPerson)
                .WithMany(x=>x.BonusIntents)
                .HasForeignKey(x => new
                {
                    x.OrganizationId,
                    x.PersonId
                });

            builder.HasOne(x => x.BonusTransfer)
                .WithMany(x => x.IndividualBonusIntents)
                .HasForeignKey(x => x.TransferId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}