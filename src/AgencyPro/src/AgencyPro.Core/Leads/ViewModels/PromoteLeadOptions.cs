// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace AgencyPro.Core.Leads.ViewModels
{
    public class PromoteLeadOptions
    {
        public string OrganizationName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? PaymentTermId { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
    }
}