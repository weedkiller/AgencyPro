// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class RecruiterCounts
    {
        public int Contractors { get; set; }
        public int Candidates { get; set; }
        public int Contracts { get; set; }
        public int Totals => Contractors + Candidates + Contracts;

    }
}