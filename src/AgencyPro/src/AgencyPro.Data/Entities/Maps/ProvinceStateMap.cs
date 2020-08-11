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
    public class ProvinceStateMap : EntityMap<ProvinceState>
    {
        public override void SeedInternal(EntityTypeBuilder<ProvinceState> entity)
        {
            var file = GetResourceFilename("province_states");
            using (var stream = _assembly.GetManifestResourceStream(file))
            {
                if (stream == null) return;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var csvReader = GetReader(reader);
                    var records = csvReader.GetRecords<ProvinceState>().ToArray();
                    records.ForEach(r =>
                    {
                        r.ObjectState = ObjectState.Added;
                        r.Country = null;
                    });

                    entity.HasData(records);
                }
            }
        }

        public override void ConfigureInternal(EntityTypeBuilder<ProvinceState> builder)
        {
            builder
                .HasKey(k => new {k.Iso2, k.Code});

            builder.Property(p => p.Iso2)
                .IsRequired()
                .HasColumnType("char(2)")
                .HasMaxLength(2);

            builder.Property(p => p.Code)
                .IsRequired()
                .HasColumnType("varchar(3)")
                .HasMaxLength(3);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(a => a.Country)
                .WithMany(p => p.ProvinceStates)
                .HasForeignKey(a => a.Iso2)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}