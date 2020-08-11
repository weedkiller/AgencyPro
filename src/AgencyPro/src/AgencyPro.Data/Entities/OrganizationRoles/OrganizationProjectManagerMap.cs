// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationRoles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.OrganizationRoles
{
    public class OrganizationProjectManagerMap : EntityMap<OrganizationProjectManager>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationProjectManager> builder)
        {
            builder.HasKey(x => new {x.OrganizationId, x.ProjectManagerId});

            builder.Property(x => x.ProjectManagerStream).HasColumnType("Money");
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Organization)
                .WithMany(x => x.ProjectManagers)
                .HasForeignKey(x => x.OrganizationId);

            builder.HasMany(x => x.Projects)
                .WithOne(x => x.OrganizationProjectManager)
                .HasForeignKey(x => new {x.ProjectManagerOrganizationId, x.ProjectManagerId});

            builder.HasOne(x => x.OrganizationPerson)
                .WithOne(x => x.ProjectManager)
                .HasForeignKey<OrganizationProjectManager>(x => new {x.OrganizationId, x.ProjectManagerId})
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);
        }
    }
}