// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.UserAccount.Models;

namespace AgencyPro.Core.Models
{
    public class Note : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public bool Starred { get; set; }
        public int SortOrder { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public new DateTimeOffset? Updated { get; set; }
    }
}