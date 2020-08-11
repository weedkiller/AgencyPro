// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CategoryMap : EntityMap<Category>
    {
        internal static List<Category> Categories;

        public override void ConfigureInternal(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.CategoryId);

            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired().HasMaxLength(100);

            builder.Property(x => x.ContractorTitle)
                .HasMaxLength(50)
                .HasDefaultValue("Contractor")
                .IsRequired();

            builder.Property(x => x.AccountManagerTitle)
                .HasMaxLength(50)
                .HasDefaultValue("Account Manager")
                .IsRequired();

            builder.Property(x => x.ProjectManagerTitle)
                .HasMaxLength(50)
                .HasDefaultValue("Project Manager")
                .IsRequired();

            builder.Property(x => x.AccountManagerTitlePlural)
                .HasMaxLength(50)
                .HasDefaultValue("Account Managers")
                .IsRequired();

            builder.Property(x => x.ProjectManagerTitlePlural)
                .HasMaxLength(50)
                .HasDefaultValue("Project Managers")
                .IsRequired();

            builder.Property(x => x.ContractorTitlePlural).HasMaxLength(50)
                .HasDefaultValue("Contractors")
                .IsRequired();

            builder.Property(x => x.RecruiterTitlePlural).HasMaxLength(50)
                .HasDefaultValue("Recruiters")
                .IsRequired();

            builder.Property(x => x.RecruiterTitle).HasMaxLength(50)
                .HasDefaultValue("Recruiter")
                .IsRequired();

            builder.Property(x => x.MarketerTitle).HasMaxLength(50)
                .HasDefaultValue("Marketer")
                .IsRequired();

            builder.Property(x => x.MarketerTitlePlural).HasMaxLength(50)
                .HasDefaultValue("Marketers")
                .IsRequired();

            builder.Property(x => x.CustomerTitle).HasMaxLength(50)
                .HasDefaultValue("Customer")
                .IsRequired();

            builder.Property(x => x.CustomerTitlePlural).HasMaxLength(50)
                .HasDefaultValue("Customers")
                .IsRequired();

            builder.Property(x => x.DefaultAccountManagerStream)
                .HasDefaultValue(5)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultMarketerStream)
                .HasDefaultValue(2.50)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultRecruiterStream)
                .HasDefaultValue(2.50)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultProjectManagerStream)
                .HasDefaultValue(7.50)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultContractorStream)
                .HasDefaultValue(25)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultAgencyStream)
                .HasDefaultValue(15)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultMarketingAgencyStream)
                .HasDefaultValue(1)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultRecruitingAgencyStream)
                .HasDefaultValue(2)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultMarketerBonus)
                .HasDefaultValue(10)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.DefaultMarketingAgencyBonus)
                .HasDefaultValue(10)
                .HasColumnType("Money")
                .IsRequired();

            builder.Property(x => x.Searchable)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasMany(x => x.Organizations)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(x => x.Positions)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

        }

        public override void SeedInternal(EntityTypeBuilder<Category> entity)
        {
            Categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    Name = "Customer Organization",
                    ProjectManagerTitle = "Project Manager",
                    ProjectManagerTitlePlural = "Project Managers",
                    AccountManagerTitle = "Account Manager",
                    AccountManagerTitlePlural = "Account Managers",
                    ContractorTitle = "Contractor",
                    ContractorTitlePlural = "Contractors",
                    RecruiterTitlePlural = "Recruiters",
                    RecruiterTitle = "Recruiter",
                    CustomerTitle = "Customer",
                    CustomerTitlePlural = "Customers",
                    MarketerTitlePlural = "Marketers",
                    MarketerTitle = "Marketer"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Software",
                    ProjectManagerTitle = "Project Manager",
                    ProjectManagerTitlePlural = "Project Managers",
                    AccountManagerTitle = "Account Manager",
                    AccountManagerTitlePlural = "Account Managers",
                    ContractorTitle = "Contractor",
                    ContractorTitlePlural = "Contractors",
                    RecruiterTitlePlural = "Recruiters",
                    RecruiterTitle = "Recruiter",
                    CustomerTitle = "Customer",
                    CustomerTitlePlural = "Customers",
                    MarketerTitlePlural = "Marketers",
                    MarketerTitle = "Marketer"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Creative",
                    ProjectManagerTitle = "Project Manager",
                    ProjectManagerTitlePlural = "Project Managers",
                    AccountManagerTitle = "Account Manager",
                    AccountManagerTitlePlural = "Account Managers",
                    ContractorTitle = "Contractor",
                    ContractorTitlePlural = "Contractors",
                    RecruiterTitlePlural = "Recruiters",
                    RecruiterTitle = "Recruiter",
                    CustomerTitle = "Customer",
                    CustomerTitlePlural = "Customers",
                    MarketerTitlePlural = "Marketers",
                    MarketerTitle = "Marketer"
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Consulting",
                    ProjectManagerTitle = "Project Manager",
                    ProjectManagerTitlePlural = "Project Managers",
                    AccountManagerTitle = "Account Manager",
                    AccountManagerTitlePlural = "Account Managers",
                    ContractorTitle = "Contractor",
                    ContractorTitlePlural = "Contractors",
                    RecruiterTitlePlural = "Recruiters",
                    RecruiterTitle = "Recruiter",
                    CustomerTitle = "Customer",
                    CustomerTitlePlural = "Customers",
                    MarketerTitlePlural = "Marketers",
                    MarketerTitle = "Marketer"
                }
            };

            Categories.ForEach(x =>
            {
                x.ObjectState = ObjectState.Added;
                x.Searchable = true;
            });

            entity.HasData(Categories);
        }
    }
}