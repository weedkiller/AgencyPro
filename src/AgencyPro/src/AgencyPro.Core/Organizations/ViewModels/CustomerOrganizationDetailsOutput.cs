// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using AgencyPro.Core.BuyerAccounts.ViewModels;

namespace AgencyPro.Core.Organizations.ViewModels
{
    public class CustomerOrganizationDetailsOutput : CustomerOrganizationOutput
    {
        public BuyerAccountOutput BuyerAccount { get; set; }
    }
}