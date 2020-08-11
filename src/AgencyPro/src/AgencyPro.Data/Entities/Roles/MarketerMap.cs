// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Roles
{
    public class MarketerMap : EntityMap<Marketer>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Marketer> builder)
        {
            builder
                .HasOne(x => x.Person)
                .WithOne(x => x.Marketer)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override void SeedInternal(EntityTypeBuilder<Marketer> entity)
        {
            entity.HasData(new List<Marketer>
            {
                new Marketer
                {
                    Id = EntityConstants.SystemAdminUserId,
                    Created = EntityConstants.DefaultDateTime,
                    ObjectState = ObjectState.Added,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Marketer
                {
                    Id = EntityConstants.MarketerOnlyId,
                    Created = EntityConstants.DefaultDateTime,
                    ObjectState = ObjectState.Added,
                    Updated = EntityConstants.DefaultDateTime
                }
            });
        }
    }
}