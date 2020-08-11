// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Comments.Services;

namespace AgencyPro.Core.Comments.ViewModels
{
    public class CommentOutput : CommentInput, IComment
    {
        public Guid PersonId { get; set; }
        public Guid OrganizationId { get; set; }
        public string PersonName { get; set; }
        public string PersonImageUrl { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationImageUrl { get; set; }

        public DateTimeOffset Created { get; set; }

        public override bool Internal { get; set; }
    }
}
