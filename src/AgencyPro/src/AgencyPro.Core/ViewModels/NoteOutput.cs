// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.ViewModels
{
    public class NoteOutput
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Meta { get; set; }
        public bool Starred { get; set; }
        public int SortOrder { get; set; }
        public DateTimeOffset? Updated { get; set; }
    }
}