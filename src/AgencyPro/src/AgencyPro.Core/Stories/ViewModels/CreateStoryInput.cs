// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.Stories.ViewModels
{
    public class CreateStoryInput
    {
        [Required] public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual int StoryPoints { get; set; }

        [BindRequired] public virtual Guid ProjectId { get; set; }

        public Guid? ContractorId { get; set; }

        public Guid? TemplateId { get; set; }
    }
}