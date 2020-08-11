// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels
{
    public class RecruitingOrganizationInput : RecruitingOrganizationUpgradeInput
    {
        [BindRequired] public virtual Guid DefaultRecruiterId { get; set; }
        


    }
}