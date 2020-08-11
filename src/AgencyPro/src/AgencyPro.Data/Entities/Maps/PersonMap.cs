// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.FinancialAccounts.Models;
using AgencyPro.Core.Leads.Models;
using AgencyPro.Core.People.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class PersonMap : EntityMap<Person>
    {
       

        public override void ConfigureInternal(EntityTypeBuilder<Person> builder)
        {
            // id properties
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            // name properties
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.DisplayName).HasComputedColumnSql("[FirstName] + ' ' + [LastName]");


            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.Address).HasMaxLength(100);
            builder.Property(x => x.Address2).HasMaxLength(100);

            builder
                .Property(p => p.Iso2)
                .HasColumnType("char(2)")
                .HasDefaultValue("US")
                .HasMaxLength(2);

            builder.Property(p => p.ProvinceState)
                .HasColumnType("varchar(3)")
                .HasMaxLength(3);

            builder
                .Property(p => p.City)
                .HasMaxLength(200);
            
            builder
                .HasOne(t => t.ApplicationUser)
                .WithOne(x => x.Person)
                .HasForeignKey<Person>(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(x => x.OrganizationPeople)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder.HasOne(x => x.Lead)
                .WithOne(x => x.Person)
                .HasForeignKey<Lead>(x => x.PersonId)
                .IsRequired(false);

            builder.HasMany(x => x.PayoutIntents)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId)
                .IsRequired(true);

            AddAuditProperties(builder);
        }
    }
}