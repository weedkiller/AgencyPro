// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Linq;
using System.Text;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Geo.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CountryMap : EntityMap<Country>
    {
        public override void SeedInternal(EntityTypeBuilder<Country> entity)
        {
            var file = GetResourceFilename("countries");
            using (var stream = _assembly.GetManifestResourceStream(file))
            {
                if (stream == null) return;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var csvReader = GetReader(reader);
                    var records = csvReader.GetRecords<Country>().ToArray();
                    records.ForEach(r =>
                    {
                        r.ObjectState = ObjectState.Added;
                        r.EnabledCountry = null;
                    });
                    entity.HasData(records);
                }
            }
        }

        public override void ConfigureInternal(EntityTypeBuilder<Country> builder)
        {
            builder
                .HasKey(p => p.Iso2);

            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasMaxLength(2);

            builder
                .HasIndex(u => u.Iso2)
                .IsUnique();

            builder
                .Property(p => p.Iso3)
                .HasColumnType("char(3)")
                .HasMaxLength(3)
                .IsRequired();

            builder
                .Property(p => p.Currency)
                .HasColumnType("char(3)")
                .HasMaxLength(3);

            builder
                .Property(p => p.Name)
                .HasMaxLength(200).IsRequired();

            builder
                .Property(p => p.Capital)
                .HasMaxLength(200);

            builder
                .Property(p => p.OfficialName)
                .HasMaxLength(200);

            builder
                .Property(p => p.Latitude)
                .HasMaxLength(20);

            builder
                .Property(p => p.Longitude)
                .HasMaxLength(20);

            builder
                .Property(p => p.PhoneCode)
                .HasMaxLength(20);

            builder
                .Property(p => p.PostalCodeRegex)
                .HasMaxLength(200);

            builder
                .Property(p => p.PostalCodeFormat)
                .HasMaxLength(200);
        }
    }
}