// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Roles.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Roles
{
    public class ContractorMap : EntityMap<Contractor>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Contractor> builder)
        {
            builder
                .HasMany(x => x.OrganizationContractors)
                .WithOne(x => x.Contractor)
                .HasForeignKey(x => x.ContractorId);

            builder.HasOne(x => x.Recruiter)
                .WithMany(x => x.Contractors)
                .HasForeignKey(x => x.RecruiterId);

            builder.Property(x => x.HoursAvailable)
                .HasDefaultValue(40);

            AddAuditProperties(builder);
        }

        //public override void SeedInternal(EntityTypeBuilder<Contractor> entity)
        //{
        //    entity.HasData(new List<Contractor>
        //    {
        //        new Contractor
        //        {
        //            Id = EntityConstants.SystemAdminUserId,
        //            LastWorkedUtc = null,
        //            IsAvailable = true,
        //            Created = EntityConstants.DefaultDateTime,
        //            Updated = EntityConstants.DefaultDateTime,
        //            RecruiterId = EntityConstants.SystemAdminUserId,
        //            RecruiterOrganizationId = EntityConstants.AgencyProId
        //        }
        //    });
        //}
    }
}