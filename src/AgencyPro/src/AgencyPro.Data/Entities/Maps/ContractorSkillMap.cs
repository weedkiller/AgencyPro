// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Skills.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class ContractorSkillMap : EntityMap<ContractorSkill>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ContractorSkill> builder)
        {
            builder.HasKey(sc => new {sc.SkillId, sc.ContractorId});

            builder.HasOne(x => x.Skill)
                .WithMany(x => x.ContractorSkills)
                .HasForeignKey(x => x.SkillId);

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.ContractorSkills)
                .HasForeignKey(x => x.ContractorId);

        }
    }
}