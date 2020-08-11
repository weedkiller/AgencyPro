// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PremiumOrganizationMap : EntityMap<PremiumOrganization>
    {
        public override void ConfigureInternal(EntityTypeBuilder<PremiumOrganization> builder)
        {
            builder
                .HasOne(x => x.Organization)
                .WithOne(x => x.PremiumOrganization)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}