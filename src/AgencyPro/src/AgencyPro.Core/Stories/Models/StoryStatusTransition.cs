// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Data.Infrastructure;
using AgencyPro.Core.Stories.Enums;

namespace AgencyPro.Core.Stories.Models
{
    public class StoryStatusTransition : IObjectState
    {
        public int Id { get; set; }
        public Guid StoryId { get; set; }
        public StoryStatus Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}