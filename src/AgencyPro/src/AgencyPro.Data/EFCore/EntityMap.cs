// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.IO;
using System.Reflection;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.EFCore
{
    public abstract class EntityMap<T> : IEntityTypeConfiguration<T>
        where T : class, new()
    {
        private static string _seederPath;
        protected static Assembly _assembly;

        protected EntityMap()
        {
            _assembly = typeof(AppDbContext).GetTypeInfo().Assembly;
            _seederPath = string.Format("{0}.Seeders.csv", _assembly.GetName().Name);
        }

        public void Configure(EntityTypeBuilder<T> builder)
        {
            ConfigureInternal(builder);
            //SeedInternal(builder);
        }

        public abstract void ConfigureInternal(EntityTypeBuilder<T> builder);

        public virtual void SeedInternal(EntityTypeBuilder<T> entity)
        {
        }

        protected void AddAuditProperties(EntityTypeBuilder entity, bool addModifiers = false)
        {
            entity
                .Property<DateTimeOffset>("Created")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            entity
                .Property<DateTimeOffset>("Updated")
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            if (!addModifiers) return;

            entity
                .Property<Guid>("CreatedById");

            entity
                .Property<Guid>("UpdatedById");
        }

        protected static string GetResourceFilename(string resouce)
        {
            return string.Format("{0}.{1}.csv", _seederPath, resouce);
        }

        protected static CsvReader GetReader(StreamReader reader)
        {
            var csvReader = new CsvReader(reader);
            csvReader.Configuration.Delimiter = "|";
            csvReader.Configuration.HeaderValidated = null;
            csvReader.Configuration.MissingFieldFound = null;

            return csvReader;
        }
    }
}