// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;
using AgencyPro.Core.Categories.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgencyPro.Data.Entities.Maps
{
    public class CategorySkillMap : EntityMap<CategorySkill>
    {
        public override void SeedInternal(EntityTypeBuilder<CategorySkill> entity)
        {
            entity.HasData(new List<CategorySkill>()
            {
                // software agency
                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.NetProgrammingSkillId
                },
                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.TypeScriptProgrammingSkillId
                },
                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.SqlProgrammingSkillId
                },
                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.TechnicalWritingSkillId
                },
                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.MobileDevelopmentSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.ApplicationDevelopmentSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.WebDesignUXSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.PHPProgrammingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.JavaProgrammingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.JavaScriptProgrammingSkillId
                },
                

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.CPlusPlusProgrammingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.CProgrammingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.PythonProgrammingSkillId
                },


                new CategorySkill()
                {
                    CategoryId = 2,
                    SkillId = EntityConstants.RubyProgrammingSkillId
                },


                // creative agency
                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.AdvertisingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.MarketingSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.WebDesignUXSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.MotionGraphicsSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.CreativeWritingSkillId
                },
                
                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.StoryBoardingSkillId
                },


                new CategorySkill()
                {
                    CategoryId = 3,
                    SkillId = EntityConstants.GraphicDesignSkillId
                },

                // consulting firm
                new CategorySkill()
                {
                    CategoryId = 4,
                    SkillId = EntityConstants.BusinessManagementSkillId
                },

                new CategorySkill()
                {
                    CategoryId = 4,
                    SkillId = EntityConstants.BusinessConsultingSkillId
                },

               
            });
        }

        public override void ConfigureInternal(EntityTypeBuilder<CategorySkill> builder)
        {
            builder
                .HasKey(sc => new {sc.SkillId, sc.CategoryId});

            builder
                .HasOne(x => x.Skill)
                .WithMany(x => x.SkillCategories)
                .HasForeignKey(x => x.SkillId);

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.AvailableSkills)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}