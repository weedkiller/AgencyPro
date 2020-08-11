// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.OrganizationPeople.Models;
using AgencyPro.Core.People.Enums;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class OrganizationPersonMap : EntityMap<OrganizationPerson>
    {
        public override void ConfigureInternal(EntityTypeBuilder<OrganizationPerson> builder)
        {
            builder
                .HasKey(x => new {x.OrganizationId, x.PersonId});

            builder.HasQueryFilter(x => x.IsDeleted == false);
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasMany(x => x.Payouts)
                .WithOne(x => x.OrganizationPerson)
                .HasForeignKey(x => new
                {
                    x.OrganizationId,
                    x.PersonId
                }).IsRequired();

            builder.HasMany(x => x.OrganizationPersonWidgets)
                .WithOne(x => x.OrganizationPerson)
                .HasForeignKey(x => new
                {
                    x.OrganizationId,
                    x.PersonId
                });

            builder
                .HasOne(x => x.Person)
                .WithMany(x => x.OrganizationPeople)
                .HasForeignKey(x => x.PersonId)
                .IsRequired();

            builder
                .HasOne(x => x.Organization)
                .WithMany(x => x.OrganizationPeople)
                .HasForeignKey(x => x.OrganizationId)
                .IsRequired();

            AddAuditProperties(builder);

        }

        //public override void SeedInternal(EntityTypeBuilder<OrganizationPerson> entity)
        //{
        //    entity.HasData(new List<OrganizationPerson>
        //    {
        //        new OrganizationPerson
        //        {
        //            OrganizationId = EntityConstants.AgencyProId,
        //            ConcurrencyStamp = EntityConstants.SystemAdminUserId.ToString(),
        //            PersonId = EntityConstants.SystemAdminUserId,
        //            Status = PersonStatus.Active,
        //            Created = EntityConstants.DefaultDateTime,
        //            Updated = EntityConstants.DefaultDateTime,
        //            IsDefault = true,
        //            IsOrganizationOwner = true
        //        },
        //        new OrganizationPerson
        //        {
        //            OrganizationId = EntityConstants.AgencyProId,
        //            ConcurrencyStamp = EntityConstants.MarketerOnlyId.ToString(),
        //            PersonId = EntityConstants.MarketerOnlyId,
        //            Status = PersonStatus.Active,
        //            Created = EntityConstants.DefaultDateTime,
        //            Updated = EntityConstants.DefaultDateTime,
        //            IsOrganizationOwner = false
        //        },
        //        new OrganizationPerson
        //        {
        //            OrganizationId = EntityConstants.WCPROId,
        //            ConcurrencyStamp = EntityConstants.MarketerOnlyId.ToString(),
        //            PersonId = EntityConstants.SystemAdminUserId,
        //            Status = PersonStatus.Active,
        //            Created = EntityConstants.DefaultDateTime,
        //            Updated = EntityConstants.DefaultDateTime,
        //            IsOrganizationOwner = true
        //        }
        //    });
        //}
    }
}