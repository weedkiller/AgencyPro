// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.PaymentTerms.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationPaymentTermMap : EntityMap<OrganizationPaymentTerm>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationPaymentTerm> builder)
        {
            builder.HasKey(x => new
            {
                x.OrganizationId,
                x.PaymentTermId
            });

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.PaymentTerms)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasOne(x => x.PaymentTerm)
                .WithMany(x => x.OrganizationPaymentTerms)
                .HasForeignKey(x => x.PaymentTermId);

            AddAuditProperties(builder);

        }
    }
}