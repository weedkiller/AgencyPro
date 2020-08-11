// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Roles
{
    public class RecruiterMap : EntityMap<Recruiter>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Recruiter> builder)
        {
            builder
                .HasOne(x => x.Person)
                .WithOne(x => x.Recruiter);
        }

        public override void SeedInternal(EntityTypeBuilder<Recruiter> entity)
        {
            entity.HasData(new List<Recruiter>
            {
                new Recruiter
                {
                    Id = EntityConstants.SystemAdminUserId,
                    Created = EntityConstants.DefaultDateTime,
                    ObjectState = ObjectState.Added,
                    Updated = EntityConstants.DefaultDateTime
                }
            });
        }
    }
}