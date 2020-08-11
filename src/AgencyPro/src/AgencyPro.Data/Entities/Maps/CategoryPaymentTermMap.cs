// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CategoryPaymentTermMap : EntityMap<CategoryPaymentTerm>
    {
        public override void ConfigureInternal(EntityTypeBuilder<CategoryPaymentTerm> builder)
        {
            builder.HasKey(x => new
            {
                x.CategoryId,
                x.PaymentTermId
            });

            builder.HasOne(x => x.PaymentTerm)
                .WithMany(x => x.CategoryPaymentTerms)
                .HasForeignKey(x => x.PaymentTermId);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.AvailablePaymentTerms)
                .HasForeignKey(x => x.CategoryId);
        }

        public override void SeedInternal(EntityTypeBuilder<CategoryPaymentTerm> entity)
        {
            entity.HasData(new List<CategoryPaymentTerm>()
            {
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 1
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 2
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 3
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 4
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 5
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 6
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 2,
                    PaymentTermId = 7
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 1
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 2
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 3
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 4
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 5
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 6
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 3,
                    PaymentTermId = 7
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 1
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 2
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 3
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 4
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 5
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 6
                },
                new CategoryPaymentTerm()
                {
                    CategoryId = 4,
                    PaymentTermId = 7
                }
            });
        }
    }
}