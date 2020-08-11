// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Geo.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace AgencyPro.Data.Entities.Maps
{
    public class EnabledCountryMap : EntityMap<EnabledCountry>
    {
        public override void ConfigureInternal(EntityTypeBuilder<EnabledCountry> builder)
        {
            builder
                .HasKey(a => new { a.Iso2 });

            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasMaxLength(2);

            builder
                .HasOne(a => a.Country)
                .WithOne(p => p.EnabledCountry)
                .HasForeignKey<EnabledCountry>(b => b.Iso2);
        }

        public override void SeedInternal(EntityTypeBuilder<EnabledCountry> entity)
        {
            entity.HasData(new List<EnabledCountry>
            {
                new EnabledCountry { Iso2 = "AU", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "AT", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "BE", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "CZ", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "DK", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "EE", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "FI", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "FR", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "DE", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "GR", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "HK", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "IN", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "IE", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "IT", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "JP", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "LV", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "LT", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "LU", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "MY", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "MX", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "NL", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "NZ", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "NO", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "PL", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "PT", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "SG", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "SK", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "SI", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "ES", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "SE", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "CH", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "GB", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "US", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added },
                new EnabledCountry { Iso2 = "CA", Enabled = true, Created = EntityConstants.DefaultDateTime, Updated = EntityConstants.DefaultDateTime, ObjectState = ObjectState.Added }
            });
        }
    }
}