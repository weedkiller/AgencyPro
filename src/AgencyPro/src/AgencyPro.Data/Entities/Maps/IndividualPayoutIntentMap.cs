// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BonusIntents;
using AgencyPro.Core.PayoutIntents.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class IndividualPayoutIntentMap : EntityMap<IndividualPayoutIntent>
    {


        public override void ConfigureInternal(EntityTypeBuilder<IndividualPayoutIntent> builder)
        {
            // id properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Person)
                .WithMany(x => x.PayoutIntents)
                .HasForeignKey(x => x.PersonId)
                .IsRequired();

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.IndividualPayoutIntents)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasOne(x => x.OrganizationPerson)
                .WithMany(x => x.Payouts)
                .HasForeignKey(x => new
                {
                    x.OrganizationId,
                    x.PersonId
                });

            builder.HasOne(x => x.InvoiceItem)
                .WithMany(x => x.IndividualPayoutIntents)
                .HasForeignKey(x => x.InvoiceItemId)
                .IsRequired();

            builder.HasOne(x => x.InvoiceTransfer)
                .WithMany(x => x.IndividualPayoutIntents)
                .HasForeignKey(x => x.InvoiceTransferId)
                .IsRequired(false);

            AddAuditProperties(builder);
        }
    }
}