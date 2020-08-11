// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Positions.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationPositionMap : EntityMap<OrganizationPosition>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationPosition> builder)
        {
            builder.HasKey(x => new
            {
                x.OrganizationId,
                x.PositionId
            });

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.Positions)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired();

            builder.HasOne(x => x.Position)
                .WithMany(x => x.Organizations)
                .HasForeignKey(x => x.PositionId)
                .IsRequired();
        }

        
    }
}