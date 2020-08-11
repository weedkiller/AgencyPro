// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
   

    public class CandidateMap : EntityMap<Candidate>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Candidate> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.IndividualBonusIntent)
                .WithOne(x => x.Candidate)
                .HasForeignKey<IndividualBonusIntent>(x => x.CandidateId)
                .IsRequired(false);

            builder.HasOne(x => x.OrganizationBonusIntent)
                .WithOne(x => x.Candidate)
                .HasForeignKey<OrganizationBonusIntent>(x => x.CandidateId)
                .IsRequired(false);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.EmailAddress).IsRequired().HasMaxLength(254);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.RecruiterStream).HasColumnType("Money");
            builder.Property(x => x.RecruiterBonus).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyBonus).HasColumnType("Money");
            builder.Property(x => x.RecruitingAgencyStream).HasColumnType("Money");
            builder.Property(x => x.RejectionDescription).HasMaxLength(1000);

            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasMaxLength(2);

            builder.Property(p => p.ProvinceState)
                .IsRequired()
                .HasColumnType("varchar(3)")
                .HasMaxLength(3);

            builder.HasOne(x => x.Recruiter)
                .WithMany(x => x.Candidates)
                .HasForeignKey(x => x.RecruiterId)
                .IsRequired();

            builder.HasOne(x => x.ProjectManager)
                .WithMany(x => x.Candidates)
                .HasForeignKey(x => x.ProjectManagerId)
                .IsRequired(false);

            builder.OwnsMany(x=>x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.CandidateId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x=>x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.OrganizationProjectManager)
                .WithMany(x => x.Candidates)
                .HasForeignKey(x => new
                {
                    x.ProjectManagerOrganizationId,
                    x.ProjectManagerId
                }).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.Candidates)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .IsRequired();

            AddAuditProperties(builder, true);
        }
    }
}