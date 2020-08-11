// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Newtonsoft.Json;

namespace AgencyPro.Core.TimeEntries.ViewModels.TimeMatrix
{
    
    public class CustomerTimeMatrixOutput : TimeMatrixOutput
    {
        //public CustomerOrganizationContractorOutput OrganizationContractor { get; set; }
        //public CustomerOrganizationCustomerOutput OrganizationCustomer { get; set; }
        //public CustomerOrganizationAccountManagerOutput OrganizationAccountManager { get; set; }
        //public CustomerOrganizationProjectManagerOutput OrganizationProjectManager { get; set; }
        //public CustomerProjectOutput Project { get; set; }
        //public CustomerContractOutput Contract { get; set; }

        [JsonIgnore]
        public override decimal TotalAccountManagerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalAgencyStream { get; set; }
        [JsonIgnore]
        public override decimal TotalMarketingAgencyStream { get; set; }
        [JsonIgnore]
        public override decimal TotalRecruitingAgencyStream { get; set; }
        [JsonIgnore]
        public override decimal TotalContractorStream { get; set; }
        [JsonIgnore]
        public override decimal TotalMarketerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalProjectManagerStream { get; set; }
        [JsonIgnore]
        public override decimal TotalRecruiterStream { get; set; }
        [JsonIgnore]
        public override decimal TotalSystemStream { get; set; }
    }
}