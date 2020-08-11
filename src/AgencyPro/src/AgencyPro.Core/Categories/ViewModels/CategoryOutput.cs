// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.Categories.Services;
using Newtonsoft.Json;

namespace AgencyPro.Core.Categories.ViewModels
{
    public class CategoryOutput : CategoryInput, ICategory
    {
        public virtual int CategoryId { get; set; }

        [JsonIgnore] public virtual bool Searchable { get; set; }
        public decimal DefaultRecruiterStream { get; set; }
        public decimal DefaultMarketerStream { get; set; }
        public decimal DefaultProjectManagerStream { get; set; }
        public decimal DefaultAccountManagerStream { get; set; }
        public decimal DefaultContractorStream { get; set; }
        public decimal DefaultAgencyStream { get; set; }
    }
}