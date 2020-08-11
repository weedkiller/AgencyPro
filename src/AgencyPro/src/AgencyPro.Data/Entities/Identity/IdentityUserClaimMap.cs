// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Data.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Identity
{
    public class IdentityUserClaimMap : EntityMap<IdentityUserClaim<Guid>>
    {
        public override void ConfigureInternal(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
        {
            // Primary key
            builder.HasKey(uc => uc.Id);

            // Maps to the AspNetUserClaims table
            builder.ToTable("UserClaim");
        }
    }
    
}