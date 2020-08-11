﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.BonusIntents.Models;
using AgencyPro.Core.Candidates.Models;
using AgencyPro.Core.Leads.Enums;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class LeadMap : EntityMap<Lead>
    {
        public override void ConfigureInternal(EntityTypeBuilder<Lead> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(x => x.OrganizationName).HasMaxLength(50);
            builder.Property(x => x.MeetingNotes).IsRequired(false).HasMaxLength(5000);
            builder.Property(x => x.MarketerStream).IsRequired().HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyStream).IsRequired().HasColumnType("Money");
            builder.Property(x => x.MarketingAgencyBonus).IsRequired().HasColumnType("Money");
            builder.Property(x => x.MarketerBonus).IsRequired().HasColumnType("Money");
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);

            builder.HasOne(x => x.IndividualBonusIntent)
                .WithOne(x => x.Lead)
                .HasForeignKey<IndividualBonusIntent>(x => x.LeadId)
                .IsRequired(false);

            builder.HasOne(x => x.OrganizationBonusIntent)
                .WithOne(x => x.Lead)
                .HasForeignKey<OrganizationBonusIntent>(x => x.LeadId)
                .IsRequired(false);


            builder.OwnsMany(x => x.StatusTransitions, a =>
            {
                a.HasForeignKey(x => x.LeadId);
                a.HasKey(x => x.Id);
                a.Property(x => x.Id).ValueGeneratedOnAdd();
                a.Ignore(x => x.ObjectState);
                a.Property(x => x.Created).HasDefaultValueSql("SYSDATETIMEOFFSET()");
                a.OnDelete(DeleteBehavior.Cascade);
            });

            builder.Property(x => x.IsInternal)
                .HasComputedColumnSql(@"case when [MarketerOrganizationId]=[ProviderOrganizationId] then cast(1 as bit) else cast(0 as bit) end");


            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasMaxLength(2);

            builder.Property(p => p.ProvinceState)
                .IsRequired()
                .HasColumnType("varchar(3)")
                .HasMaxLength(3);

            builder.HasOne(x => x.OrganizationMarketer)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => new
                {
                    x.MarketerOrganizationId,
                    x.MarketerId
                }).IsRequired();

            builder.HasOne(x => x.AccountManager)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.AccountManagerId)
                .IsRequired(false);

            builder.HasOne(x => x.Marketer)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.MarketerId)
                .IsRequired();

            builder.HasOne(x => x.OrganizationAccountManager)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => new
                {
                    OrganizationId = x.AccountManagerOrganizationId,
                    x.AccountManagerId
                }).IsRequired(false);

            builder.HasOne(x => x.MarketerOrganization)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.MarketerOrganizationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProviderOrganization)
                .WithMany(x => x.Leads)
                .HasForeignKey(x => x.ProviderOrganizationId)
                .IsRequired();

            builder.HasOne(x => x.Person)
                .WithOne(x => x.Lead)
                .HasForeignKey<Lead>(x => x.PersonId);

            AddAuditProperties(builder);
        }
    }
}