// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.Charges.ViewModels;
using AgencyPro.Core.Stripe.Model;

namespace AgencyPro.Core.Retainers.ViewModels
{

    public class RetainerDetails : RetainerOutput
    {
        public ICollection<ChargeOutput> Charges { get; set; }
    } 

    public class RetainerOutput
    {
        public Guid CustomerId { get; set; }
        public Guid ProjectId { get; set; }
        public decimal Amount { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerOrganizationName { get; set; }
        public string ProviderOrganizationName { get; set; }
       
    }
}