// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Skills.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationSkillMap : EntityMap<OrganizationSkill>
    {
      

        public override void ConfigureInternal(EntityTypeBuilder<OrganizationSkill> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.OrganizationId,
                    x.SkillId
                });

            builder
                .HasOne(x => x.Organization)
                .WithMany(x => x.Skills)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Skill)
                .WithMany(x => x.OrganizationSkill)
                .HasForeignKey(x => x.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            AddAuditProperties(builder);

        }
    }
}