// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Widgets.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Widgets
{
    public class WidgetMap : EntityMap<Widget>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Widget> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.WidgetCategories)
                .WithOne(x => x.Widget);
        }
    }
}