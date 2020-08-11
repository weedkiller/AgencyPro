// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationAccountManagers
{
    public class OrganizationAccountManagerStatistics : OrganizationAccountManagerOutput
    {
        public virtual int TotalAccounts { get; set; }
        public virtual int TotalProjects { get; set; }
        public virtual int TotalContracts { get; set; }
        public virtual int TotalLeads { get; set; }
        public virtual int MaxWeeklyHours { get; set; }
    }
}