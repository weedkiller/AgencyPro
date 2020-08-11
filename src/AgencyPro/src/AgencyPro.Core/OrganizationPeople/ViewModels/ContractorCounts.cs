// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class ContractorCounts
    {
        public int Contracts { get; set; }
        public int Stories { get; set; }
        public int TimeEntries { get; set; }
        public int Totals => Stories + Contracts + TimeEntries;
    }
}