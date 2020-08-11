// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using AgencyPro.Core.ViewModels;

namespace AgencyPro.Core.Organizations.Models
{

    public class OrganizationResult : BaseResult
    {
        private readonly List<OrganizationError> _errors = new List<OrganizationError>();
        
        public static OrganizationResult Failed { get; } = new OrganizationResult();

        public IEnumerable<OrganizationError> Errors => _errors;
        
        public Guid? OrganizationId { get; set; }
    }
}