// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Projects.Enums;

namespace AgencyPro.Core.Projects.Models
{
    public class ProjectStatusTransition : IObjectState
    {
        public int Id { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectStatus Status { get; set; }
        public ObjectState ObjectState { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}