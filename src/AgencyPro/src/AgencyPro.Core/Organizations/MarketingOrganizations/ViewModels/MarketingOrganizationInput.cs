// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.Organizations.MarketingOrganizations.ViewModels
{
    public class MarketingOrganizationInput : MarketingOrganizationUpgradeInput
    {
      
        [BindRequired] public virtual Guid DefaultMarketerId { get; set; }
     


    }
}