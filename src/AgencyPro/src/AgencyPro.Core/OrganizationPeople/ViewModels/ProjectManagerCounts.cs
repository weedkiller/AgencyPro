// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class ProjectManagerCounts
    {
        public int Projects { get; set; }
        public int Proposals { get; set; }
        public int Contracts { get; set; }
        public int Stories { get; set; }
        public int Candidates { get; set; }
        public int TimeEntries { get; set; }

        public int Totals => Proposals + Projects + Contracts + Stories + Candidates + TimeEntries;
    }
}