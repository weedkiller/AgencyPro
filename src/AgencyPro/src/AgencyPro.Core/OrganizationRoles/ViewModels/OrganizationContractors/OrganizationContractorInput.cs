// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel.DataAnnotations;
using AgencyPro.Core.OrganizationRoles.Services;

namespace AgencyPro.Core.OrganizationRoles.ViewModels.OrganizationContractors
{
    public class OrganizationContractorInput : IOrganizationContractor
    {
        public virtual bool IsFeatured { get; set; }
        public virtual string Biography { get; set; }
        public virtual string PortfolioMediaUrl { get; set; }

        [Range(0, 999)]
        public virtual decimal ContractorStream { get; set; }

        public Guid ContractorId { get; set; }
        public Guid OrganizationId { get; set; }
        public bool AutoApproveTimeEntries { get; set; }

    }
}