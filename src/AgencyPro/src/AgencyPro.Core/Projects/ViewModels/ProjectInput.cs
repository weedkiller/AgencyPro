// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.Projects.ViewModels
{
    public class ProjectInput
    {
        private string _abbreviation;

        [BindRequired] public virtual Guid ProjectManagerId { get; set; }

        [BindNever] public virtual Guid ProjectManagerOrganizationId { get; set; }

        public virtual int AccountNumber { get; set; }
        public bool AutoApproveTimeEntries { get; set; }

        public string Name { get; set; }

        public string Abbreviation
        {
            get => _abbreviation.ToUpper();
            set => _abbreviation = value;
        }
    }
}