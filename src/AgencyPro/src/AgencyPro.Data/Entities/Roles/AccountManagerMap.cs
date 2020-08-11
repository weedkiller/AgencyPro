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
    public class AccountManagerMap : EntityMap<AccountManager>
    {
        public override void ConfigureInternal(EntityTypeBuilder<AccountManager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.Person)
                .WithOne(x => x.AccountManager);

            builder.HasMany(x => x.OrganizationAccountManagers)
                .WithOne(x => x.AccountManager)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired();

            AddAuditProperties(builder);
        }

        //public override void SeedInternal(EntityTypeBuilder<AccountManager> entity)
        //{
        //    entity.HasData(new List<AccountManager>
        //    {
        //        new AccountManager
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