﻿// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using AgencyPro.Core.Organizations.Models;
using AgencyPro.Core.Organizations.RecruitingOrganizations.Services;
using AgencyPro.Core.Organizations.Services;

namespace AgencyPro.Core.Organizations.RecruitingOrganizations.ViewModels
{
    public class RecruitingOrganizationOutput : RecruitingOrganizationInput, IRecruitingOrganization, IOrganization
    {
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public string City { get; set; }
        public string Iso2 { get; set; }
        public string ProvinceState { get; set; }
        public string PostalCode { get; set; }
        public int Recruiters { get; set; }
    }
}