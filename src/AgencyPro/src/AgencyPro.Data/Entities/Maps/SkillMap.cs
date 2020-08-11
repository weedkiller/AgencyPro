// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Extensions;
using AgencyPro.Core.Skills.Models;
using AgencyPro.Data.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AgencyPro.Data.Entities.Maps
{
    public class SkillMap : EntityMap<Skill>
    {
        public override void SeedInternal(EntityTypeBuilder<Skill> entity)
        {

            //var file = GetResourceFilename("skills");

            //using (var stream = _assembly.GetManifestResourceStream(file))
            //{
            //    if (stream == null) return;
            //    using (var reader = new StreamReader(stream, Encoding.UTF8))
            //    {
            //        var csvReader = GetReader(reader);
            //        var records = csvReader.GetRecords<Skill>().ToArray();
            //        records.ForEach(r =>
            //        {
            //            r.ObjectState = ObjectState.Added;
            //            r.Created = EntityConstants.DefaultDateTime;
            //            r.Updated = EntityConstants.DefaultDateTime;
            //        });

            //        entity.HasData(records);
            //    }
            //}

            var skills = new List<Skill>()
            {
                new Skill()
                {
                    Id = EntityConstants.NetProgrammingSkillId,
                    Name = ".NET Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.SqlProgrammingSkillId,
                    Name = "SQL Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.TypeScriptProgrammingSkillId,
                    Name = "TypeScript Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },

                new Skill()
                {
                    Id = EntityConstants.BusinessManagementSkillId,
                    Name = "Business Management",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },

                new Skill()
                {
                    Id = EntityConstants.BusinessConsultingSkillId,
                    Name = "Business Consulting",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },

                new Skill()
                {
                    Id = EntityConstants.MotionGraphicsSkillId,
                    Name = "Motion Graphics",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.CreativeWritingSkillId,
                    Name = "Writing (Creative)",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.TechnicalWritingSkillId,
                    Name = "Writing (Technical)",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.StoryBoardingSkillId,
                    Name = "StoryBoarding",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },

                new Skill()
                {
                    Id = EntityConstants.GraphicDesignSkillId,
                    Name = "Graphic Design",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.WebDesignUXSkillId,
                    Name = "Web Design",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.MarketingSkillId,
                    Name = "Marketing",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.AdvertisingSkillId,
                    Name = "Advertising",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.ApplicationDevelopmentSkillId,
                    Name = "Application Development",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.MobileDevelopmentSkillId,
                    Name = "Mobile Development",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.SystemAdministrationSkillId,
                    Name = "System Administration",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },
                new Skill()
                {
                    Id = EntityConstants.DraftingSkillId,
                    Name = "Drafting",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },


                new Skill()
                {
                    Id = EntityConstants.SwimsuitModelingSkillId,
                    Name = "Swimsuit Modeling",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },


                new Skill()
                {
                    Id = EntityConstants.FitnessModelingSkillId,
                    Name = "Fitness Modeling",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },


                new Skill()
                {
                    Id = EntityConstants.CommercialModelingSkillId,
                    Name = "Commercial Modeling",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                },

                new Skill()
                {
                    Id = EntityConstants.JavaProgrammingSkillId,
                    Name = "Java Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.JavaScriptProgrammingSkillId,
                    Name = "JavaScript Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.PHPProgrammingSkillId,
                    Name = "PHP Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.CPlusPlusProgrammingSkillId,
                    Name = "C++ Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.PythonProgrammingSkillId,
                    Name = "Python Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.RubyProgrammingSkillId,
                    Name = "Ruby Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

                ,new Skill()
                {
                    Id = EntityConstants.CProgrammingSkillId,
                    Name = "C Programming",
                    Created = EntityConstants.DefaultDateTime,
                    Updated = EntityConstants.DefaultDateTime
                }

            };

            entity.HasData(skills);
        }

        public override void ConfigureInternal(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(x => x.Id);

            AddAuditProperties(builder);

        }
    }
}