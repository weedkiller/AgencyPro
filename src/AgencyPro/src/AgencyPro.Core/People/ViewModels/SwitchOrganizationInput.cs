// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AgencyPro.Core.People.ViewModels
{
    public class SwitchOrganizationInput
    {
        [BindNever] public Guid PersonId { get; set; }

        [BindRequired] public Guid OrganizationId { get; set; }
    }
}