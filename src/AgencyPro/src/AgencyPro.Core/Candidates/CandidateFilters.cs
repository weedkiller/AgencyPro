// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;

namespace AgencyPro.Core.Candidates
{
    public class CandidateFilters
    {
        public static readonly CandidateFilters NoFilter = new CandidateFilters();

        public Guid? ProjectManagerId { get; set; }
        public Guid? ProjectManagerOrganizationId { get; set; }
    }
}