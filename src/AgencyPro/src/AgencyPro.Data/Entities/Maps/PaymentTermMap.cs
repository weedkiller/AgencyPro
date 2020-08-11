// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PaymentTermMap : EntityMap<PaymentTerm>
    {
        public override void ConfigureInternal(EntityTypeBuilder<PaymentTerm> builder)
        {
            builder.HasKey(x => x.PaymentTermId);

            builder.Property(x => x.PaymentTermId)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.CustomerAccounts)
                .WithOne(x => x.PaymentTerm)
                .HasForeignKey(x => x.PaymentTermId);

            builder.Property(x => x.Name).HasMaxLength(50);

            AddAuditProperties(builder);
        }

        public override void SeedInternal(EntityTypeBuilder<PaymentTerm> entity)
        {
            entity.HasData(new List<PaymentTerm>()
            {
                new PaymentTerm()
                {
                    PaymentTermId = 1,
                    NetValue = 0,
                    Name = "ImmediatePayment"
                },
                new PaymentTerm()
                {
                    PaymentTermId = 2,
                    NetValue = 7,
                    Name = "Net 7",
                },
                new PaymentTerm()
                {
                    PaymentTermId = 3,
                    NetValue = 10,
                    Name = "Net 10"
                },
                new PaymentTerm()
                {
                    PaymentTermId = 4,
                    NetValue = 30,
                    Name = "Net 30"
                },
                new PaymentTerm()
                {
                    PaymentTermId = 5,
                    NetValue = 60,
                    Name = "Net 60"
                },
                new PaymentTerm()
                {
                    PaymentTermId = 6,
                    NetValue = 90,
                    Name = "Net 90"
                },
                new PaymentTerm()
                {
                    PaymentTermId = 7,
                    NetValue = 15,
                    Name = "Net 15"
                },
            });
        }
    }
}