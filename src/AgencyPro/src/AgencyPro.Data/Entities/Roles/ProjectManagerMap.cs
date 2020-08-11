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
    public class ProjectManagerMap : EntityMap<ProjectManager>
    {
        public override void ConfigureInternal(EntityTypeBuilder<ProjectManager> builder)
        {
            builder
                .HasOne(x => x.Person)
                .WithOne(x => x.ProjectManager)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //public override void SeedInternal(EntityTypeBuilder<ProjectManager> entity)
        //{
        //    entity.HasData(new List<ProjectManager>
        //    {
        //        new ProjectManager
        //        {
        //            Id = EntityConstants.SystemAdminUserId,
        //            Created = EntityConstants.DefaultDateTime,
        //            ObjectState = ObjectState.Added,
        //            Updated = EntityConstants.DefaultDateTime
        //        }
        //    });
        //}
    }
}