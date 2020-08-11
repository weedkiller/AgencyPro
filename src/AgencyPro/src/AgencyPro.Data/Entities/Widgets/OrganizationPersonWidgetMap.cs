// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Widgets.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Widgets
{
    public class OrganizationPersonWidgetMap : EntityMap<OrganizationPersonWidget>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationPersonWidget> builder)
        {
            builder.HasKey(x => new {x.OrganizationId, x.PersonId, x.CategoryId, x.WidgetId});

            builder.HasOne(x => x.OrganizationPerson)
                .WithMany(x => x.OrganizationPersonWidgets)
                .HasForeignKey(x => new {x.OrganizationId, x.PersonId});

            builder.HasOne(x => x.CategoryWidget)
                .WithMany(x => x.OrganizationPersonWidgets)
                .HasForeignKey(x => new
                {
                    x.CategoryId,
                    x.WidgetId
                });
        }
    }
}