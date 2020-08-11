// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.StoryTemplates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class StoryTemplateMap : EntityMap<StoryTemplate>
    {
        public override void ConfigureInternal(EntityTypeBuilder<StoryTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.StoryTemplates)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Stories)
                .WithOne(x => x.StoryTemplate)
                .HasForeignKey(x => x.StoryTemplateId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasQueryFilter(x => !x.IsDeleted);

            AddAuditProperties(builder);

        }
    }
}