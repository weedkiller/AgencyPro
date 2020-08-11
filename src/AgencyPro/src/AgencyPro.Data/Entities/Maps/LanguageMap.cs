// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.IO;
using System.Linq;
using System.Text;
using AgencyPro.Core.Common.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Extensions;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class LanguageMap : EntityMap<Language>
    {
        public override void SeedInternal(EntityTypeBuilder<Language> entity)
        {
            var file = GetResourceFilename("languages");
            using (var stream = _assembly.GetManifestResourceStream(file))
            {
                if (stream == null) return;
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    var csvReader = GetReader(reader);
                    var records = csvReader.GetRecords<Language>().ToArray();
                    records.ForEach(r => { r.ObjectState = ObjectState.Added; });
                    entity.HasData(records);
                }
            }
        }

        public override void ConfigureInternal(EntityTypeBuilder<Language> builder)
        {
            builder
                .HasKey(p => p.Code);

            builder
                .Property(p => p.Code)
                .HasMaxLength(20);

            builder
                .HasIndex(u => u.Code)
                .IsUnique();

            builder
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}