// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Orders.Model;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ProposalWorkOrderMap : EntityMap<ProposalWorkOrder>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProposalWorkOrder> builder)
        {
            builder.HasKey(x => new
            {
                x.WorkOrderId,
                x.ProposalId
            });

            builder.HasOne(x => x.Proposal)
                .WithMany(x => x.WorkOrders)
                .HasForeignKey(x => x.ProposalId);

            builder.HasOne(x => x.WorkOrder)
                .WithMany(x => x.Proposals)
                .HasForeignKey(x => x.WorkOrderId);
        }
    }
}