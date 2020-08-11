// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Common.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class LanguageCountryMap : EntityMap<LanguageCountry>
    {
        public override void ConfigureInternal(EntityTypeBuilder<LanguageCountry> builder)
        {
            builder
                .HasKey(a => new {a.LanguageCode, a.Iso2});

            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property(p => p.LanguageCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(a => a.Country)
                .WithMany(p => p.Languages)
                .HasForeignKey(a => a.Iso2)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Language)
                .WithMany(s => s.Countries)
                .HasForeignKey(a => a.LanguageCode)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}