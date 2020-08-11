// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationMarketers
{
    public class OrganizationMarketerInput : IOrganizationMarketer
    {
        [Required] public virtual decimal MarketerStream { get; set; }
        public virtual decimal MarketerBonus { get; set; }
        public virtual Guid MarketerId { get; set; }
        public virtual Guid OrganizationId { get; set; }
    }
}