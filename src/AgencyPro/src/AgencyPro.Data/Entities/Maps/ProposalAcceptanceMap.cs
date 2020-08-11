// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Proposals.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProposalAcceptanceMap : EntityMap<ProposalAcceptance>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProposalAcceptance> builder)
        {
            builder.HasKey(x => x.Id);
           
            builder
                .HasOne(x => x.Proposal)
                .WithOne(x => x.ProposalAcceptance);

           

            builder.Property(x => x.AgreementText);
            builder.Property(x => x.CustomerRate).HasColumnType("Money");
            builder.Property(x => x.TotalDays);
            builder.Property(x => x.Velocity);
            builder.Property(x => x.ProposalType);


            builder.HasOne(x => x.Customer)
                .WithMany(x => x.ProposalsAccepted)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationCustomer)
                .WithMany(x => x.ProposalsAccepted)
                .HasForeignKey(x => new
                {
                    x.CustomerOrganizationId,
                    x.CustomerId
                })
                .IsRequired();

            AddAuditProperties(builder);
        }
    }
}