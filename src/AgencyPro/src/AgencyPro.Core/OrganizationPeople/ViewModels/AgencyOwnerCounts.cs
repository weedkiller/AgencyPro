// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationPeople.ViewModels
{
    public class AgencyOwnerCounts
    {
        public int Accounts { get; set; }
        public int Projects { get; set; }
        public int Contracts { get; set; }
        public int People { get; set; }
        public int Invoices { get; set; }
        public int Leads { get; set; }
        public int Candidates { get; set; }
        public int Paychecks { get; set; }
        public int WorkOrders { get; set; }
        public int Proposals { get; set; }
        public int Stories { get; set; }
        public int TimeEntries { get; set; }

        public int Totals => TimeEntries 
                             + Stories 
                             + Proposals 
                             + Paychecks 
                             + Accounts 
                             + Leads 
                             + Projects 
                             + Contracts 
                             + WorkOrders 
                             + People 
                             + Invoices 
                             + Candidates;

    }
}