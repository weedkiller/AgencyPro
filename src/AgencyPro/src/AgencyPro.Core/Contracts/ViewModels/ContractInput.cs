// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;

namespace AgencyPro.Core.Contracts.ViewModels
{
    public class ContractInput
    {
        public virtual Guid ProjectId { get; set; }
        public virtual Guid ContractorId { get; set; }
        public virtual Guid ContractorOrganizationId { get; set; }

        [Range(1,60)]
        public virtual int MaxWeeklyHours { get; set; }

    }
}